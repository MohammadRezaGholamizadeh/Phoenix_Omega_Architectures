using ServiceLayer.Setups.ServicecInterfaces;
using System.Security.Claims;

namespace ServiceLayer.Setups.TokenManagerInterface
{
    public interface TokenManager : IService
    {
        string Generate(IList<Claim> userClaims, IList<string> userRoles, string applicationUserId);
        string GenerateRefreshToken();
        DateTime GetRefreshTokenExpiryTime();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
