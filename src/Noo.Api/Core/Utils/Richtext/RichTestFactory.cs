using Noo.Api.Core.Utils.Richtext.Delta;

namespace Noo.Api.Core.Utils.Richtext;

public static class RichTextFactory
{
    public static IRichTextType Create(string? text = null)
    {
        return DeltaRichText.FromString(text);
    }
}
