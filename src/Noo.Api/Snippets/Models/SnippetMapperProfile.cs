using AutoMapper;
using Noo.Api.Core.Utils.AutoMapper;
using Noo.Api.Snippets.DTO;

namespace Noo.Api.Snippets.Models;

[AutoMapperProfile]
public class SnippetMapperProfile : Profile
{
    public SnippetMapperProfile()
    {
        CreateMap<SnippetModel, SnippetDTO>();

        CreateMap<SnippetModel, UpdateSnippetDTO>();

        CreateMap<UpdateSnippetDTO, SnippetModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());

        CreateMap<CreateSnippetDTO, SnippetModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());
    }
}
