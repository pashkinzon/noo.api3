using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.Utils.DI;

namespace Noo.Api.Core.DataAbstraction.Db;

[RegisterScoped(typeof(IUnitOfWork))]
public class UnitOfWork : IUnitOfWork
{
    public NooDbContext Context { get; init; }

    public UnitOfWork(NooDbContext context)
    {
        Context = context;
    }

    public Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return Context.SaveChangesAsync(cancellationToken);
    }

    public void Rollback()
    {
        foreach (var entry in Context.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Modified:
                case EntityState.Deleted:
                    entry.Reload();
                    break;
            }
        }
    }

    public void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }
}
