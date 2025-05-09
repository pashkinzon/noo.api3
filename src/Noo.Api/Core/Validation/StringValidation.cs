using System.Text.RegularExpressions;

namespace Noo.Api.Core.Validation;

public static partial class StringValidation
{
    public static bool IsUlid(object? value)
    {
        return value is string ulidString
           && !string.IsNullOrEmpty(ulidString)
           && Ulid.TryParse(ulidString, out _);
    }

    public static bool IsCSSVariable(object? value)
    {
        var regex = CssVarRegexp();
        return value is string cssVariable
           && !string.IsNullOrEmpty(cssVariable)
           && regex.IsMatch(cssVariable);
    }

    [GeneratedRegex(@"^var\(--[\w-]+\)$", RegexOptions.Compiled)]
    private static partial Regex CssVarRegexp();
}
