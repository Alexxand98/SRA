using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SRA.ApiRest.Models.DTOs;
using SRA.ApiRest.Models.DTOs.ReservaDTO;
using SRA.ApiRest.Models.Entity;
using SRA.ApiRest.Repository.IRepository;
using System.Net;

namespace SRA.ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReservaController : BaseController<Reserva, ReservaDTO, CreateReservaDTO>
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ReservaController> _logger;

        public ReservaController(
            IReservaRepository reservaRepository,
            IMapper mapper,
            ILogger<ReservaController> logger
        ) : base(reservaRepository, mapper, logger)
        {
            _reservaRepository = reservaRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        public override async Task<ActionResult<ResponseApi>> Create([FromBody] CreateReservaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseApi
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });
            }

            var reserva = _mapper.Map<Reserva>(dto);

            var (esValida, errores) = await _reservaRepository.ValidarReservaAsync(reserva);
            if (!esValida)
            {
                return BadRequest(new ResponseApi
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = errores
                });
            }

            await _reservaRepository.CreateAsync(reserva);
            var result = _mapper.Map<ReservaDTO>(reserva);

            return CreatedAtRoute($"{ControllerContext.ActionDescriptor.ControllerName}_GetById", new { id = result.Id }, new ResponseApi
            {
                StatusCode = HttpStatusCode.Created,
                Result = result
            });
        }

        [HttpGet("pendientes")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPendientes()
        {
            var reservas = await _reservaRepository.GetReservasPendientesAsync();
            var result = _mapper.Map<IEnumerable<ReservaDTO>>(reservas);

            return Ok(new ResponseApi
            {
                StatusCode = HttpStatusCode.OK,
                Result = result
            });
        }

        [HttpPut("{id}/estado")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] UpdateEstadoReservaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseApi
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage).ToList()
                });
            }

            var success = await _reservaRepository.ActualizarEstadoAsync(id, dto.Estado);
            if (!success)
            {
                return NotFound(new ResponseApi
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = new() { "Reserva no encontrada" }
                });
            }

            return Ok(new ResponseApi
            {
                StatusCode = HttpStatusCode.OK,
                Result = $"Reserva {id} actualizada a '{dto.Estado}'"
            });
        }
    }
}
