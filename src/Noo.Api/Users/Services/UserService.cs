using AutoMapper;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Security;
using Noo.Api.Core.Security.Authorization;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Users.DTO;
using Noo.Api.Users.Models;

namespace Noo.Api.Users.Services;

[RegisterScoped(typeof(IUserService))]
public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IHashService _hashService;

    private readonly IMapper _mapper;

    public UserService(
        IUnitOfWork unitOfWork,
        IHashService hashService,
        IMapper mapper
    )
    {
        _unitOfWork = unitOfWork;
        _hashService = hashService;
        _mapper = mapper;
    }

    public Task BlockUserAsync(Ulid id)
    {
        throw new NotImplementedException();
    }

    public Task ChangeRoleAsync(Ulid id, UserRoles newRole)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO> CreateUserAsync(UserCreationPayload payload)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(Ulid id)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO?> GetCurrentUserAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO?> GetUserByIdAsync(Ulid id)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO?> GetUserByUsernameOrEmailAsync(string usernameOrEmail)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<UserDTO>, int)> GetUsersAsync(Criteria<UserModel> criteria)
    {
        throw new NotImplementedException();
    }

    public Task UnblockUserAsync(Ulid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserEmailAsync(Ulid id, string newEmail)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserPasswordAsync(Ulid id, string newPasswordHash)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UserExistsAsync(string? username, string? email)
    {
        throw new NotImplementedException();
    }

    Task<Ulid> IUserService.CreateUserAsync(UserCreationPayload payload)
    {
        throw new NotImplementedException();
    }
}
