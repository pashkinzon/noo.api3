using Noo.Api.Core.Validation;

namespace Noo.UnitTests.Core.Validation;

public class StringValidationTests
{
    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("01H84V6K3M4CQP2W6E3Q2X8V7Z", true)] // valid ULID format
    [InlineData("not-a-ulid", false)]
    public void IsUlid_Works(object? input, bool expected)
    {
        Assert.Equal(expected, StringValidation.IsUlid(input));
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("var(--color-primary)", true)]
    [InlineData("var(--c1)", true)]
    [InlineData("var(--bad value)", false)]
    [InlineData("--color", false)]
    public void IsCSSVariable_Works(object? input, bool expected)
    {
        Assert.Equal(expected, StringValidation.IsCSSVariable(input));
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("short1!", false)] // too short
    [InlineData("alllowercase1!", false)] // no upper
    [InlineData("ALLUPPERCASE1!", false)] // no lower
    [InlineData("NoDigits!", false)] // no digit
    [InlineData("NoSpecial1", false)] // no special
    [InlineData("GoodPwd1!", true)]
    [InlineData("P@ssw0rd!", true)]
    public void IsValidPassword_Works(object? input, bool expected)
    {
        Assert.Equal(expected, StringValidation.IsValidPassword(input));
    }
}
