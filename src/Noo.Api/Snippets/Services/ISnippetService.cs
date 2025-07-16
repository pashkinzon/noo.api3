using Microsoft.AspNetCore.Mvc.ModelBinding;
using Noo.Api.Snippets.DTO;
using SystemTextJsonPatch;

namespace Noo.Api.Snippets.Services;

public interface ISnippetService
{
    public Task CreateSnippetAsync(Ulid userId, CreateSnippetDTO createSnippetDto);
    public Task UpdateSnippetAsync(Ulid userId, Ulid snippetId, JsonPatchDocument<UpdateSnippetDTO> updateSnippetDto, ModelStateDictionary? modelState = null);
    public Task DeleteSnippetAsync(Ulid userId, Ulid snippetId);
    public Task<(IEnumerable<SnippetDTO>, int)> GetSnippetsAsync(Ulid userId);
}
