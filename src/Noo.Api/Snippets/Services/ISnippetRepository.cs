using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Snippets.Models;

namespace Noo.Api.Snippets.Services;

public interface ISnippetRepository : IRepository<SnippetModel>
{
    public Task<SnippetModel?> GetAsync(Ulid snippetId, Ulid userId);
}
