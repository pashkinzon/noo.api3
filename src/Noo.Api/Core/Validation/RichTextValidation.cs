using Noo.Api.Core.Utils.Richtext;

namespace Noo.Api.Core.Validation;

public static class RichTextValidation
{
    public static bool IsRichText(object? value)
    {
        return value == null || value is IRichTextType;
    }

    public static bool IsNonEmptyRichText(object? value)
    {
        return value != null && value is IRichTextType richText && !richText.IsEmpty();
    }
}
