using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Subjects.DTO;
using Noo.Api.Subjects.Filters;
using Noo.Api.Subjects.Models;
using SystemTextJsonPatch;

namespace Noo.Api.Subjects.Services;

[RegisterScoped(typeof(ISubjectService))]
public class SubjectService : ISubjectService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ISubjectRepository _subjectRepository;

    private readonly IMapper _mapper;

    public SubjectService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _subjectRepository = unitOfWork.SubjectRepository();
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

    public Task<SearchResult<SubjectModel>> GetSubjectsAsync(SubjectFilter filter)
    {
        return _subjectRepository.SearchAsync(filter);
    }

    public async Task UpdateSubjectAsync(Ulid id, JsonPatchDocument<SubjectUpdateDTO> updateSubjectDto, ModelStateDictionary? modelState = null)
    {
        await _subjectRepository.UpdateWithJsonPatchAsync(id, updateSubjectDto, _mapper, modelState);

        await _unitOfWork.CommitAsync();
    }
}
