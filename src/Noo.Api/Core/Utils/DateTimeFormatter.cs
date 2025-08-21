using System.Globalization;

namespace Noo.Api.Core.Utils;

public static class DateTimeFormatter
{
    public static readonly CultureInfo culture = new("ru-RU");

    public static string FormatDate(DateTime dateTime)
    {
        return dateTime.ToString("dd MMMM", culture);
    }
}
