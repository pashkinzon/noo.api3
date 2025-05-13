using System.Collections.Immutable;
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

    [GeneratedRegex(@"^var\(--[\w-]+\)$", RegexOptions.Compiled)]
    private static partial Regex CssVarRegexp();

    public static bool IsCSSVariable(object? value)
    {
        var regex = CssVarRegexp();
        return value is string cssVariable
           && !string.IsNullOrEmpty(cssVariable)
           && regex.IsMatch(cssVariable);
    }

    public static bool IsValidPassword(object? value)
    {
        if (value is not string password)
        {
            return false;
        }

        // Min length of 8
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
        {
            return false;
        }

        // At least 1 uppercase letter
        var hasUpperCase = password.Any(char.IsUpper);

        // At least 1 lowercase letter
        var hasLowerCase = password.Any(char.IsLower);

        // At least 1 digit
        var hasDigit = password.Any(char.IsDigit);

        // At least 1 special character
        var hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

        return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
    }
}
