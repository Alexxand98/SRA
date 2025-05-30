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
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO dto)
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

        [AllowAnonymous]
        [HttpPost("google-login")]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDTO dto)
        {
            if (string.IsNullOrEmpty(dto.TokenId))
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("ID Token requerido");
                return BadRequest(_response);
            }

            var result = await _userRepository.LoginWithGoogleAsync(dto.TokenId);

            if (result == null || string.IsNullOrEmpty(result.Token))
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.Unauthorized;
                _response.ErrorMessages.Add("Token inválido o dominio no permitido");
                return Unauthorized(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = result;
            return Ok(_response);
        }
    }
}
