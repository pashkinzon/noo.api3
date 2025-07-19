using AutoFilterer.Attributes;
using AutoFilterer.Types;
using Noo.Api.Calendar.Models;

namespace Noo.Api.Calendar.Filters;

[PossibleSortings(
    nameof(CalendarEventModel.UserId),
    nameof(CalendarEventModel.StartDateTime),
    nameof(CalendarEventModel.EndDateTime)
)]
public class CalendarEventFilter : PaginationFilterBase
{
    public Ulid? UserId { get; set; }

    public Range<DateTime>? StartDateTime { get; set; }
}
