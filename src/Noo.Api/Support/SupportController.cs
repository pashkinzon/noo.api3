using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.Support.DTO;
using Noo.Api.Support.Services;
using SystemTextJsonPatch;
using ProducesAttribute = Noo.Api.Core.Documentation.ProducesAttribute;

namespace Noo.Api.Support;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("support")]
public class SupportController : ApiController
{
    private readonly ISupportService _supportService;

    public SupportController(ISupportService supportService)
    {
        _supportService = supportService;
    }

    /// <summary>
    /// Retrieves a tree of support categories.
    /// </summary>
    [HttpGet]
    [MapToApiVersion(NooApiVersions.Current)]
    [AllowAnonymous]
    [Produces(
        typeof(ApiResponseDTO<IEnumerable<SupportCategoryDTO>>), StatusCodes.Status200OK
    )]
    public async Task<IActionResult> GetTreeAsync()
    {
        var response = await _supportService.GetCategoryTreeAsync();
        return OkResponse(response);
    }

    /// <summary>
    /// Retrieves a support article by its ID.
    /// </summary>
    [HttpGet("article/{id}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [AllowAnonymous]
    [Produces(
        typeof(ApiResponseDTO<SupportArticleDTO>), StatusCodes.Status200OK,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> GetArticleAsync(Ulid id)
    {
        var response = await _supportService.GetArticleAsync(id);
        return OkResponse(response);
    }

    /// <summary>
    /// Creates a new support article.
    /// </summary>
    [HttpPost("article")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = SupportPolicies.CanCreateArticle)]
    [Produces(
        typeof(ApiResponseDTO<IdResponseDTO>), StatusCodes.Status201Created,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> CreateArticleAsync([FromBody] CreateSupportArticleDTO request)
    {
        var id = await _supportService.CreateArticleAsync(request);
        return CreatedResponse(id);
    }

    /// <summary>
    /// Updates a support article by its ID using a JSON Patch document.
    /// </summary>
    [HttpPatch("article/{id}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = SupportPolicies.CanUpdateArticle)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> UpdateArticleAsync([FromRoute] Ulid id, [FromBody] JsonPatchDocument<UpdateSupportArticleDTO> request)
    {
        await _supportService.UpdateArticleAsync(id, request);
        return NoContent();
    }

    /// <summary>
    /// Deletes a support article by its ID.
    /// </summary>
    [HttpDelete("article/{id}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = SupportPolicies.CanDeleteArticle)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> DeleteArticleAsync([FromRoute] Ulid id)
    {
        await _supportService.DeleteArticleAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Creates a new support category.
    /// </summary>
    [HttpPost("category")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = SupportPolicies.CanCreateCategory)]
    [Produces(
        typeof(ApiResponseDTO<IdResponseDTO>), StatusCodes.Status201Created,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateSupportCategoryDTO request)
    {
        var id = await _supportService.CreateCategoryAsync(request);
        return CreatedResponse(id);
    }

    /// <summary>
    /// Updates a support category by its ID using a JSON Patch document.
    /// </summary>
    [HttpPatch("category/{id}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = SupportPolicies.CanUpdateCategory)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> UpdateCategoryAsync([FromRoute] Ulid id, [FromBody] JsonPatchDocument<UpdateSupportCategoryDTO> request)
    {
        await _supportService.UpdateCategoryAsync(id, request);
        return NoContent();
    }

    /// <summary>
    /// Deletes a support category by its ID.
    /// </summary>
    [HttpDelete("category/{id}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = SupportPolicies.CanDeleteCategory)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> DeleteCategoryAsync([FromRoute] Ulid id)
    {
        await _supportService.DeleteCategoryAsync(id);
        return NoContent();
    }
}
