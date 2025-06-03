using Noo.Api.Core.Utils.DI;
using Noo.Api.Support.DTO;
using SystemTextJsonPatch;

namespace Noo.Api.Support.Services;

[RegisterScoped(typeof(ISupportService))]
public class SupportService : ISupportService
{
    public Task<Ulid> CreateArticleAsync(CreateSupportArticleDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<Ulid> CreateCategoryAsync(CreateSupportCategoryDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteArticleAsync(Ulid articleId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCategoryAsync(Ulid categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SupportArticleDTO>> GetArticleAsync(Ulid articleId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SupportCategoryDTO>> GetCategoryTreeAsync()
    {
        throw new NotImplementedException();
    }

    public Task<SupportArticleDTO> UpdateArticleAsync(Ulid articleId, JsonPatchDocument<UpdateSupportArticleDTO> dto)
    {
        throw new NotImplementedException();
    }

    public Task<SupportCategoryDTO> UpdateCategoryAsync(Ulid categoryId, JsonPatchDocument<UpdateSupportCategoryDTO> dto)
    {
        throw new NotImplementedException();
    }
}
