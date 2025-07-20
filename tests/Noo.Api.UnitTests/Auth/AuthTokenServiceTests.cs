using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Noo.Api.Auth;
using Noo.Api.Auth.Services;
using Noo.Api.Core.Config.Env;
using Noo.Api.Core.Security.Authorization;
using Xunit;

namespace Noo.Api.UnitTests.Auth;

public class AuthTokenServiceTests
{
	private readonly AuthTokenService _service;

	public const string Secret = "/hD0quP8+eY++lpzfmnenifX1Gl5S0MVwDf0wZfEfBfnJ3S7bX4pfcGa+0Rly2meNv0rW0R0cvC/h+IZdDr5zw==";

	public AuthTokenServiceTests()
	{
		var config = new JwtConfig
		{
			Secret = Secret,
			Issuer = "issuer",
			Audience = "audience",
			ExpireDays = 1
		};
		_service = new AuthTokenService(Options.Create(config));
	}

	[Fact]
	public void GenerateAccessToken_ShouldContainClaims()
	{
		var payload = new AccessTokenPayload
		{
			UserId = Ulid.NewUlid(),
			SessionId = Ulid.NewUlid(),
			UserRole = UserRoles.Teacher,
			ExpiresAt = DateTime.UtcNow.AddDays(1)
		};

		var token = _service.GenerateAccessToken(payload);
		var handler = new JwtSecurityTokenHandler();
		var jwt = handler.ReadJwtToken(token);

		Assert.Contains(jwt.Claims, c => c.Type == ClaimTypes.Name && c.Value == nameof(AuthTokenType.Access));
		Assert.Contains(jwt.Claims, c => c.Type == ClaimTypes.NameIdentifier && c.Value == payload.UserId.ToString());
		Assert.Contains(jwt.Claims, c => c.Type == ClaimTypes.Sid && c.Value == payload.SessionId.ToString());
		Assert.Contains(jwt.Claims, c => c.Type == ClaimTypes.Role && c.Value == payload.UserRole.ToString());
	}

	[Fact]
	public void EmailVerificationToken_ShouldValidate()
	{
		var userId = Ulid.NewUlid();
		var token = _service.GenerateEmailVerificationToken(userId);
		Assert.True(_service.ValidateEmailVerificationToken(token));
	}

	[Fact]
	public void PasswordResetToken_ShouldValidate()
	{
		var userId = Ulid.NewUlid();
		var token = _service.GeneratePasswordResetToken(userId);
		var result = _service.ValidatePasswordResetToken(token);
		Assert.Equal(userId, result);
	}

	[Fact]
	public void EmailChangeToken_ShouldValidate()
	{
		var userId = Ulid.NewUlid();
		const string newEmail = "new@example.com";
		var token = _service.GenerateEmailChangeToken(userId, newEmail);
		var result = _service.ValidateEmailChangeToken(token);
		Assert.Equal(newEmail, result);
	}
}
