using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Core.Utils.Json;
using Noo.Api.Subjects.DTO;
using Noo.Api.Subjects.Models;
using SystemTextJsonPatch;

namespace Noo.Api.Subjects.Services;

[RegisterScoped(typeof(ISubjectService))]
public class SubjectService : ISubjectService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ISearchStrategy<SubjectModel> _searchStrategy;

    private readonly IMapper _mapper;

    public SubjectService(IUnitOfWork unitOfWork, SubjectSearchStrategy searchStrategy, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _searchStrategy = searchStrategy;
        _mapper = mapper;
    }

    public async Task<Ulid> CreateSubjectAsync(SubjectCreationDTO subject)
    {
        var subjectModel = _mapper.Map<SubjectModel>(subject);
        var repository = _unitOfWork.SubjectRepository();

        repository.Add(subjectModel);
        await _unitOfWork.CommitAsync();

        return subjectModel.Id;
    }

    public async Task DeleteSubjectAsync(Ulid id)
    {
        var repository = _unitOfWork.SubjectRepository();
        repository.DeleteById(id);
        await _unitOfWork.CommitAsync();
    }

    public Task<SubjectDTO?> GetSubjectByIdAsync(Ulid id)
    {
        var repository = _unitOfWork.SubjectRepository();
        return repository.GetByIdAsync<SubjectDTO>(id, _mapper.ConfigurationProvider);
    }

    public async Task<(IEnumerable<SubjectDTO>, int)> GetSubjectsAsync(Criteria<SubjectModel> criteria)
    {
        var repository = _unitOfWork.SubjectRepository();
        var (items, total) = await repository.SearchAsync<SubjectDTO>(
            criteria,
            _searchStrategy,
            _mapper.ConfigurationProvider
        );

        return (items, total);
    }

    public async Task UpdateSubjectAsync(Ulid id, JsonPatchDocument<SubjectUpdateDTO> subject, ModelStateDictionary? modelState = null)
    {
        var repository = _unitOfWork.SubjectRepository();
        var model = await repository.GetByIdAsync(id);

        if (model == null)
        {
            throw new NotFoundException();
        }

        var dto = _mapper.Map<SubjectUpdateDTO>(model);

        modelState ??= new ModelStateDictionary();

        subject.ApplyToAndValidate(dto, modelState);

        if (!modelState.IsValid)
        {
            throw new BadRequestException();
        }

        _mapper.Map(dto, model);

        repository.Update(model);

        await _unitOfWork.CommitAsync();
    }
}
