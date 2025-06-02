using System.Security.Claims;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Security.Authorization;

public static class ClaimsPrincipalExtensions
{
    public static Ulid GetId(this ClaimsPrincipal user)
    {
        var raw = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedException();
        return Ulid.Parse(raw);
    }

    public static UserRoles GetRole(this ClaimsPrincipal user)
    {
        var raw = user.FindFirst(ClaimTypes.Role)?.Value ?? throw new UnauthorizedException();
        return Enum.TryParse<UserRoles>(raw, out var role) ? role : throw new UnauthorizedException();
    }

    public static Ulid GetSessionId(this ClaimsPrincipal user)
    {
        var raw = user.FindFirst(ClaimTypes.Sid)?.Value;
        return Ulid.Parse(raw);
    }
}
