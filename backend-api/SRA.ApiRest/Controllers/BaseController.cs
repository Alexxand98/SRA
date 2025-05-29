using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SRA.ApiRest.Models.DTOs;
using SRA.ApiRest.Repository.IRepository;
using System.Net;

namespace SRA.ApiRest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<TEntity, TDto, TCreateDto> : ControllerBase
        where TEntity : class
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;

        protected BaseController(IRepository<TEntity> repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseApi>> GetAll()
        {
            try
            {
                var entities = await _repository.GetAllAsync();
                var result = _mapper.Map<List<TDto>>(entities);

                return Ok(new ResponseApi
                {
                    StatusCode = HttpStatusCode.OK,
                    Result = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los datos");
                return StatusCode(500, new ResponseApi
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessages = new() { ex.Message }
                });
            }
        }

        [HttpGet("{id:int}", Name = "[controller]_GetById")]
        public async Task<ActionResult<ResponseApi>> Get(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                return NotFound(new ResponseApi
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = new() { "Entidad no encontrada" }
                });
            }

            return Ok(new ResponseApi
            {
                StatusCode = HttpStatusCode.OK,
                Result = _mapper.Map<TDto>(entity)
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseApi>> Create([FromBody] TCreateDto dto)
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

            var entity = _mapper.Map<TEntity>(dto);
            await _repository.CreateAsync(entity);
            var result = _mapper.Map<TDto>(entity);

            return CreatedAtRoute($"{ControllerContext.ActionDescriptor.ControllerName}_GetById", new { id = result?.GetHashCode() }, new ResponseApi
            {
                StatusCode = HttpStatusCode.Created,
                Result = result
            });
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ResponseApi>> Update(int id, [FromBody] TDto dto)
        {
            var existing = await _repository.GetAsync(id);
            if (existing == null)
            {
                return NotFound(new ResponseApi
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = new() { "Entidad no encontrada" }
                });
            }

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing);

            return Ok(new ResponseApi
            {
                StatusCode = HttpStatusCode.OK,
                Result = _mapper.Map<TDto>(existing)
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ResponseApi>> Delete(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                return NotFound(new ResponseApi
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = new() { "Entidad no encontrada" }
                });
            }

            await _repository.DeleteAsync(id);
            return Ok(new ResponseApi
            {
                StatusCode = HttpStatusCode.OK,
                Result = $"Entidad con ID {id} eliminada correctamente"
            });
        }
    }
}
