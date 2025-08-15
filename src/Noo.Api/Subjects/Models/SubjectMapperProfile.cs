using AutoMapper;
using Noo.Api.Core.Utils.AutoMapper;
using Noo.Api.Subjects.DTO;

namespace Noo.Api.Subjects.Models;

[AutoMapperProfile]
public class SubjectMapperProfile : Profile
{
    public SubjectMapperProfile()
    {
        CreateMap<SubjectModel, SubjectUpdateDTO>();

        CreateMap<SubjectModel, SubjectDTO>();

        CreateMap<SubjectUpdateDTO, SubjectModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.MentorAssignments, opt => opt.Ignore())
            .ForMember(dest => dest.Works, opt => opt.Ignore())
            .ForMember(dest => dest.Courses, opt => opt.Ignore());

        CreateMap<SubjectCreationDTO, SubjectModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.MentorAssignments, opt => opt.Ignore())
            .ForMember(dest => dest.Works, opt => opt.Ignore())
            .ForMember(dest => dest.Courses, opt => opt.Ignore());
    }
}
