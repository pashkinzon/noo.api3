
using Microsoft.AspNetCore.Components;

namespace Noo.Api.Core.System.Email;

public interface IEmailRenderer
{
    // Renders the email template with the razor componont provided as type arg
    public Task<string> RenderEmailAsync<TComponent, TParams>(TParams dictionary) where TComponent : IComponent where TParams : class;
}
