using Mango.Web_MVC.Models;
using Mango.Web_MVC.Services.IServices;
using Mango.Web_MVC.Utility;

namespace Mango.Web_MVC.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRole(RegistrationRequestDto RegisterModel)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.POST,
                Data = RegisterModel,
                Url = StaticData.AuthAPIBase + "/api/auth/assign-role",
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.POST,
                Data = loginRequestDto,
                Url = StaticData.AuthAPIBase + "/api/auth/login",
            });
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.POST,
                Data = registrationRequestDto,
                Url = StaticData.AuthAPIBase + "/api/auth/register",
            });
        }

    }
}
