using AutoFilterer.Attributes;
using AutoFilterer.Types;
using Noo.Api.Subjects.Models;

namespace Noo.Api.Subjects.Filters;

[PossibleSortings(
    nameof(SubjectModel.Name)
)]
public class SubjectFilter : PaginationFilterBase
{
    // 2) Global Search: one field that compares to multiple props
    [CompareTo(nameof(SubjectModel.Name))]
    [ToLowerContainsComparison]
    public string? Search { get; set; }
}
