using Domain.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SimularmyAPI.Extensions;

public static class HttpContextExtensions
{
    public static Guid ExtractUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var claims = claimsPrincipal.Claims;
        var userIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null)
        {
            throw new DomainException(401, "The user has no id in the token");
        }

        return Guid.Parse(userIdClaim);
    }
}