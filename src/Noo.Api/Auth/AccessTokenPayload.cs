using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Auth;

public class AccessTokenPayload
{
    public Ulid UserId { get; set; }

    public UserRoles UserRole { get; set; } = UserRoles.Student;

    public Ulid SessionId { get; set; }

    public DateTime ExpiresAt { get; set; }
}
