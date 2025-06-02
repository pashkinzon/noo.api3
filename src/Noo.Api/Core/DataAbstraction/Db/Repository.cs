using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.DataAbstraction.Model;

namespace Noo.Api.Core.DataAbstraction.Db;

public class Repository<T> : IRepository<T> where T : BaseModel, new()
{
    public NooDbContext Context { get; init; }

    public Repository(NooDbContext? context = null)
    {
        Context = context!;
    }

    public Task<T?> GetByIdAsync(Ulid id)
    {
        return Context.GetDbSet<T>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public Task<TDTO?> GetByIdAsync<TDTO>(Ulid id, AutoMapper.IConfigurationProvider configurationProvider) where TDTO : class
    {
        return Context.GetDbSet<T>()
            .Where(e => e.Id == id)
            .ProjectTo<TDTO>(configurationProvider)
            .FirstOrDefaultAsync();
    }

    public void Add(T entity)
    {
        Context.GetDbSet<T>().Add(entity);
    }

    public Task<bool> ExistsAsync(Ulid id)
    {
        return Context.GetDbSet<T>().AnyAsync(e => e.Id == id);
    }

    public void Update(T entity)
    {
        Context.GetDbSet<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        Context.GetDbSet<T>().Remove(entity);
    }

    public void DeleteById(Ulid id)
    {
        Context.GetDbSet<T>().Remove(new() { Id = id });
    }

    public async Task<SearchResult<T>> SearchAsync(Criteria<T> criteria, ISearchStrategy<T> searchStrategy)
    {
        var query = Context.GetDbSet<T>().AsQueryable();

        var total = await query
            .AddCountingCriteria(criteria, searchStrategy)
            .CountAsync();

        var results = await query
            .AddCriteria(criteria, searchStrategy)
            .ToListAsync();

        return new SearchResult<T>(results, total);
    }

    public async Task<SearchResult<TDTO>> SearchAsync<TDTO>(Criteria<T> criteria, ISearchStrategy<T> searchStrategy, AutoMapper.IConfigurationProvider configurationProvider) where TDTO : class
    {
        var query = Context.GetDbSet<T>().AsQueryable();

        var total = await query
            .AddCountingCriteria(criteria, searchStrategy)
            .CountAsync();

        var results = await query
            .AddCriteria(criteria, searchStrategy)
            .ProjectTo<TDTO>(configurationProvider)
            .ToListAsync();

        return new SearchResult<TDTO>(results, total);
    }

    public async Task<SearchResult<T>> GetManyAsync(Criteria<T> criteria)
    {
        var query = Context.GetDbSet<T>().AsQueryable();

        var total = await query
            .AddCountingCriteria(criteria)
            .CountAsync();

        var results = await query
            .AddCriteria(criteria)
            .ToListAsync();

        return new SearchResult<T>(results, total);
    }

    public async Task<SearchResult<TDTO>> GetManyAsync<TDTO>(Criteria<T> criteria, AutoMapper.IConfigurationProvider configurationProvider) where TDTO : class
    {
        var query = Context.GetDbSet<T>().AsQueryable();

        var total = await query
            .AddCountingCriteria(criteria)
            .CountAsync();

        var results = await query
            .AddCriteria(criteria)
            .ProjectTo<TDTO>(configurationProvider)
            .ToListAsync();

        return new SearchResult<TDTO>(results, total);
    }
}
