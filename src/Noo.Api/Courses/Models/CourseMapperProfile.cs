using AutoMapper;
using Noo.Api.Core.Utils.AutoMapper;
using Noo.Api.Courses.DTO;

namespace Noo.Api.Courses.Models;

[AutoMapperProfile]
public class CourseMapperProfile : Profile
{
    public CourseMapperProfile()
    {
        CreateMap<CreateCourseDTO, CourseModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Chapters, opt => opt.Ignore())
            .ForMember(dest => dest.Editors, opt => opt.Ignore())
            .ForMember(dest => dest.Authors, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Memberships, opt => opt.Ignore())
            .ForMember(dest => dest.Thumbnail, opt => opt.Ignore())
            .ForMember(dest => dest.Subject, opt => opt.Ignore());

        CreateMap<CourseModel, CourseDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ThumbnailId, opt => opt.MapFrom(src => src.ThumbnailId))
            .ForMember(dest => dest.Chapters, opt => opt.MapFrom(src => src.Chapters))
            .ForMember(dest => dest.MemberCount, opt => opt.Ignore());
        // TODO: Member count

        CreateMap<CourseChapterModel, CourseChapterDTO>();

        CreateMap<CourseMaterialModel, CourseMaterialDTO>();

        CreateMap<CourseMaterialContentModel, CourseMaterialContentDTO>();

        CreateMap<CourseMembershipModel, CourseMembershipDTO>();
    }
}
