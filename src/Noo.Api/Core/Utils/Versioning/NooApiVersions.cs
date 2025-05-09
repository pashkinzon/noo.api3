using Microsoft.AspNetCore.Mvc;

namespace Noo.Api.Core.Utils.Versioning;

public static class NooApiVersions
{
    public const string Current = V1;

    public static ApiVersion CurrentAsVersion => ApiVersion.Parse(Current);

    public const string V1 = "1.0";
}
