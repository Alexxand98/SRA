using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SRA.ApiRest.Models.DTOs.FranjaHorariaDTO;
using SRA.ApiRest.Models.Entity;
using SRA.ApiRest.Repository.IRepository;

namespace SRA.ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FranjaHorariaController : BaseController<FranjaHoraria, FranjaHorariaDTO, CreateFranjaHorariaDTO>
    {
        public FranjaHorariaController(
            IFranjaHorariaRepository repo,
            IMapper mapper,
            ILogger<FranjaHorariaController> logger
        ) : base(repo, mapper, logger)
        {
        }
    }
}
