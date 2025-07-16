using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.GoogleSheetsIntegrations.DTO;
using Noo.Api.GoogleSheetsIntegrations.Models;
using Noo.Api.GoogleSheetsIntegrations.Services;
using ProducesAttribute = Noo.Api.Core.Documentation.ProducesAttribute;

namespace Noo.Api.GoogleSheetsIntegrations;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("google-sheets")]
public class GoogleSheetsIntegrationController : ApiController
{
    private readonly IGoogleSheetsIntegrationService _googleSheetsIntegrationService;

    public GoogleSheetsIntegrationController(IGoogleSheetsIntegrationService googleSheetsIntegrationService)
    {
        _googleSheetsIntegrationService = googleSheetsIntegrationService;
    }

    /// <summary>
    /// Retrieves a list of Google Sheets integrations based on the provided criteria.
    /// </summary>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpGet]
    [Authorize(Policy = GoogleSheetsIntegrationPolicies.CanGetGoogleSheetsIntegrations)]
    [Produces(
        typeof(ApiResponseDTO<IEnumerable<GoogleSheetsIntegrationDTO>>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> GetIntegrationsAsync(
        [FromQuery] Criteria<GoogleSheetsIntegrationModel> criteria
    )
    {
        var result = await _googleSheetsIntegrationService.GetIntegrationsAsync(criteria);
        return OkResponse(result);
    }

    /// <summary>
    /// Creates a new Google Sheets integration.
    /// </summary>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpPost]
    [Authorize(Policy = GoogleSheetsIntegrationPolicies.CanCreateGoogleSheetsIntegration)]
    [Produces(
        typeof(ApiResponseDTO<IdResponseDTO>), StatusCodes.Status201Created,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> CreateIntegrationAsync([FromBody] CreateGoogleSheetsIntegrationDTO request)
    {
        var integrationId = await _googleSheetsIntegrationService.CreateIntegrationAsync(request);

        return CreatedResponse(integrationId);
    }

    /// <summary>
    /// Runs a Google Sheets integration by its ID.
    /// </summary>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpPost("{integrationId}/run")]
    [Authorize(Policy = GoogleSheetsIntegrationPolicies.CanRunGoogleSheetsIntegration)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> RunIntegrationAsync([FromRoute] Ulid integrationId)
    {
        await _googleSheetsIntegrationService.RunIntegrationAsync(integrationId);
        return NoContent();
    }

    /// <summary>
    /// Deletes a Google Sheets integration by its ID.
    /// </summary>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpDelete("{integrationId}")]
    [Authorize(Policy = GoogleSheetsIntegrationPolicies.CanDeleteGoogleSheetsIntegration)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> DeleteIntegrationAsync([FromRoute] Ulid integrationId)
    {
        await _googleSheetsIntegrationService.DeleteIntegrationAsync(integrationId);
        return NoContent();
    }
}
