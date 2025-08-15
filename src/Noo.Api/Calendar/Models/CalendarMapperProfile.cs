using AutoMapper;
using Noo.Api.Calendar.DTO;
using Noo.Api.Core.Utils.AutoMapper;

namespace Noo.Api.Calendar.Models;

[AutoMapperProfile]
public class CalendarMapperProfile : Profile
{
    public CalendarMapperProfile()
    {
        CreateMap<CreateCalendarEventDTO, CalendarEventModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.AssignedWork, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<CalendarEventModel, CalendarEventDTO>();
    }
}
