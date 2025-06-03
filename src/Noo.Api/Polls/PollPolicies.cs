using Microsoft.AspNetCore.Authorization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Polls;

public class PollPolicies : IPolicyRegistrar
{
    public void RegisterPolicies(AuthorizationOptions options)
    {
        throw new NotImplementedException();
    }
}
