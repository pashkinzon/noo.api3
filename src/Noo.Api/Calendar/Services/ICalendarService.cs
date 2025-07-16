using Noo.Api.Calendar.DTO;
using Noo.Api.Core.DataAbstraction.Db;

namespace Noo.Api.Calendar.Services;

public interface ICalendarService
{
    public Task<SearchResult<CalendarEventDTO>> GetCalendarEventsAsync(Ulid userId, int year, int month);
    public Task<Ulid> CreateCalendarEventAsync(Ulid userId, CreateCalendarEventDTO dto);
    public Task DeleteCalendarEventAsync(Ulid userId, Ulid eventId);
}
