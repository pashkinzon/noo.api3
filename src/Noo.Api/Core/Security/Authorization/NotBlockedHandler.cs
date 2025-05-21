using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Noo.Api.Users.Services;
using Noo.Api.Core.Security.Authorization.Requirements;

namespace Noo.Api.Core.Security.Authorization;

public class NotBlockedHandler : AuthorizationHandler<NotBlockedRequirement>
{
    private readonly IUserService _userService;

    public NotBlockedHandler(IUserService userService)
    {
        _userService = userService;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        NotBlockedRequirement requirement
    )
    {
        var idClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (idClaim is null)
        {
            return;
        }

        var userId = Ulid.Parse(idClaim);
        var isBlocked = await _userService.IsBlockedAsync(userId);

        if (isBlocked)
        {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
    }
}
