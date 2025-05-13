namespace Noo.Api.Auth.Services;

public interface IAuthTokenService
{
    public string GenerateAccessToken(AccessTokenPayload payload);

    public string GenerateEmailVerificationToken(Ulid userId);

    public string GeneratePasswordResetToken(Ulid userId);

    public string GenerateEmailChangeToken(Ulid userId, string newEmail);

    public bool ValidateEmailVerificationToken(string token);

    public bool ValidatePasswordResetToken(string token);

    public string? ValidateEmailChangeToken(string token);
}
