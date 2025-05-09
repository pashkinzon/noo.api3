using System.Text.Json;
using System.Text.RegularExpressions;

namespace Noo.Api.Core.Utils.Json;

public partial class HyphenLowerCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        // Split PascalCase into words using regex
        var words = HyphenLowercaseRegexp().Matches(name)
            .Cast<Match>()
            .Select(m => m.Value.ToLower());

        return string.Join("-", words);
    }

    [GeneratedRegex("(^[a-z]+|[A-Z]+(?![a-z])|[A-Z][a-z]+)")]
    private static partial Regex HyphenLowercaseRegexp();
}
