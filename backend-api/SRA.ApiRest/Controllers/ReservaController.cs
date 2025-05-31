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
    }
}
