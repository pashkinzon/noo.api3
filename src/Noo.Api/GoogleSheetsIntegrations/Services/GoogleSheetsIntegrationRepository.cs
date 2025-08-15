using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.GoogleSheetsIntegrations.Models;

namespace Noo.Api.GoogleSheetsIntegrations.Services;

public class GoogleSheetsIntegrationRepository : Repository<GoogleSheetsIntegrationModel>, IGoogleSheetsIntegrationRepository;

public static class GoogleSheetsIntegrationRepositoryExtensions
{
    public static IGoogleSheetsIntegrationRepository GoogleSheetsIntegrationRepository(this IUnitOfWork unitOfWork)
    {
        return new GoogleSheetsIntegrationRepository
        {
            Context = unitOfWork.Context
        };
    }
}
