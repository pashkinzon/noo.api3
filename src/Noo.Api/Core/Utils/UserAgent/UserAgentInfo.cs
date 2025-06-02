namespace Noo.Api.Core.Utils.UserAgent;

public class UserAgentInfo
{
    public string? Browser { get; set; }
    public string? Os { get; set; }
    public string? Device { get; set; }
    public DeviceType DeviceType { get; set; } = DeviceType.Unknown;
}
