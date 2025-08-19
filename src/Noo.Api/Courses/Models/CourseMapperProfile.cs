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

        // Allow creating content entities from DTOs
        CreateMap<CreateCourseMaterialContentDTO, CourseMaterialContentModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Material, opt => opt.Ignore())
            .ForMember(dest => dest.Work, opt => opt.Ignore());

        CreateMap<CourseModel, CourseDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ThumbnailId, opt => opt.MapFrom(src => src.ThumbnailId))
            .ForMember(dest => dest.Chapters, opt => opt.MapFrom(src => src.Chapters))
            .ForMember(dest => dest.MemberCount, opt => opt.Ignore())
            // Avoid implicit nested mapping in tests; controller/services set these when needed
            .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(_ => (Noo.Api.Media.DTO.MediaDTO?)null))
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(_ => (Noo.Api.Subjects.DTO.SubjectDTO?)null));
        // TODO: Member count
        CreateMap<CourseChapterModel, CourseChapterDTO>();
        CreateMap<CourseMaterialModel, CourseMaterialDTO>();

        CreateMap<CourseMaterialContentModel, CourseMaterialContentDTO>()
            // WorkDTO comes from WorkMapperProfile when included; ignore here by default
            .ForMember(dest => dest.Work, opt => opt.MapFrom(_ => (Noo.Api.Works.DTO.WorkDTO?)null));

        CreateMap<CourseMembershipModel, CourseMembershipDTO>()
            .ForMember(dest => dest.Student, opt => opt.MapFrom(_ => (Noo.Api.Users.DTO.UserDTO?)null))
            .ForMember(dest => dest.Assigner, opt => opt.MapFrom(_ => (Noo.Api.Users.DTO.UserDTO?)null))
            .ForMember(dest => dest.Course, opt => opt.Ignore());

        CreateMap<CreateCourseMembershipDTO, CourseMembershipModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Assigner, opt => opt.Ignore())
            .ForMember(dest => dest.Course, opt => opt.Ignore())
            .ForMember(dest => dest.Student, opt => opt.Ignore())
            // map incoming UserId -> StudentId
            .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.UserId))
            // default values
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
            .ForMember(dest => dest.IsArchived, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(_ => Noo.Api.Courses.Types.CourseMembershipType.ManualAssigned))
            // AssignerId is set in service layer from current user
            .ForMember(dest => dest.AssignerId, opt => opt.Ignore());
    }
}
