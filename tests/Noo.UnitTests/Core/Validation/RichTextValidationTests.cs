using Noo.Api.Core.Validation;
using Noo.Api.Core.Utils.Richtext;

namespace Noo.UnitTests.Core.Validation;

public class RichTextValidationTests
{
    private sealed class DummyRichText : IRichTextType
    {
        public bool Empty { get; set; }

        public int Length() => Empty ? 0 : 1;
        public bool IsEmpty() => Empty;
        public override string? ToString() => Empty ? string.Empty : "x";
    }

    [Fact]
    public void IsRichText_AllowsNullOrIRichText()
    {
        Assert.True(RichTextValidation.IsRichText(null));
        Assert.True(RichTextValidation.IsRichText(new DummyRichText()));
        Assert.False(RichTextValidation.IsRichText("not-rich"));
    }

    [Fact]
    public void IsNonEmptyRichText_TrueOnlyForNonEmptyIRichText()
    {
        Assert.False(RichTextValidation.IsNonEmptyRichText(null));
        Assert.False(RichTextValidation.IsNonEmptyRichText(new DummyRichText { Empty = true }));
        Assert.True(RichTextValidation.IsNonEmptyRichText(new DummyRichText { Empty = false }));
        Assert.False(RichTextValidation.IsNonEmptyRichText("not-rich"));
    }
}
