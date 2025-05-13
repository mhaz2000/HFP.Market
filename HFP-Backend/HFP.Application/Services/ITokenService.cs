using System.Security.Claims;

namespace HFP.Application.Services
{
    public interface ITokenService
    {
        string GenerateToken(string userId, IEnumerable<Claim>? additionalClaims = null);
    }
}
