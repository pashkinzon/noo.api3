namespace Noo.Api.Core.DataAbstraction.Db;

public interface IUnitOfWork : IDisposable
{
    public NooDbContext Context { get; }


    public Task<int> CommitAsync(CancellationToken cancellationToken = default);

    public void Rollback();
}
