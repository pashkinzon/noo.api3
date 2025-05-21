using System.Security.Claims;
using Noo.Api.Core.Exceptions.Http;

public static class ClaimsPrincipalExtensions
{
    public static Ulid GetId(this ClaimsPrincipal user)
    {
        var raw = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedException();
        return Ulid.Parse(raw);
    }
}
