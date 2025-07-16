using Microsoft.EntityFrameworkCore;
using Noo.Api.Calendar.Models;
using Noo.Api.Core.DataAbstraction.Db;

namespace Noo.Api.Calendar.Services;

public class CalendarEventRepository : Repository<CalendarEventModel>, ICalendarEventRepository
{

    public Task<CalendarEventModel?> GetEventAsync(Ulid userId, Ulid eventId)
    {
        return Context.Set<CalendarEventModel>()
            .FirstOrDefaultAsync(e => e.UserId == userId && e.Id == eventId);
    }
}

public static class CalendarEventRepositoryExtensions
{
    public static ICalendarEventRepository CalendarEventRepository(this IUnitOfWork unitOfWork)
    {
        return new CalendarEventRepository()
        {
            Context = unitOfWork.Context
        };
    }
}
