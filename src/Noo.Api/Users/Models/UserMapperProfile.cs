using AutoMapper;
using Noo.Api.Core.Utils.AutoMapper;
using Noo.Api.Users.DTO;
using Telegram.Bot.Types;

namespace Noo.Api.Users.Models;

[AutoMapperProfile]
public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        // user
        CreateMap<UserModel, UserDTO>();
        CreateMap<UserCreationPayload, User>();

        // mentor assignment
        CreateMap<MentorAssignmentModel, MentorAssignmentDTO>();
    }
}
