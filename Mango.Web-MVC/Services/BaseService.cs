using Mango.Web_MVC.Models;
using Mango.Web_MVC.Services.IServices;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using System.Text.Json;
using static Mango.Web_MVC.Utility.StaticData;

namespace Mango.Web_MVC.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                //Token Auth


                message.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonSerializer.Serialize(requestDto.Data), Encoding.UTF8, "application/json");
                }
                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage? responseMessage = null;
                responseMessage = await client.SendAsync(message);

                switch (responseMessage.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new ResponseDto()
                        { IsSuccess = false, Message = "Resource not found"};
                    //case HttpStatusCode.BadRequest:
                    //    return new ResponseDto()
                    //    { IsSuccess = false, Message = "Bad request" };
                    case HttpStatusCode.Unauthorized:
                        return new ResponseDto()
                        { IsSuccess = false, Message = "Unauthorized access" };
                    case HttpStatusCode.Forbidden:
                        return new ResponseDto()
                        { IsSuccess = false, Message = "Forbidden access" };
                    case HttpStatusCode.InternalServerError:
                        return new ResponseDto()
                        { IsSuccess = false, Message = "Internal server error" };
                    default:
                        var apiContent = await responseMessage.Content.ReadAsStringAsync();
                        var apiResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                        {
                            apiResponse.IsSuccess = false;
                            return apiResponse;
                        }
                        if (apiResponse != null)
                        {

                            apiResponse.IsSuccess = true;
                            return apiResponse;
                        }
                        return new ResponseDto()
                        {
                            IsSuccess = false,
                            Message = "Unknown error occurred"
                        };
                }

            }
            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
