using Microsoft.AspNetCore.Components;

namespace Noo.Api.Core.System.Email;

public class Email<TData, TTemplateComponent> where TTemplateComponent : IComponent where TData : class

{
    public string? ToEmail { get; set; }

    public string? ToName { get; set; }

    public string FromEmail { get; set; } = string.Empty;

    public string FromName { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public TData Body { get; set; } = default!;
}
