using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;
        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _response = new ResponseDto();
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var result = await _authService.RegisterAsync(registrationRequestDto);
            if (!string.IsNullOrEmpty(result))
            {
                _response.IsSuccess = false;
                _response.Message = result; 
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var result = await _authService.LoginAsync(loginRequestDto);
            if (result.User == null || string.IsNullOrEmpty(result.Token))
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = result;
            return Ok(_response);
        }

        [HttpPost]
        [Route("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto RegisterModel)
        {
            var result = await _authService.AssignRole(RegisterModel.Email, RegisterModel.Role);
            if (!result)
            {
                _response.IsSuccess = false;
                _response.Message = "Failed to assign role";
                return BadRequest(_response);
            }

            return Ok(_response);
        }

    }
}
