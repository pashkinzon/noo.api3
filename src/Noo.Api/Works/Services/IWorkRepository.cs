using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Works.Models;

namespace Noo.Api.Works.Services;

public interface IWorkRepository : IRepository<WorkModel>
{
    public Task<WorkModel?> GetWithTasksAsync(Ulid id);
}
