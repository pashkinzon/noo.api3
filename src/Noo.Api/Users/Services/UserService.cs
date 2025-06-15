using AutoMapper;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Security.Authorization;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Users.DTO;
using Noo.Api.Users.Models;
using Noo.Api.Users.Types;

namespace Noo.Api.Users.Services;

[RegisterScoped(typeof(IUserService))]
public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly UserSearchStrategy _userSearchStrategy;

    public UserService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        UserSearchStrategy userSearchStrategy
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userSearchStrategy = userSearchStrategy;
    }

    public async Task BlockUserAsync(Ulid id)
    {
        await _unitOfWork
            .UserRepository()
            .BlockUserAsync(id);
    }

    public async Task ChangeRoleAsync(Ulid id, UserRoles newRole)
    {
        var user = await _unitOfWork
            .UserRepository()
            .GetByIdAsync(id);

        if (user is null)
        {
            throw new NotFoundException();
        }

        if (user.IsBlocked)
        {
            throw new UserIsBlockedException();
        }

        if (user.Role != UserRoles.Student)
        {
            throw new CantChangeRoleException();
        }

        user.Role = newRole;

        _unitOfWork.UserRepository().Update(user);
        await _unitOfWork.CommitAsync();
    }

    public async Task<Ulid> CreateUserAsync(UserCreationPayload payload)
    {
        var model = _mapper.Map<UserModel>(payload);

        _unitOfWork.UserRepository().Add(model);
        await _unitOfWork.CommitAsync();

        return model.Id;
    }

    public async Task DeleteUserAsync(Ulid id)
    {
        _unitOfWork.UserRepository().DeleteById(id);
        await _unitOfWork.CommitAsync();
    }

    public Task<UserDTO?> GetUserByIdAsync(Ulid id)
    {
        return _unitOfWork
            .UserRepository()
            .GetByIdAsync<UserDTO>(id, _mapper.ConfigurationProvider);
    }

    public Task<UserDTO?> GetUserByUsernameOrEmailAsync(string usernameOrEmail)
    {
        return _unitOfWork
            .UserRepository()
            .GetByUsernameOrEmailAsync(usernameOrEmail, _mapper.ConfigurationProvider);
    }

    public async Task<(IEnumerable<UserDTO>, int)> GetUsersAsync(Criteria<UserModel> criteria)
    {
        var (items, total) = await _unitOfWork
            .UserRepository()
            .SearchAsync<UserDTO>(criteria, _userSearchStrategy, _mapper.ConfigurationProvider);

        return (items, total);
    }

    public Task<bool> IsBlockedAsync(Ulid id)
    {
        return _unitOfWork
            .UserRepository()
            .IsBlockedAsync(id);
    }

    public Task UnblockUserAsync(Ulid id)
    {
        return _unitOfWork
            .UserRepository()
            .UnblockUserAsync(id);
    }

    public Task UpdateAvatarAsync(Ulid userId, UpdateAvatarDTO updateAvatarDTO)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateTelegramAsync(Ulid id, UpdateTelegramDTO updateTelegramDTO)
    {
        var user = await _unitOfWork
            .UserRepository()
            .GetByIdAsync(id);

        if (user is null)
        {
            throw new NotFoundException();
        }

        if (user.IsBlocked)
        {
            throw new UserIsBlockedException();
        }

        // TODO: add telegram service, validate payload and enable notifications

        user.TelegramId = updateTelegramDTO.TelegramId;
        user.TelegramUsername = updateTelegramDTO.TelegramUsername;

        _unitOfWork.UserRepository().Update(user);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateUserEmailAsync(Ulid id, string newEmail)
    {
        var user = await _unitOfWork
            .UserRepository()
            .GetByIdAsync(id);

        if (user is null)
        {
            throw new NotFoundException();
        }

        user.Email = newEmail;

        _unitOfWork.UserRepository().Update(user);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateUserPasswordAsync(Ulid id, string newPasswordHash)
    {
        var user = await _unitOfWork
            .UserRepository()
            .GetByIdAsync(id);

        if (user is null)
        {
            throw new NotFoundException();
        }

        user.PasswordHash = newPasswordHash;

        _unitOfWork.UserRepository().Update(user);
        await _unitOfWork.CommitAsync();
    }

    public Task<bool> UserExistsAsync(string? username, string? email)
    {
        if (username is null && email is null)
        {
            throw new ArgumentException("Username or email must be provided");
        }

        return _unitOfWork
            .UserRepository()
            .ExistsByUsernameOrEmailAsync(username, email);
    }

    public async Task VerifyUserAsync(Ulid id)
    {
        var user = await _unitOfWork
            .UserRepository()
            .GetByIdAsync(id);

        if (user is null)
        {
            throw new NotFoundException();
        }

        if (user.IsBlocked)
        {
            throw new UserIsBlockedException();
        }

        user.IsVerified = true;

        _unitOfWork.UserRepository().Update(user);
        await _unitOfWork.CommitAsync();
    }
}
