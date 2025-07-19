using AutoFilterer.Attributes;
using AutoFilterer.Types;
using Noo.Api.Users.Models;

namespace Noo.Api.Users.Filters;

[PossibleSortings(
    nameof(MentorAssignmentModel.SubjectId),
    nameof(MentorAssignmentModel.MentorId),
    nameof(MentorAssignmentModel.StudentId)
)]
public class MentorAssignmentFilter : PaginationFilterBase
{
    public Ulid? SubjectId { get; set; }
    public Ulid? MentorId { get; set; }
    public Ulid? StudentId { get; set; }
}
