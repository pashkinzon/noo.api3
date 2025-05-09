using System.Text.Json.Serialization;
using Noo.Api.Core.Utils.Richtext.Delta;

namespace Noo.Api.Core.Utils.Richtext;

[JsonPolymorphic(
    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor,
    TypeDiscriminatorPropertyName = "$type"
)]
[JsonDerivedType(
    derivedType: typeof(DeltaRichText),
    typeDiscriminator: DeltaRichText.TypeDiscriminator
)]
public interface IRichTextType
{
    public string? ToString();

    public bool IsEmpty();

    public int Length();
}
