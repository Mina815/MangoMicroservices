using Mango.Web_MVC.Models;

namespace Mango.Web_MVC.Services.IServices
{
    public interface IAuthService
    {
        Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);

        Task<ResponseDto?> AssignRole(RegistrationRequestDto RegisterModel);
    }
}
