using AutoFilterer.Attributes;
using AutoFilterer.Types;
using Noo.Api.Notifications.Models;

namespace Noo.Api.Notifications.Filters;

[PossibleSortings(
    nameof(NotificationModel.IsRead),
    nameof(NotificationModel.UserId)
)]
public class NotificationFilter : PaginationFilterBase
{
    public bool? IsRead { get; set; }

    public Ulid? UserId { get; set; }
}
