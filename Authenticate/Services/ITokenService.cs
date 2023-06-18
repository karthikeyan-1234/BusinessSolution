using Authenticate.Models;

namespace Authenticate.Services
{
    public interface ITokenService
    {
        Task<TokenModel> GetJWTTokenAsync(IList<string> userRoles, ApplicationUser user);
        TokenModel GetRefreshToken(ApplicationUser user);
        bool IsValidJWT(string userName, string jwtToken);
        bool IsValidRefreshToken(string userName, string refreshToken);
    }
}