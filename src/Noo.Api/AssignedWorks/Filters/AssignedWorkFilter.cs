using AutoFilterer.Attributes;
using AutoFilterer.Types;
using Noo.Api.AssignedWorks.Models;
using Noo.Api.Works.Types;
using Noo.Api.AssignedWorks.Types;

namespace Noo.Api.AssignedWorks.Filters;

[PossibleSortings(
    nameof(AssignedWorkModel.Title),
    nameof(AssignedWorkModel.CreatedAt),
    nameof(AssignedWorkModel.SolvedAt),
    nameof(AssignedWorkModel.CheckedAt)
)]
public class AssignedWorkFilter : PaginationFilterBase
{
    // 2) Global Search: one field that compares to multiple props
    [CompareTo(nameof(AssignedWorkModel.Title))]
    [CompareTo(nameof(AssignedWorkModel.Type))]
    [ToLowerContainsComparison]
    public string? Search { get; set; }

    public WorkType? Type { get; set; }

    public AssignedWorkSolveStatus? SolveStatus { get; set; }

    public AssignedWorkCheckStatus? CheckStatus { get; set; }
}
