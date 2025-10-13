using Mango.Web_MVC.Models;

namespace Mango.Web_MVC.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
        //Task<T> SendAsync<T>(RequestDto requestDto, string token);
        //void Dispose();
        //ResponseDto CreateResponseDto(object result, bool isSuccess, string message = null);

        //ResponseDto CreateResponseDto(string message, bool isSuccess = false);
    }
}
