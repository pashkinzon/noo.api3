namespace Noo.Api.Auth.EmailTemplates;

public class EmailVerificationData
{
    public string Name { get; set; } = string.Empty;

    public string VerificationLink { get; set; } = string.Empty;
}
