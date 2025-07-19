using SystemTextJsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Noo.Api.Works.DTO;
using Noo.Api.Works.Models;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Works.Filters;

namespace Noo.Api.Works.Services;

public interface IWorkService
{
    public Task<WorkModel?> GetWorkAsync(Ulid id);

    public Task<SearchResult<WorkModel>> GetWorksAsync(WorkFilter filter);

    public Task<Ulid> CreateWorkAsync(CreateWorkDTO work);

    public Task UpdateWorkAsync(Ulid id, JsonPatchDocument<UpdateWorkDTO> workUpdatePayload, ModelStateDictionary? modelState = null);

    public Task DeleteWorkAsync(Ulid id);
}
