namespace Noo.Api.Auth.Services;

public interface IAuthEmailService
{
    public Task SendEmailVerificationEmailAsync(string toEmail, string name, string verificationLink);

    public Task SendForgotPasswordEmailAsync(string toEmail, string name, string resetPasswordLink);

    public Task SendEmailChangeEmailAsync(string toEmail, string name, string changeEmailLink);
}
