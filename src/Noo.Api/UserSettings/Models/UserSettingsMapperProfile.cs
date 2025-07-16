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
        CreateMap<UserSettingsUpdateDTO, UserSettingsModel>();
    }
}
