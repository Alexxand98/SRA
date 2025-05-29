using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SRA.ApiRest.Models.DTOs.ProfesorDTO;
using SRA.ApiRest.Models.Entity;
using SRA.ApiRest.Repository.IRepository;

namespace SRA.ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : BaseController<Profesor, ProfesorDTO, CreateProfesorDTO>
    {
        public ProfesorController(
            IProfesorRepository repo,
            IMapper mapper,
            ILogger<ProfesorController> logger
        ) : base(repo, mapper, logger)
        {
        }
    }
}
