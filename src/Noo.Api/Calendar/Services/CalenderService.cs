using Noo.Api.Calendar.DTO;
using Noo.Api.Core.Utils.DI;

namespace Noo.Api.Calendar.Services;

[RegisterScoped(typeof(ICalendarService))]
public class CalendarService : ICalendarService
{
    public Task<Ulid> CreateCalendarEventAsync(CreateCalendarEventDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCalendarEventAsync(Ulid userId, Ulid eventId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CalendarEventDTO>> GetCalendarEventsAsync(Ulid userId, int year, int month)
    {
        throw new NotImplementedException();
    }
}
