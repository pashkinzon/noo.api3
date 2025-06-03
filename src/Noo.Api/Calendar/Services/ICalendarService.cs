using Noo.Api.Calendar.DTO;

namespace Noo.Api.Calendar.Services;

public interface ICalendarService
{
    public Task<IEnumerable<CalendarEventDTO>> GetCalendarEventsAsync(Ulid userId, int year, int month);
    public Task<Ulid> CreateCalendarEventAsync(CreateCalendarEventDTO dto);
}
