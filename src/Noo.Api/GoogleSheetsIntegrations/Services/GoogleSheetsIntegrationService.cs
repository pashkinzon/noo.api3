using AutoMapper;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Utils.DI;
using Noo.Api.GoogleSheetsIntegrations.DTO;
using Noo.Api.GoogleSheetsIntegrations.Filters;
using Noo.Api.GoogleSheetsIntegrations.Models;

namespace Noo.Api.GoogleSheetsIntegrations.Services;

[RegisterScoped(typeof(IGoogleSheetsIntegrationService))]
public class GoogleSheetsIntegrationService : IGoogleSheetsIntegrationService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IGoogleSheetsIntegrationRepository _integrationRepository;

    private readonly IMapper _mapper;

    public GoogleSheetsIntegrationService(
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _unitOfWork = unitOfWork;
        _integrationRepository = _unitOfWork.GoogleSheetsIntegrationRepository();
        _mapper = mapper;
    }

    public async Task<Ulid> CreateIntegrationAsync(CreateGoogleSheetsIntegrationDTO request)
    {
        var model = _mapper.Map<GoogleSheetsIntegrationModel>(request);

        _integrationRepository.Add(model);
        await _unitOfWork.CommitAsync();

        return model.Id;
    }

    public Task DeleteIntegrationAsync(Ulid integrationId)
    {
        _integrationRepository.DeleteById(integrationId);
        return _unitOfWork.CommitAsync();
    }

    public Task<SearchResult<GoogleSheetsIntegrationModel>> GetIntegrationsAsync(GoogleSheetsIntegrationFilter filter)
    {
        return _integrationRepository.SearchAsync(filter);
    }

    public Task RunIntegrationAsync(Ulid integrationId)
    {
        throw new NotImplementedException();
        // TODO: run integration using google sheets api
    }
}
