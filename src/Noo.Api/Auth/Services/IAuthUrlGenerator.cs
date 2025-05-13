namespace Noo.Api.Auth.Services;

public interface IAuthUrlGenerator
{

    public string GenerateEmailVerificationUrl(string token);

    public string GeneratePasswordResetUrl(string token);

    public string GenerateEmailChangeUrl(string token);
}
