using AutoMapper;
using SystemTextJsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Core.Utils.Json;
using Noo.Api.Works.DTO;
using Noo.Api.Works.Models;

namespace Noo.Api.Works.Services;

[RegisterScoped(typeof(IWorkService))]
public class WorkService : IWorkService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ISearchStrategy<WorkModel> _searchStrategy;

    private readonly IMapper _mapper;

    public WorkService(IUnitOfWork unitOfWork, IMapper mapper, WorkSearchStrategy searchStrategy)
    {
        _unitOfWork = unitOfWork;
        _searchStrategy = searchStrategy;
        _mapper = mapper;
    }

    public async Task<Ulid> CreateWorkAsync(CreateWorkDTO work)
    {
        var model = _mapper.Map<WorkModel>(work);

        _unitOfWork.WorkRepository().Add(model);
        await _unitOfWork.CommitAsync();

        return model.Id;
    }

    public async Task<WorkDTO?> GetWorkAsync(Ulid id)
    {
        var model = await _unitOfWork.WorkRepository().GetWithTasksAsync(id);

        return _mapper.Map<WorkDTO?>(model);
    }

    public async Task<(IEnumerable<WorkDTO>, int)> GetWorksAsync(Criteria<WorkModel> criteria)
    {
        var (items, total) = await _unitOfWork.WorkRepository().SearchAsync<WorkDTO>(criteria, _searchStrategy, _mapper.ConfigurationProvider);

        return (items, total);
    }

    public async Task UpdateWorkAsync(Ulid id, JsonPatchDocument<UpdateWorkDTO> updateWorkDto, ModelStateDictionary? modelState = null)
    {
        var repository = _unitOfWork.WorkRepository();
        var model = await repository.GetByIdAsync(id) ?? throw new NotFoundException();

        if (model == null)
        {
            throw new NotFoundException();
        }

        var dto = _mapper.Map<UpdateWorkDTO>(model);

        modelState ??= new ModelStateDictionary();

        updateWorkDto.ApplyToAndValidate(dto, modelState);

        if (!modelState.IsValid)
        {
            throw new BadRequestException();
        }

        _mapper.Map(dto, model);

        repository.Update(model);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteWorkAsync(Ulid id)
    {
        _unitOfWork.WorkRepository().DeleteById(id);

        await _unitOfWork.CommitAsync();
    }
}
