using Microsoft.AspNetCore.Mvc.ModelBinding;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Subjects.DTO;
using Noo.Api.Subjects.Models;
using SystemTextJsonPatch;

namespace Noo.Api.Subjects.Services;

public interface ISubjectService
{
    public Task<SubjectDTO?> GetSubjectByIdAsync(Ulid id);
    public Task<(IEnumerable<SubjectDTO>, int)> GetSubjectsAsync(Criteria<SubjectModel> criteria);
    public Task<Ulid> CreateSubjectAsync(SubjectCreationDTO subject);
    public Task UpdateSubjectAsync(Ulid id, JsonPatchDocument<SubjectUpdateDTO> subject, ModelStateDictionary? modelState = null);
    public Task DeleteSubjectAsync(Ulid id);
}
