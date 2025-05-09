using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.Utils.DI;

namespace Noo.Api.Core.DataAbstraction.Db;

[RegisterScoped(typeof(IUnitOfWork))]
public class UnitOfWork : IUnitOfWork
{
    private readonly NooDbContext _context;

    private readonly Dictionary<Type, object> _repositories = [];

    public UnitOfWork(NooDbContext context)
    {
        _context = context;
    }

    public IRepository<T> GetRepository<T>() where T : BaseModel, new()
    {
        if (_repositories.TryGetValue(typeof(T), out var repository))
        {
            return (IRepository<T>)repository;
        }

        IRepository<T> newRepository = new Repository<T>(_context);
        _repositories.Add(typeof(T), newRepository);

        return newRepository;
    }

    public TRepository GetSpecificRepository<TRepository, TModel>() where TRepository : IRepository<TModel>, new() where TModel : BaseModel
    {
        var repository = new TRepository();

        repository.SetContext(_context);

        return repository;
    }

    public Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public void Rollback()
    {
        foreach (var entry in _context.ChangeTracker.Entries())
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
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
