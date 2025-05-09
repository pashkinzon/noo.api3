using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Works.Models;

namespace Noo.Api.Works;

public class WorkRepository : Repository<WorkModel>
{
    public Task<WorkModel?> GetWithTasksAsync(Ulid id)
    {
        var repository = Context.GetDbSet<WorkModel>();

        return repository
            .Include(x => x.Tasks)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
