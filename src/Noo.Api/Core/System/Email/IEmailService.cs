using Microsoft.AspNetCore.Components;

namespace Noo.Api.Core.System.Email;

public interface IEmailService
{
    public Task SendEmailAsync<TData, TTemplate>(Email<TData, TTemplate> email) where TTemplate : IComponent where TData : class;
}
