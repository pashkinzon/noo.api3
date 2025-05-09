using Noo.Api.Core.DataAbstraction.Model;

namespace Noo.Api.Core.DataAbstraction.Db;

public interface IUnitOfWork : IDisposable
{
    public IRepository<T> GetRepository<T>() where T : BaseModel, new();

    public TRepository GetSpecificRepository<TRepository, TModel>() where TRepository : IRepository<TModel>, new() where TModel : BaseModel;

    public Task<int> CommitAsync(CancellationToken cancellationToken = default);

    public void Rollback();
}
