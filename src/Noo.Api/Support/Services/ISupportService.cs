using Noo.Api.Support.DTO;
using SystemTextJsonPatch;

namespace Noo.Api.Support.Services;

public interface ISupportService
{
    public Task<Ulid> CreateCategoryAsync(CreateSupportCategoryDTO dto);
    public Task<SupportCategoryDTO> UpdateCategoryAsync(Ulid categoryId, JsonPatchDocument<UpdateSupportCategoryDTO> dto);
    public Task DeleteCategoryAsync(Ulid categoryId);
    public Task<Ulid> CreateArticleAsync(CreateSupportArticleDTO dto);
    public Task<SupportArticleDTO> UpdateArticleAsync(Ulid articleId, JsonPatchDocument<UpdateSupportArticleDTO> dto);
    public Task DeleteArticleAsync(Ulid articleId);
    public Task<IEnumerable<SupportCategoryDTO>> GetCategoryTreeAsync();
    public Task<IEnumerable<SupportArticleDTO>> GetArticleAsync(Ulid articleId);
}
