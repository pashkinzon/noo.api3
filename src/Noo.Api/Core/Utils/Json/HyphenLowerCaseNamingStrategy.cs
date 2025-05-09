using System.Text.RegularExpressions;
using Newtonsoft.Json.Serialization;

namespace Noo.Api.Core.Utils.Json;

public partial class HyphenLowerCaseNamingStrategy : NamingStrategy
{

    [GeneratedRegex("(?<!^)([A-Z])")]
    private static partial Regex CapitalLettersRegexp();

    protected override string ResolvePropertyName(string name)
    {
        return CapitalLettersRegexp().Replace(name, "-$1").ToLower();
    }
}
