using AutoMapper;
using Noo.Api.Core.Utils.AutoMapper;
using Noo.Api.UserSettings.DTO;

namespace Noo.Api.UserSettings.Models;

[AutoMapperProfile]
public class UserSettingsMapperProfile : Profile
{
    public UserSettingsMapperProfile()
    {
        CreateMap<UserSettingsModel, UserSettingsDTO>();

        CreateMap<UserSettingsUpdateDTO, UserSettingsModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
    }
}
