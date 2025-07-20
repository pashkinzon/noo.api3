using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Noo.Api.Auth.DTO;
using Noo.Api.Auth.Services;
using Noo.Api.Core.Config.Env;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Security;
using Noo.Api.Core.Security.Authorization;
using Noo.Api.Sessions.Services;
using Noo.Api.Users.Models;
using Noo.Api.Users.Services;
using Xunit;

namespace Noo.Api.UnitTests.Auth;

public class AuthServiceTests
{
	private AuthService CreateService(Mock<IUserService> userServiceMock)
	{
		var tokenService = new Mock<IAuthTokenService>();
		var emailService = new Mock<IAuthEmailService>();
		var urlGenerator = new Mock<IAuthUrlGenerator>();
		var hashService = new Mock<IHashService>();
		var sessionService = new Mock<ISessionService>();
		var httpContextAccessor = new Mock<IHttpContextAccessor>();
		httpContextAccessor.SetupGet(x => x.HttpContext).Returns(new DefaultHttpContext());

		var config = new JwtConfig
		{
			Secret = AuthTokenServiceTests.Secret,
			Issuer = "issuer",
			Audience = "audience",
			ExpireDays = 1
		};

		return new AuthService(
			tokenService.Object,
			emailService.Object,
			urlGenerator.Object,
			userServiceMock.Object,
			hashService.Object,
			Options.Create(config),
			sessionService.Object,
			httpContextAccessor.Object
		);
	}

	[Fact]
	public async Task CheckUsernameAsync_ReturnsExpectedValue()
	{
		var userService = new Mock<IUserService>();
		userService.Setup(x => x.UserExistsAsync("taken", null)).ReturnsAsync(true);
		userService.Setup(x => x.UserExistsAsync("free", null)).ReturnsAsync(false);
		var service = CreateService(userService);

		var taken = await service.CheckUsernameAsync("taken");
		var free = await service.CheckUsernameAsync("free");

		Assert.False(taken);
		Assert.True(free);
	}

	[Fact]
	public async Task RegisterAsync_WhenUserExists_ThrowsAlreadyExistsException()
	{
		var userService = new Mock<IUserService>();
		userService.Setup(x => x.UserExistsAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
		var service = CreateService(userService);

		var dto = new RegisterDTO { Username = "user", Email = "e@e", Name = "n", Password = "p" };
		await Assert.ThrowsAsync<AlreadyExistsException>(() => service.RegisterAsync(dto));
	}

	[Fact]
	public async Task RequestPasswordResetAsync_UserNotFound_ThrowsNotFoundException()
	{
		var userService = new Mock<IUserService>();
		userService.Setup(x => x.GetUserByUsernameOrEmailAsync("email")).ReturnsAsync((UserModel?)null);
		var service = CreateService(userService);

		await Assert.ThrowsAsync<NotFoundException>(() => service.RequestPasswordResetAsync("email"));
	}

	[Fact]
	public async Task ConfirmEmailChangeAsync_InvalidToken_ThrowsUnauthorizedException()
	{
		var user = new UserModel { Id = Ulid.NewUlid() };
		var userService = new Mock<IUserService>();
		userService.Setup(x => x.GetUserByIdAsync(user.Id)).ReturnsAsync(user);
		var tokenService = new Mock<IAuthTokenService>();
		tokenService.Setup(x => x.ValidateEmailChangeToken("bad")).Returns((string?)null);

		var emailService = new Mock<IAuthEmailService>();
		var urlGenerator = new Mock<IAuthUrlGenerator>();
		var hashService = new Mock<IHashService>();
		var sessionService = new Mock<ISessionService>();
		var httpContextAccessor = new Mock<IHttpContextAccessor>();
		httpContextAccessor.SetupGet(x => x.HttpContext).Returns(new DefaultHttpContext());
		var config = new JwtConfig
		{
			Secret = "supersecretsecret1234567890",
			Issuer = "issuer",
			Audience = "audience",
			ExpireDays = 1
		};

		var service = new AuthService(
			tokenService.Object,
			emailService.Object,
			urlGenerator.Object,
			userService.Object,
			hashService.Object,
			Options.Create(config),
			sessionService.Object,
			httpContextAccessor.Object
		);

		await Assert.ThrowsAsync<UnauthorizedException>(() => service.ConfirmEmailChangeAsync(user.Id, "bad"));
	}
}
