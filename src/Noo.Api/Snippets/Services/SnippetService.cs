using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.DataAbstraction.Criteria.Filters;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Core.Utils.Json;
using Noo.Api.Snippets.DTO;
using Noo.Api.Snippets.Models;
using SystemTextJsonPatch;

namespace Noo.Api.Snippets.Services;

[RegisterScoped(typeof(ISnippetService))]
public class SnippetService : ISnippetService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ISnippetRepository _snippetRepository;

    private readonly IMapper _mapper;

    public SnippetService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _snippetRepository = unitOfWork.SnippetRepository();
        _mapper = mapper;
    }

    public async Task CreateSnippetAsync(Ulid userId, CreateSnippetDTO createSnippetDto)
    {
        var model = _mapper.Map<SnippetModel>(createSnippetDto);
        model.UserId = userId;

        _snippetRepository.Add(model);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteSnippetAsync(Ulid userId, Ulid snippetId)
    {
        var snippet = await _unitOfWork.SnippetRepository()
            .GetAsync(snippetId, userId);

        if (snippet == null)
        {
            throw new NotFoundException();
        }

        _snippetRepository.Delete(snippet);
        await _unitOfWork.CommitAsync();
    }

    public Task<SearchResult<SnippetModel>> GetSnippetsAsync(Ulid userId)
    {
        var criteria = new Criteria<SnippetModel>();

        criteria.AddFilter("UserId", FilterType.Equals, userId);
        criteria.Page = 1;
        criteria.Limit = SnippetConfig.MaxSnippetsPerUser;

        return _snippetRepository.GetManyAsync(criteria);
    }

    public async Task UpdateSnippetAsync(Ulid userId, Ulid snippetId, JsonPatchDocument<UpdateSnippetDTO> updateSnippetDto, ModelStateDictionary? modelState = null)
    {
        var repository = _unitOfWork.SnippetRepository();
        var model = await repository.GetByIdAsync(snippetId) ?? throw new NotFoundException();

        if (model == null || model.UserId != userId)
        {
            throw new NotFoundException();
        }

        var dto = _mapper.Map<UpdateSnippetDTO>(model);

        modelState ??= new ModelStateDictionary();

        updateSnippetDto.ApplyToAndValidate(dto, modelState);

        if (!modelState.IsValid)
        {
            throw new BadRequestException();
        }

        _mapper.Map(dto, model);

        repository.Update(model);
        await _unitOfWork.CommitAsync();
    }
}
