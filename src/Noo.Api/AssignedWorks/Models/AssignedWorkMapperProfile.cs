using AutoMapper;
using Noo.Api.AssignedWorks.DTO;
using Noo.Api.Core.Utils.AutoMapper;

namespace Noo.Api.AssignedWorks.Models;

[AutoMapperProfile]
public class AssignedWorkMapperProfile : Profile
{
    public AssignedWorkMapperProfile()
    {
        CreateMap<UpsertAssignedWorkAnswerDTO, AssignedWorkAnswerModel>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<UpsertAssignedWorkCommentDTO, AssignedWorkCommentModel>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
    }
}
