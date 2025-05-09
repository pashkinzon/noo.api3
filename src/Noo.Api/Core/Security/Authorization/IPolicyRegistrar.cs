using Microsoft.AspNetCore.Authorization;

namespace Noo.Api.Core.Security.Authorization;

public interface IPolicyRegistrar
{
    public void RegisterPolicies(AuthorizationOptions options);
}
