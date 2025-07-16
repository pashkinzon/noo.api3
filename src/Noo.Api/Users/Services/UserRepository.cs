using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Security.Authorization;
using Noo.Api.Users.DTO;
using Noo.Api.Users.Models;

namespace Noo.Api.Users.Services;

public class UserRepository : Repository<UserModel>, IUserRepository
{
    public Task<UserModel?> GetByUsernameOrEmailAsync(string usernameOrEmail)
    {
        var repository = Context.GetDbSet<UserModel>();

        return repository
            .Where(x => x.Username == usernameOrEmail || x.Email == usernameOrEmail)
            .FirstOrDefaultAsync();
    }

    public Task<bool> ExistsByUsernameOrEmailAsync(string? username, string? email)
    {
        var repository = Context.GetDbSet<UserModel>();

        if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(email))
        {
            return Task.FromResult(false);
        }

        if (string.IsNullOrEmpty(username))
        {
            return repository.AnyAsync(x => x.Email == email);
        }

        if (string.IsNullOrEmpty(email))
        {
            return repository.AnyAsync(x => x.Username == username);
        }

        return repository.AnyAsync(x => x.Username == username || x.Email == email);
    }

    public Task<bool> IsBlockedAsync(Ulid id)
    {
        var repository = Context.GetDbSet<UserModel>();

        return repository
            .Where(x => x.Id == id)
            .Select(x => x.IsBlocked)
            .FirstOrDefaultAsync();
    }

    public async Task BlockUserAsync(Ulid id)
    {
        var repository = Context.GetDbSet<UserModel>();

        await repository
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => x.SetProperty(y => y.IsBlocked, true));
    }

    public async Task UnblockUserAsync(Ulid id)
    {
        var repository = Context.GetDbSet<UserModel>();

        await repository
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => x.SetProperty(y => y.IsBlocked, false));
    }

    public Task<bool> MentorExistsAsync(Ulid mentorId)
    {
        var repository = Context.GetDbSet<UserModel>();

        return repository
            .Where(x => x.Id == mentorId && x.Role == UserRoles.Mentor)
            .AnyAsync();
    }
}

public static class IUnitOfWorkUserRepositoryExtensions
{
    public static IUserRepository UserRepository(this IUnitOfWork unitOfWork)
    {
        return new UserRepository()
        {
            Context = unitOfWork.Context
        };
    }
}
