namespace Noo.Api.Core.Security.Authorization;

public interface ICurrentUser
{
    public Ulid? UserId { get; }

    public UserRoles? UserRole { get; }

    public bool IsAuthenticated { get; }

    public bool IsInRole(params UserRoles[] role);
}
