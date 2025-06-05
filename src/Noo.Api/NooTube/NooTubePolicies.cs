using Microsoft.AspNetCore.Authorization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.NooTube;

public class NooTubePolicies : IPolicyRegistrar
{
    public const string CanGetNooTubeVideos = nameof(CanGetNooTubeVideos);

    public void RegisterPolicies(AuthorizationOptions options)
    {
        throw new NotImplementedException();
    }
}
