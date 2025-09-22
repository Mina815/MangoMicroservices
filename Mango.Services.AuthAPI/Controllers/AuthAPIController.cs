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
       
    }
}
