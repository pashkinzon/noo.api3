using System.Reflection;
using Microsoft.AspNetCore.Authorization;

namespace Noo.Api.Core.Security.Authorization.Attributes;

/// <summary>
/// Specifies the name of a policy to require for a given action.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class PolicyAttribute : AuthorizeAttribute
{
    public PolicyAttribute(Type policyContainerType, string policyFieldOrPropertyName)
    {
        var field = policyContainerType.GetField(policyFieldOrPropertyName, BindingFlags.Static | BindingFlags.Public);

        if (field == null)
        {
            throw new ArgumentException($"No static field '{policyFieldOrPropertyName}' found on {policyContainerType}");
        }

        Policy = field.GetValue(null)?.ToString()
                 ?? throw new ArgumentException($"Field '{policyFieldOrPropertyName}' is null or not a string.");
    }
}
