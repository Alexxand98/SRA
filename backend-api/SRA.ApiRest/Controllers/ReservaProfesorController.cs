using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SRA.ApiRest.Models.DTOs;
using SRA.ApiRest.Models.DTOs.ReservaDTO;
using SRA.ApiRest.Models.Entity;
using SRA.ApiRest.Repository.IRepository;
using System.Net;
using System.Security.Claims;

namespace SRA.ApiRest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Profesor")]
    public class ReservaProfesorController : ControllerBase
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ReservaProfesorController> _logger;
        private readonly ResponseApi _response;

        public ReservaProfesorController(
            IReservaRepository reservaRepository,
            IMapper mapper,
            ILogger<ReservaProfesorController> logger)
        {
            _reservaRepository = reservaRepository;
            _mapper = mapper;
            _logger = logger;
            _response = new ResponseApi();
        }

        [HttpGet]
        public async Task<IActionResult> GetMisReservas()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reservas = await _reservaRepository.GetReservasPorProfesorAsync(userId);

            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<IEnumerable<ReservaDTO>>(reservas);
            return Ok(_response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearReserva([FromBody] CreateReservaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.AddRange(ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return BadRequest(_response);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var profesorIdEnDb = await _reservaRepository.ObtenerProfesorIdDesdeAppUserId(userId);
            if (profesorIdEnDb == null || profesorIdEnDb.Value != dto.ProfesorId)
            {
                _response.StatusCode = HttpStatusCode.Forbidden;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("No tienes permiso para crear reservas en nombre de otro profesor.");
                return Forbid();
            }

            var reserva = _mapper.Map<Reserva>(dto);
            await _reservaRepository.CreateAsync(reserva);

            var result = _mapper.Map<ReservaDTO>(reserva);
            _response.StatusCode = HttpStatusCode.Created;
            _response.Result = result;
            return Ok(_response);
        }
    }
}
