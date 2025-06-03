using AutoMapper;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Support.DTO;
using Noo.Api.Support.Models;
using SystemTextJsonPatch;

namespace Noo.Api.Support.Services;

[RegisterScoped(typeof(ISupportService))]
public class SupportService : ISupportService
{
    private readonly ISupportArticleRepository _articleRepository;
    private readonly ISupportCategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SupportService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _articleRepository = unitOfWork.SupportArticleRepository();
        _categoryRepository = unitOfWork.SupportCategoryRepository();
        _mapper = mapper;
    }

    public async Task<Ulid> CreateArticleAsync(CreateSupportArticleDTO dto)
    {
        var model = _mapper.Map<SupportArticleModel>(dto);

        _articleRepository.Add(model);
        await _unitOfWork.CommitAsync();

        return model.Id;
    }

    public async Task<Ulid> CreateCategoryAsync(CreateSupportCategoryDTO dto)
    {
        var model = _mapper.Map<SupportCategoryModel>(dto);

        _categoryRepository.Add(model);
        await _unitOfWork.CommitAsync();

        return model.Id;
    }

    public async Task DeleteArticleAsync(Ulid articleId)
    {
        _articleRepository.DeleteById(articleId);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteCategoryAsync(Ulid categoryId)
    {
        _categoryRepository.DeleteById(categoryId);
        await _unitOfWork.CommitAsync();
    }

    public Task<SupportArticleDTO?> GetArticleAsync(Ulid articleId)
    {
        return _articleRepository
            .GetByIdAsync<SupportArticleDTO>(articleId, _mapper.ConfigurationProvider);
    }

    public async Task<IEnumerable<SupportCategoryDTO>> GetCategoryTreeAsync()
    {
        var categories = await _categoryRepository.GetCategoryTreeAsync(includeInactive: false);
        return _mapper.Map<IEnumerable<SupportCategoryDTO>>(categories);
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
