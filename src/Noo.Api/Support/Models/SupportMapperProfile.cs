using AutoMapper;
using Noo.Api.Core.Utils.AutoMapper;
using Noo.Api.Support.DTO;

namespace Noo.Api.Support.Models;

[AutoMapperProfile]
public class SupportMapperProfile : Profile
{
    public SupportMapperProfile()
    {
        CreateMap<CreateSupportArticleDTO, SupportArticleModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore());

        CreateMap<SupportArticleModel, UpdateSupportArticleDTO>();

        CreateMap<UpdateSupportArticleDTO, SupportArticleModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore());

        CreateMap<SupportArticleModel, SupportArticleDTO>();

        CreateMap<CreateSupportCategoryDTO, SupportCategoryModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Parent, opt => opt.Ignore())
            .ForMember(dest => dest.Children, opt => opt.Ignore())
            .ForMember(dest => dest.Articles, opt => opt.Ignore());

        CreateMap<SupportCategoryModel, UpdateSupportCategoryDTO>();

        CreateMap<UpdateSupportCategoryDTO, SupportCategoryModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Parent, opt => opt.Ignore())
            .ForMember(dest => dest.Children, opt => opt.Ignore())
            .ForMember(dest => dest.Articles, opt => opt.Ignore());

        CreateMap<SupportCategoryModel, SupportCategoryDTO>()
            .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children.OrderBy(c => c.Order)))
            .ForMember(dest => dest.Articles, opt => opt.MapFrom(src => src.Articles.OrderBy(a => a.Order)));
    }
}
