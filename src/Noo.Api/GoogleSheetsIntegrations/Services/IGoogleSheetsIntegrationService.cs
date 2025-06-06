using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.GoogleSheetsIntegrations.DTO;
using Noo.Api.GoogleSheetsIntegrations.Models;

namespace Noo.Api.GoogleSheetsIntegrations.Services;

public interface IGoogleSheetsIntegrationService
{
    public Task<(IEnumerable<GoogleSheetsIntegrationDTO>, int)> GetIntegrationsAsync(Criteria<GoogleSheetsInegrationModel> criteria);
    public Task<Ulid> CreateIntegrationAsync(CreateGoogleSheetsIntegrationDTO request);
    public Task DeleteIntegrationAsync(Ulid integrationId);
    public Task RunIntegrationAsync(Ulid integrationId);
}

