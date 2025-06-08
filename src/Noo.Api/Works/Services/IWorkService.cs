using SystemTextJsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Works.DTO;
using Noo.Api.Works.Models;

namespace Noo.Api.Works.Services;

public interface IWorkService
{
    public Task<WorkDTO?> GetWorkAsync(Ulid id);

    public Task<(IEnumerable<WorkDTO>, int)> GetWorksAsync(Criteria<WorkModel> criteria);

    public Task<Ulid> CreateWorkAsync(CreateWorkDTO work);

    public Task UpdateWorkAsync(Ulid id, JsonPatchDocument<UpdateWorkDTO> workUpdatePayload, ModelStateDictionary? modelState = null);

    public Task DeleteWorkAsync(Ulid id);
}
