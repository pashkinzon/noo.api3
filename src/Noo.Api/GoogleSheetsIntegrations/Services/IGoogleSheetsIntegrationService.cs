using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.GoogleSheetsIntegrations.DTO;
using Noo.Api.GoogleSheetsIntegrations.Models;

namespace Noo.Api.GoogleSheetsIntegrations.Services;

public interface IGoogleSheetsIntegrationService
{
    public Task<SearchResult<GoogleSheetsIntegrationModel>> GetIntegrationsAsync(Criteria<GoogleSheetsIntegrationModel> criteria);
    public Task<Ulid> CreateIntegrationAsync(CreateGoogleSheetsIntegrationDTO request);
    public Task DeleteIntegrationAsync(Ulid integrationId);
    public Task RunIntegrationAsync(Ulid integrationId);
}

