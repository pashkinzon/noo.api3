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
        CreateMap<SubjectUpdateDTO, SubjectModel>();
        CreateMap<SubjectCreationDTO, SubjectModel>();
    }
}
