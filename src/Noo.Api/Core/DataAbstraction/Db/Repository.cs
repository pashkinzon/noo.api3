using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using AutoFilterer.Abstractions;
using AutoFilterer.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Utils.Json;
using SystemTextJsonPatch;

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
        var set = Context.GetDbSet<T>();
        // If an entity with the same key is already tracked, remove that instance to avoid tracking conflicts
        var entity = set.Local.FirstOrDefault(e => e.Id == id) ?? set.Find(id);
        if (entity != null)
        {
            set.Remove(entity);
            return;
        }

        // Otherwise, attach a stub entity and remove it
        var stub = new T { Id = id };
        set.Attach(stub);
        set.Remove(stub);
    }

    public async Task<SearchResult<T>> SearchAsync(IPaginationFilter filter, IEnumerable<ISpecification<T>>? specifications = default)
    {
        var query = Context.GetDbSet<T>().AsQueryable();

        if (specifications != null)
        {
            foreach (var spec in specifications)
            {
                query = query.WithSpecification(spec);
            }
        }

        var total = await query
            .ApplyFilterWithoutPagination(filter)
            .CountAsync();

        var results = await query
            .ApplyFilter(filter)
            .ToListAsync();

        return new SearchResult<T>(results, total);
    }

    public async Task<SearchResult<T>> GetManyAsync(IPaginationFilter filter)
    {
        var query = Context.GetDbSet<T>().AsQueryable();

        var total = await query
            .ApplyFilterWithoutPagination(filter)
            .CountAsync();

        var results = await query
            .ApplyFilter(filter)
            .ToListAsync();

        return new SearchResult<T>(results, total);
    }

    public async Task UpdateWithJsonPatchAsync<TDto>(Ulid id, JsonPatchDocument<TDto> updateDto, IMapper mapper, ModelStateDictionary? modelState = null) where TDto : class
    {
        var model = await GetByIdAsync(id) ?? throw new NotFoundException();

        if (model == null)
        {
            throw new NotFoundException();
        }

        var dto = mapper.Map<TDto>(model);

        modelState ??= new ModelStateDictionary();

        updateDto.ApplyToAndValidate(dto, modelState);

        if (!modelState.IsValid)
        {
            throw new BadRequestException();
        }

        mapper.Map(dto, model);
    }
}
