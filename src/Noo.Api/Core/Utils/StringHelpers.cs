using System.Text;

namespace Noo.Api.Core.Utils;

public static class StringHelpers
{
    public static string ToSnakeCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        var result = new StringBuilder();

        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];
            if (char.IsUpper(c) && i > 0 && !char.IsUpper(str[i - 1]))
            {
                result.Append('_');
            }
            result.Append(char.ToLower(c));
        }

        return result.ToString();
    }
}
