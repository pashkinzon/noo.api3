using Microsoft.Extensions.Options;
using Noo.Api.Auth.Services;
using Noo.Api.Core.Config.Env;
using Xunit;

namespace Noo.Api.UnitTests.Auth;

public class AuthUrlGeneratorTests
{
    private readonly AuthUrlGenerator _generator;

    public AuthUrlGeneratorTests()
    {
        var config = new AppConfig
        {
            Location = "https://example.com",
            BaseUrl = "https://example.com",
            UserOnlineThresholdMinutes = 15,
            UserActiveThresholdDays = 7,
            AllowedOrigins = []
        };
        _generator = new AuthUrlGenerator(Options.Create(config));
    }

    [Fact]
    public void GenerateEmailVerificationUrl_ReturnsCorrectUrl()
    {
        const string token = "token";
        var url = _generator.GenerateEmailVerificationUrl(token);
        Assert.Equal("https://example.com/auth/verify-email?token=token", url);
    }

    [Fact]
    public void GeneratePasswordResetUrl_ReturnsCorrectUrl()
    {
        const string token = "token";
        var url = _generator.GeneratePasswordResetUrl(token);
        Assert.Equal("https://example.com/auth/reset-password?token=token", url);
    }

    [Fact]
    public void GenerateEmailChangeUrl_ReturnsCorrectUrl()
    {
        const string token = "token";
        var url = _generator.GenerateEmailChangeUrl(token);
        Assert.Equal("https://example.com/auth/verify-email-change?token=token", url);
    }
}
