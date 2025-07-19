using AutoFilterer.Attributes;
using AutoFilterer.Types;
using Noo.Api.Snippets.Models;

namespace Noo.Api.Snippets.Filters;

[PossibleSortings(
    nameof(SnippetModel.Name)
)]
public class SnippetFilter : PaginationFilterBase
{
    // 2) Global Search: one field that compares to multiple props
    [CompareTo(nameof(SnippetModel.Name))]
    [ToLowerContainsComparison]
    public string? Search { get; set; }

    public Ulid? UserId { get; set; }
}
