using Microsoft.AspNetCore.Mvc;

namespace Noo.Api.Media.Types;

public class UploadReason
{
    [FromQuery(Name = "reasonType")]
    public ReasonType Type { get; set; }

    [FromQuery(Name = "entityId")]
    public Ulid EntityId { get; set; }

    public /* IAuthorizationRequirement */ void GetRequirement()
    {
        /* switch (Type)
        {
            case ReasonType.Work:
            case ReasonType.Course:
            case ReasonType.CourseThumbnail:
            case ReasonType.AssignedWorkAnswer:
            case ReasonType.AssignedWorkComment:
            case ReasonType.Avatar:
            case ReasonType.Help:
            case ReasonType.PollAnswer:
            case ReasonType.VideoThumbnail:
            default:
                return new ImpossibleRequirement();
        } */
    }
}
