using AutoMapper;
using Noo.Api.Calendar.DTO;
using Noo.Api.Core.Utils.AutoMapper;

namespace Noo.Api.Calendar.Models;

[AutoMapperProfile]
public class CalendarMapperProfile : Profile
{
    public CalendarMapperProfile()
    {
        CreateMap<CreateCalendarEventDTO, CalendarEventModel>();

        CreateMap<CalendarEventModel, CalendarEventDTO>();
    }
}
