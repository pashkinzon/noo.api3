using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Snippets.Models;

namespace Noo.Api.Snippets.Services;

public class SnippetRepository : Repository<SnippetModel>, ISnippetRepository
{
    public Task<SnippetModel?> GetAsync(Ulid snippetId, Ulid userId)
    {
        return Context.GetDbSet<SnippetModel>()
            .Where(x => x.Id == snippetId && x.UserId == userId)
            .FirstOrDefaultAsync();
    }
}


public static class IUnitOfWorkSnippetRepositoryExtensions
{
    public static ISnippetRepository SnippetRepository(this IUnitOfWork unitOfWork)
    {
        return new SnippetRepository()
        {
            Context = unitOfWork.Context
        };
    }
}
