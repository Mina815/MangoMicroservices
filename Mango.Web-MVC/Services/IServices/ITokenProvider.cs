namespace Mango.Web_MVC.Services.IServices
{
    public interface ITokenProvider
    {
        void SetToken(string token);
        void RemoveToken();
        string? GetToken();
    }
}
