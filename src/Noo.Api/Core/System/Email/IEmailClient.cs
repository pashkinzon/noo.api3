namespace Noo.Api.Core.System.Email;

public interface IEmailClient : IDisposable
{
    public Task SendHtmlEmailAsync(
        string? fromEmail,
        string? fromName,
        string toEmail,
        string toName,
        string subject,
        string htmlBody
    );
}
