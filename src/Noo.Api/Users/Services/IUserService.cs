using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.Security.Authorization;
using Noo.Api.Users.DTO;
using Noo.Api.Users.Models;

namespace Noo.Api.Users.Services;

public interface IUserService
{
    public Task<bool> UserExistsAsync(string? username, string? email);
    public Task<Ulid> CreateUserAsync(UserCreationPayload payload);
    public Task<UserDTO?> GetUserByIdAsync(Ulid id);
    public Task<UserDTO?> GetCurrentUserAsync();
    public Task<UserDTO?> GetUserByUsernameOrEmailAsync(string usernameOrEmail);
    public Task<(IEnumerable<UserDTO>, int)> GetUsersAsync(Criteria<UserModel> criteria);
    public Task ChangeRoleAsync(Ulid id, UserRoles newRole);
    public Task UpdateUserPasswordAsync(Ulid id, string newPasswordHash);
    public Task UpdateUserEmailAsync(Ulid id, string newEmail);
    public Task BlockUserAsync(Ulid id);
    public Task UnblockUserAsync(Ulid id);
    public Task DeleteUserAsync(Ulid id);
}
