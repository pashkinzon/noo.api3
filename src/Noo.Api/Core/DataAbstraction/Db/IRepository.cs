using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.DataAbstraction.Model;

namespace Noo.Api.Core.DataAbstraction.Db;

public interface IRepository<T> where T : BaseModel
{
    public void SetContext(NooDbContext context);

    public Task<T?> GetByIdAsync(Ulid id);

    public Task<TDTO?> GetByIdAsync<TDTO>(Ulid id, AutoMapper.IConfigurationProvider configurationProvider) where TDTO : class;

    public Task<SearchResult<T>> SearchAsync(Criteria<T> criteria, ISearchStrategy<T> searchStrategy);

    public Task<SearchResult<TDTO>> SearchAsync<TDTO>(Criteria<T> criteria, ISearchStrategy<T> searchStrategy, AutoMapper.IConfigurationProvider configurationProvider) where TDTO : class;

    public Task<bool> ExistsAsync(Ulid id);

    public void Add(T entity);

    public void Update(T entity);

    public void Delete(T entity);

    public void DeleteById(Ulid id);
}
