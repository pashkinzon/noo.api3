using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Noo.Api.Core.Config.Env;
using Noo.Api.Core.Utils.DI;

namespace Noo.Api.Auth.Services;

[RegisterScoped(typeof(IAuthTokenService))]
public class AuthTokenService : IAuthTokenService
{
    private readonly TimeSpan _verificationTokenExpiration = TimeSpan.FromDays(1);

    private readonly TimeSpan _passwordResetTokenExpiration = TimeSpan.FromHours(1);

    private readonly TimeSpan _emailChangeTokenExpiration = TimeSpan.FromHours(1);

    private readonly JwtConfig _jwtConfig;

    public AuthTokenService(IOptions<JwtConfig> jwtConfig)
    {
        _jwtConfig = jwtConfig.Value;
    }

    public string GenerateAccessToken(AccessTokenPayload payload)
    {
        return GenerateToken([
            new Claim(ClaimTypes.Name, nameof(AuthTokenType.Access)),
            new Claim(ClaimTypes.NameIdentifier, payload.UserId.ToString()),
            new Claim(ClaimTypes.Sid, payload.SessionId.ToString()),
            new Claim(ClaimTypes.Role, payload.UserRole.ToString()),
        ], TimeSpan.FromDays(_jwtConfig.ExpireDays));
    }

    public string GenerateEmailVerificationToken(Ulid userId)
    {
        return GenerateToken([
            new Claim(ClaimTypes.Name, nameof(AuthTokenType.EmailVerification)),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        ], _verificationTokenExpiration);
    }

    public string GeneratePasswordResetToken(Ulid userId)
    {
        return GenerateToken([
            new Claim(ClaimTypes.Name, nameof(AuthTokenType.PasswordReset)),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        ], _passwordResetTokenExpiration);
    }

    public string GenerateEmailChangeToken(Ulid userId, string newEmail)
    {
        return GenerateToken([
            new Claim(ClaimTypes.Name, nameof(AuthTokenType.EmailChange)),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Email, newEmail),
        ], _emailChangeTokenExpiration);
    }

    public bool ValidateEmailVerificationToken(string token)
    {
        return ValidateToken(token, AuthTokenType.EmailVerification) != null;
    }

    public Ulid? ValidatePasswordResetToken(string token)
    {
        var principal = ValidateToken(token, AuthTokenType.PasswordReset);

        if (principal == null)
        {
            return null;
        }

        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !Ulid.TryParse(userIdClaim.Value, out var userId))
        {
            return null;
        }

        return userId;
    }

    /// <summary>
    /// Validates the email change token.
    /// </summary>
    /// <param name="token"></param>
    /// <returns>The new email to change to or null if validation fails</returns>
    public string? ValidateEmailChangeToken(string token)
    {
        var principal = ValidateToken(token, AuthTokenType.EmailChange);

        if (principal == null)
        {
            return null;
        }

        return principal.FindFirst(ClaimTypes.Email)?.Value;
    }

    private string GenerateToken(Claim[] claims, TimeSpan expiration)
    {
        var key = _jwtConfig.GetSymmetricSecurityKey();

        var token = new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(expiration),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private ClaimsPrincipal? ValidateToken(string token, AuthTokenType tokenType)
    {
        var key = _jwtConfig.GetSymmetricSecurityKey();

        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = true,
            ValidIssuer = _jwtConfig.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwtConfig.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

            if (validatedToken is JwtSecurityToken jwt)
            {
                var userIdClaim = principal.FindFirst("userId");
                var typeClaim = principal.FindFirst("type");

                if (userIdClaim != null
                    && typeClaim != null
                    && tokenType.ToString() == typeClaim.Value
                    && Ulid.TryParse(userIdClaim.Value, out _))
                {
                    return principal;
                }

                return null;
            }
        }
        catch (Exception)
        {
            return null;
        }

        return null;
    }
}
