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

    private readonly ISubjectRepository _subjectRepository;

    private readonly ISearchStrategy<SubjectModel> _searchStrategy;

    private readonly IMapper _mapper;

    public SubjectService(IUnitOfWork unitOfWork, SubjectSearchStrategy searchStrategy, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _subjectRepository = unitOfWork.SubjectRepository();
        _searchStrategy = searchStrategy;
        _mapper = mapper;
    }

    public async Task<Ulid> CreateSubjectAsync(SubjectCreationDTO subject)
    {
        var subjectModel = _mapper.Map<SubjectModel>(subject);

        _subjectRepository.Add(subjectModel);
        await _unitOfWork.CommitAsync();

        return subjectModel.Id;
    }

    public async Task DeleteSubjectAsync(Ulid id)
    {
        _subjectRepository.DeleteById(id);
        await _unitOfWork.CommitAsync();
    }

    public Task<SubjectModel?> GetSubjectByIdAsync(Ulid id)
    {
        return _subjectRepository.GetByIdAsync(id);
    }

    public Task<SearchResult<SubjectModel>> GetSubjectsAsync(Criteria<SubjectModel> criteria)
    {
        return _subjectRepository.SearchAsync(criteria, _searchStrategy);
    }

    public async Task UpdateSubjectAsync(Ulid id, JsonPatchDocument<SubjectUpdateDTO> subject, ModelStateDictionary? modelState = null)
    {
        var model = await _subjectRepository.GetByIdAsync(id);

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

        _subjectRepository.Update(model);

        await _unitOfWork.CommitAsync();
    }
}
