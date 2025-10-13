using Mango.Services.AuthAPI.Models;

namespace Mango.Services.AuthAPI.Services.IServices
{
    public interface IjwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
