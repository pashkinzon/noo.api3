using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Request;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Works.DTO;
using Noo.Api.Works.Models;
using SystemTextJsonPatch;

namespace Noo.Api.Works.Services;

[RegisterScoped(typeof(IWorkService))]
public class WorkService : IWorkService
{
    protected readonly IUnitOfWork UnitOfWork;

    protected readonly ISearchStrategy<WorkModel> SearchStrategy;

    protected readonly IMapper Mapper;

    public WorkService(IUnitOfWork unitOfWork, IMapper mapper, WorkSearchStrategy searchStrategy)
    {
        UnitOfWork = unitOfWork;
        SearchStrategy = searchStrategy;
        Mapper = mapper;
    }

    public async Task<Ulid> CreateWorkAsync(CreateWorkDTO work)
    {
        var model = Mapper.Map<WorkModel>(work);

        await UnitOfWork.GetRepository<WorkModel>().AddAsync(model);
        await UnitOfWork.CommitAsync();

        return model.Id;
    }

    public async Task<WorkResponseDTO?> GetWorkAsync(Ulid id)
    {
        var repository = UnitOfWork.GetSpecificRepository<WorkRepository, WorkModel>();

        var model = await repository.GetWithTasksAsync(id);

        return Mapper.Map<WorkResponseDTO?>(model);
    }

    public async Task<(IEnumerable<WorkResponseDTO>, int)> GetWorksAsync(Criteria<WorkModel> criteria)
    {
        var repository = UnitOfWork.GetRepository<WorkModel>();
        var (items, total) = await repository.SearchAsync<WorkResponseDTO>(criteria, SearchStrategy, Mapper.ConfigurationProvider);

        return (items, total);
    }

    public async Task UpdateWorkAsync(Ulid id, JsonPatchDocument<UpdateWorkDTO> workUpdatePayload, ModelStateDictionary? modelState = null)
    {
        var repository = UnitOfWork.GetRepository<WorkModel>();
        var model = await repository.GetByIdAsync(id) ?? throw new NotFoundException("Work not found");
        var dto = Mapper.Map<UpdateWorkDTO>(model);

        modelState ??= new ModelStateDictionary();

        workUpdatePayload.ApplyTo(dto, (error) =>
            modelState.AddModelError(error.Operation.ToString() ?? "Unknown operation", error.ErrorMessage)
        );

        if (!modelState.IsValid)
        {
            return;
        }

        Mapper.Map(dto, model);

        repository.Update(model);
        await UnitOfWork.CommitAsync();
    }

    public async Task DeleteWorkAsync(Ulid id)
    {
        var repository = UnitOfWork.GetRepository<WorkModel>();
        repository.DeleteById(id);

        await UnitOfWork.CommitAsync();
    }
}
