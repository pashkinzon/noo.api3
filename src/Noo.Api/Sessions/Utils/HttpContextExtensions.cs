using Noo.Api.Core.Utils.UserAgent;
using Noo.Api.Sessions.Models;

namespace Noo.Api.Sessions.Utils;

public static class HttpContextExtensions
{
    public static SessionModel AsSessionModel(this HttpContext context, Ulid userId)
    {
        if (context is null || context.User is null)
        {
            throw new ArgumentNullException(nameof(context), "HttpContext or User cannot be null.");
        }

        var userAgent = context.Request.Headers.UserAgent.ToString();
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

        var info = UserAgentParser.Parse(userAgent);

        return new SessionModel
        {
            UserId = userId,
            UserAgent = userAgent,
            Os = info.Os,
            Browser = info.Browser,
            Device = info.Device,
            DeviceType = info.DeviceType,
            IpAddress = ip
        };
    }
}
