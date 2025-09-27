using Mango.Web_MVC.Services.IServices;
using Mango.Web_MVC.Utility;

namespace Mango.Web_MVC.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetToken()
        {
            string? token = null;
            bool? exists = _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(StaticData.TokenCookie, out token);
            return exists == true ? token : null;
        }

        public void RemoveToken()
        {
                _httpContextAccessor.HttpContext?.Response.Cookies.Delete(StaticData.TokenCookie);
        }

        public void SetToken(string token)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(StaticData.TokenCookie, token);
        }
    }
}
