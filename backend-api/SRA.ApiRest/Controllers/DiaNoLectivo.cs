using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SRA.ApiRest.Models.DTOs.DiaNoLectivoDTO;
using SRA.ApiRest.Models.Entity;
using SRA.ApiRest.Repository.IRepository;

namespace SRA.ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaNoLectivoController : BaseController<DiaNoLectivo, DiaNoLectivoDTO, CreateDiaNoLectivoDTO>
    {
        public DiaNoLectivoController(
            IDiaNoLectivoRepository repo,
            IMapper mapper,
            ILogger<DiaNoLectivoController> logger
        ) : base(repo, mapper, logger)
        {
        }
    }
}
