using Moq;
using Noo.Api.Auth.EmailTemplates;
using Noo.Api.Auth.Services;
using Noo.Api.Core.System.Email;
using Xunit;

namespace Noo.Api.UnitTests.Auth;

public class AuthEmailServiceTests
{
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly AuthEmailService _service;

    public AuthEmailServiceTests()
    {
        _emailServiceMock = new Mock<IEmailService>();
        _service = new AuthEmailService(_emailServiceMock.Object);
    }

    [Fact]
    public async Task SendEmailVerificationEmailAsync_SendsCorrectEmail()
    {
        const string email = "user@example.com";
        const string name = "User";
        const string link = "link";

        await _service.SendEmailVerificationEmailAsync(email, name, link);

        _emailServiceMock.Verify(x => x.SendEmailAsync(It.Is<Email<EmailVerificationData, EmailVerificationTemplate>>(e =>
            e.ToEmail == email &&
            e.ToName == name &&
            e.Subject == "Подтверждение электронной почты" &&
            e.Body.Name == name &&
            e.Body.VerificationLink == link
        )), Times.Once);
    }

    [Fact]
    public async Task SendForgotPasswordEmailAsync_SendsCorrectEmail()
    {
        const string email = "user@example.com";
        const string name = "User";
        const string link = "link";

        await _service.SendForgotPasswordEmailAsync(email, name, link);

        _emailServiceMock.Verify(x => x.SendEmailAsync(It.Is<Email<RequestPasswordChangeData, RequestPasswordChangeTemplate>>(e =>
            e.ToEmail == email &&
            e.ToName == name &&
            e.Subject == "Сброс пароля" &&
            e.Body.Name == name &&
            e.Body.ResetPasswordLink == link
        )), Times.Once);
    }

    [Fact]
    public async Task SendEmailChangeEmailAsync_SendsCorrectEmail()
    {
        const string email = "user@example.com";
        const string name = "User";
        const string link = "link";

        await _service.SendEmailChangeEmailAsync(email, name, link);

        _emailServiceMock.Verify(x => x.SendEmailAsync(It.Is<Email<ChangeEmailData, ChangeEmailTemplate>>(e =>
            e.ToEmail == email &&
            e.ToName == name &&
            e.Subject == "Изменение электронной почты" &&
            e.Body.Name == name &&
            e.Body.ChangeEmailLink == link
        )), Times.Once);
    }
}
