using AutoFilterer.Attributes;
using AutoFilterer.Types;
using Noo.Api.Works.Models;
using Noo.Api.Works.Types;

namespace Noo.Api.Works.Filters;

[PossibleSortings(
    nameof(WorkModel.Title),
    nameof(WorkModel.Type),
    nameof(WorkModel.SubjectId),
    nameof(WorkModel.CreatedAt)
)]
public class WorkFilter : PaginationFilterBase
{
    // 2) Global Search: one field that compares to multiple props
    [CompareTo(nameof(WorkModel.Title))]
    [CompareTo(nameof(WorkModel.Description))]
    [ToLowerContainsComparison]
    public string? Search { get; set; }

    // 3) Per‑field filters still just properties
    public WorkType? Type { get; set; }
    public Ulid? SubjectId { get; set; }
}
