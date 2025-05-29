using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SRA.ApiRest.Models.DTOs;
using SRA.ApiRest.Models.DTOs.UserDTO;
using SRA.ApiRest.Repository.IRepository;
using System.Net;

namespace SRA.ApiRest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ResponseApi _response;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _response = new ResponseApi();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var result = await _userRepository.Login(dto);

            if (result == null || string.IsNullOrEmpty(result.Token))
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.Unauthorized;
                _response.ErrorMessages.Add("Credenciales inválidas");
                return Unauthorized(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = result;
            return Ok(_response);
        }
    }
}
