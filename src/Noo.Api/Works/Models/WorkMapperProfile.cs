using AutoMapper;
using Noo.Api.Core.Utils.AutoMapper;
using Noo.Api.Works.DTO;

namespace Noo.Api.Works.Models;

[AutoMapperProfile]
public class WorkMapperProfile : Profile
{
    public WorkMapperProfile()
    {
        // work task
        CreateMap<WorkTaskModel, WorkTaskResponseDTO>();

        CreateMap<CreateWorkTaskDTO, WorkTaskModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.WorkId, opt => opt.Ignore())
            .ForMember(dest => dest.Work, opt => opt.Ignore());

        CreateMap<WorkTaskModel, UpdateWorkTaskDTO>();

        CreateMap<UpdateWorkTaskDTO, WorkTaskModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.WorkId, opt => opt.Ignore())
            .ForMember(dest => dest.Work, opt => opt.Ignore());

        // work
        CreateMap<WorkModel, UpdateWorkDTO>();

        CreateMap<WorkModel, WorkResponseDTO>();

        CreateMap<UpdateWorkDTO, WorkModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Subject, opt => opt.Ignore())
            .ForMember(dest => dest.CourseMaterialContents, opt => opt.Ignore());

        CreateMap<CreateWorkDTO, WorkModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Subject, opt => opt.Ignore())
            .ForMember(dest => dest.CourseMaterialContents, opt => opt.Ignore());
    }
}
