using Noo.Api.Core.Utils.Richtext.Delta;

namespace Noo.Api.Core.Utils.Richtext;

public static class RichTestFactory
{
    public static IRichTextType Create(string? text = null)
    {
        return DeltaRichText.FromString(text);
    }
}
