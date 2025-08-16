using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Noo.Api.Auth;
using Noo.Api.Auth.Services;
using Noo.Api.Core.Config.Env;

namespace Noo.UnitTests.Auth;

public class AuthTokenServiceTests
{
	private static IAuthTokenService CreateSvc()
	{
		var secret = Convert.ToBase64String(new byte[32]); // deterministic test key
		var opts = Options.Create(new JwtConfig
		{
			Secret = secret,
			Issuer = "TestIssuer",
			Audience = "TestAudience",
			ExpireDays = 7
		});

		return new AuthTokenService(opts);
	}

	[Fact]
	public void EmailVerification_Token_Roundtrip()
	{
		var svc = CreateSvc();
		var userId = Ulid.NewUlid();

		var token = svc.GenerateEmailVerificationToken(userId);
		Assert.False(string.IsNullOrWhiteSpace(token));

		var ok = svc.ValidateEmailVerificationToken(token);
		Assert.True(ok);
	}

	[Fact]
	public void PasswordReset_Token_Roundtrip()
	{
		var svc = CreateSvc();
		var userId = Ulid.NewUlid();

		var token = svc.GeneratePasswordResetToken(userId);
		Assert.False(string.IsNullOrWhiteSpace(token));

		var parsed = svc.ValidatePasswordResetToken(token);
		Assert.NotNull(parsed);
		Assert.Equal(userId, parsed);
	}

	[Fact]
	public void EmailChange_Token_Roundtrip()
	{
		var svc = CreateSvc();
		var userId = Ulid.NewUlid();
		var newEmail = "new@example.com";

		var token = svc.GenerateEmailChangeToken(userId, newEmail);
		Assert.False(string.IsNullOrWhiteSpace(token));

		var parsedEmail = svc.ValidateEmailChangeToken(token);
		Assert.Equal(newEmail, parsedEmail);
	}
}
