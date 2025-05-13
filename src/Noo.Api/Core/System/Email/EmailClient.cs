using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Noo.Api.Core.Config.Env;
using Noo.Api.Core.Utils.DI;

namespace Noo.Api.Core.System.Email;

[RegisterScoped(typeof(IEmailClient))]
public class EmailClient : IEmailClient
{
    private readonly ISmtpClient _client;

    private readonly ILogger<EmailClient> _logger;

    private readonly EmailConfig _config;

    public EmailClient(IOptions<EmailConfig> config, ILogger<EmailClient> logger, ISmtpClient smtpClient)
    {
        _logger = logger;
        _config = config.Value;
        _client = smtpClient;

        _client = new SmtpClient
        {
            Timeout = _config.SmtpTimeout
        };
    }

    public async Task SendHtmlEmailAsync(
        string? fromEmail,
        string? fromName,
        string toEmail,
        string toName,
        string subject,
        string htmlBody
    )
    {
        await ConnectAsync();
        await AuthenticateAsync();

        try
        {
            var message = new MimeMessage();

            var from = new MailboxAddress(
                fromName ?? _config.FromName,
                fromEmail ?? _config.FromEmail
            );

            var to = new MailboxAddress(toName, toEmail);

            message.From.Add(from);
            message.To.Add(to);
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = htmlBody };

            await _client.SendAsync(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email");
            throw new CantSendEmailException(ex);
        }
        finally
        {
            await DisconnectAsync();
        }
    }

    public void Dispose()
    {
        if (_client.IsConnected)
        {
            _client.Disconnect(true);
        }

        _client.Dispose();
    }

    private Task ConnectAsync()
    {
        if (_client.IsConnected)
            return Task.CompletedTask;

        return _client.ConnectAsync(_config.SmtpHost, _config.SmtpPort, true);
    }

    private Task DisconnectAsync()
    {
        if (_client.IsConnected)
        {
            return _client.DisconnectAsync(true);
        }

        return Task.CompletedTask;
    }

    private async Task AuthenticateAsync()
    {
        if (_client.IsAuthenticated)
        {
            return;
        }

        await _client.AuthenticateAsync();
    }
}
