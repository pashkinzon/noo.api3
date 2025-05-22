using AutoMapper;
using Noo.Api.Core.Utils.AutoMapper;
using Noo.Api.Users.DTO;
using Noo.Api.Users.Types;

namespace Noo.Api.Users.Models;

[AutoMapperProfile]
public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        // user
        CreateMap<UserModel, UserDTO>();
        CreateMap<UserCreationPayload, UserModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.TelegramId, opt => opt.Ignore())
            .ForMember(dest => dest.TelegramUsername, opt => opt.Ignore())
            .ForMember(dest => dest.CoursesAsMember, opt => opt.Ignore())
            .ForMember(dest => dest.CoursesAsAssigner, opt => opt.Ignore())
            .ForMember(dest => dest.CoursesAsAuthor, opt => opt.Ignore())
            .ForMember(dest => dest.CoursesAsEditor, opt => opt.Ignore())
            .ForMember(dest => dest.CourseMaterialReactions, opt => opt.Ignore())
            .ForMember(dest => dest.IsBlocked, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(_ => false));


        // mentor assignment
        CreateMap<MentorAssignmentModel, MentorAssignmentDTO>();
    }
}
