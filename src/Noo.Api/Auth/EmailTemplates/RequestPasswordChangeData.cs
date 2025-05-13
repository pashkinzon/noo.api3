namespace Noo.Api.Auth.EmailTemplates;

public class RequestPasswordChangeData
{
    public string Name { get; set; } = string.Empty;

    public string ResetPasswordLink { get; set; } = string.Empty;
}
