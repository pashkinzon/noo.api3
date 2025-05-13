using Noo.Api.Core.System.Email;
using Noo.Api.Auth.EmailTemplates;
using Noo.Api.Core.Utils.DI;

namespace Noo.Api.Auth.Services;

[RegisterScoped(typeof(IAuthEmailService))]
public class AuthEmailService : IAuthEmailService
{
    private readonly IEmailService _emailService;

    public AuthEmailService(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task SendEmailVerificationEmailAsync(string toEmail, string name, string verificationLink)
    {
        var email = new Email<EmailVerificationData, EmailVerificationTemplate>
        {
            ToEmail = toEmail,
            ToName = name,
            Subject = "Подтверждение электронной почты",
            Body = new EmailVerificationData
            {
                Name = name,
                VerificationLink = verificationLink
            }
        };

        await _emailService.SendEmailAsync(email);
    }

    public Task SendForgotPasswordEmailAsync(string toEmail, string name, string resetPasswordLink)
    {
        var email = new Email<RequestPasswordChangeData, RequestPasswordChangeTemplate>
        {
            ToEmail = toEmail,
            ToName = name,
            Subject = "Сброс пароля",
            Body = new RequestPasswordChangeData
            {
                Name = name,
                ResetPasswordLink = resetPasswordLink
            }
        };

        return _emailService.SendEmailAsync(email);
    }

    public Task SendEmailChangeEmailAsync(string toEmail, string name, string changeEmailLink)
    {
        var email = new Email<ChangeEmailData, ChangeEmailTemplate>
        {
            ToEmail = toEmail,
            ToName = name,
            Subject = "Изменение электронной почты",
            Body = new ChangeEmailData
            {
                Name = name,
                ChangeEmailLink = changeEmailLink
            }
        };

        return _emailService.SendEmailAsync(email);
    }
}
