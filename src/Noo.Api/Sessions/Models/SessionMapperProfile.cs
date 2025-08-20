using AutoMapper;
using Noo.Api.Core.Utils.AutoMapper;
using Noo.Api.Sessions.DTO;

namespace Noo.Api.Sessions.Models;

[AutoMapperProfile]
public class SessionMapperProfile : Profile
{
    public SessionMapperProfile()
    {
        CreateMap<SessionModel, SessionDTO>()
            .ForMember(d => d.LastRequestAt, opt => opt.MapFrom(s => s.LastRequestAt ?? s.UpdatedAt ?? s.CreatedAt));
    }
}
