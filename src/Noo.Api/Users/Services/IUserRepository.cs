using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Users.DTO;
using Noo.Api.Users.Models;

namespace Noo.Api.Users.Services;

public interface IUserRepository : IRepository<UserModel>
{
    public Task<bool> ExistsByUsernameOrEmailAsync(string? username, string? email);
    public Task<UserDTO?> GetByUsernameOrEmailAsync(string usernameOrEmail, AutoMapper.IConfigurationProvider configurationProvider);
    public Task<bool> IsBlockedAsync(Ulid id);
    public Task BlockUserAsync(Ulid id);
    public Task UnblockUserAsync(Ulid id);
}
