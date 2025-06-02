using Microsoft.AspNetCore.Authorization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Snippets;

public class SnippetPolicies : IPolicyRegistrar
{
    public const string CanCreateSnippet = nameof(CanCreateSnippet);
    public const string CanEditSnippet = nameof(CanEditSnippet);
    public const string CanDeleteSnippet = nameof(CanDeleteSnippet);
    public const string CanGetOwnSnippets = nameof(CanGetOwnSnippets);

    public void RegisterPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(CanCreateSnippet, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Mentor)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanEditSnippet, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Mentor)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanDeleteSnippet, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Mentor)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanGetOwnSnippets, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Mentor)
            ).RequireNotBlocked();
        });
    }
}
