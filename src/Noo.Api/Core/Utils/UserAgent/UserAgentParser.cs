using System.Text.RegularExpressions;

namespace Noo.Api.Core.Utils.UserAgent;


public static class UserAgentParser
{
    // Common regex patterns for browser detection (verbatim strings to avoid escape issues)
    private static readonly (string Name, Regex Pattern)[] _browserPatterns = [
        ("Edge", new(@"Edg(e|A|iOS)?/\d+")),
        ("Chrome", new(@"Chrome/\d+")),
        ("Firefox", new(@"Firefox/\d+")),
        ("Safari", new(@"Version/\d+.*Safari/")),
        ("Opera", new(@"OPR/\d+")),
        ("IE", new(@"MSIE \d+|Trident/\d+"))
    ];

    // Common regex patterns for OS detection
    private static readonly (string Name, Regex Pattern)[] _osPatterns = [
        ("Windows", new(@"Windows NT [\d.]+")),
        ("Mac OS", new(@"Mac OS X [\d_]+")),
        ("iOS", new(@"(iPhone|iPad|iPod).*OS [\d_]+")),
        ("Android", new(@"Android [\d.]+")),
        ("Linux", new(@"Linux"))
    ];

    public static UserAgentInfo Parse(string userAgent)
    {
        if (string.IsNullOrEmpty(userAgent))
            throw new ArgumentNullException(nameof(userAgent));

        var info = new UserAgentInfo();
        var ua = userAgent;

        // Browser detection
        foreach (var (name, pattern) in _browserPatterns)
        {
            if (pattern.IsMatch(ua))
            {
                info.Browser = name;
                break;
            }
        }

        info.Browser ??= "Unknown";

        // OS detection
        foreach (var (name, pattern) in _osPatterns)
        {
            if (pattern.IsMatch(ua))
            {
                info.Os = name;
                break;
            }
        }

        info.Os ??= "Unknown";

        // Device type and device name
        if (Regex.IsMatch(ua, "Mobi|Android.*Mobile|iPhone", RegexOptions.IgnoreCase))
        {
            info.DeviceType = DeviceType.Mobile;
            // Use Device name hints
            if (Regex.IsMatch(ua, "iPhone", RegexOptions.IgnoreCase))
                info.Device = "iPhone";
            else if (Regex.IsMatch(ua, "Android", RegexOptions.IgnoreCase))
                info.Device = "Android Phone";
            else
                info.Device = "Mobile";
        }
        else if (Regex.IsMatch(ua, "iPad|Tablet|Nexus 7|Nexus 10|SM-T|Kindle", RegexOptions.IgnoreCase))
        {
            info.DeviceType = DeviceType.Tablet;
            if (Regex.IsMatch(ua, "iPad", RegexOptions.IgnoreCase))
                info.Device = "iPad";
            else
                info.Device = "Tablet";
        }
        else
        {
            info.DeviceType = DeviceType.Desktop;
            info.Device = "Desktop";
        }

        return info;
    }
}

