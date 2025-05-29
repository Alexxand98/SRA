using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SRA.ApiRest.Models.DTOs.ReservaDTO;
using SRA.ApiRest.Models.Entity;
using SRA.ApiRest.Repository.IRepository;

namespace SRA.ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : BaseController<Reserva, ReservaDTO, CreateReservaDTO>
    {
        public ReservaController(
            IReservaRepository repo,
            IMapper mapper,
            ILogger<ReservaController> logger
        ) : base(repo, mapper, logger)
        {
        }
    }
}
