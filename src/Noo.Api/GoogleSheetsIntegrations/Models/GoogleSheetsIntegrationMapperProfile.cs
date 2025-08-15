using AutoMapper;
using Noo.Api.Core.Utils.AutoMapper;
using Noo.Api.GoogleSheetsIntegrations.DTO;

namespace Noo.Api.GoogleSheetsIntegrations.Models;

[AutoMapperProfile]
public class GoogleSheetsIntegrationMapperProfile : Profile
{
    public GoogleSheetsIntegrationMapperProfile()
    {
        CreateMap<GoogleSheetsIntegrationModel, GoogleSheetsIntegrationDTO>();

        CreateMap<CreateGoogleSheetsIntegrationDTO, GoogleSheetsIntegrationModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.LastErrorText, opt => opt.Ignore())
            .ForMember(dest => dest.LastRunAt, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
    }
}
