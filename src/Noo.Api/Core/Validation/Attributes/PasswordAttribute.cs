using System.ComponentModel.DataAnnotations;


namespace Noo.Api.Core.Validation.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class PasswordAttribute : ValidationAttribute
{
    public PasswordAttribute()
    {
        ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.";
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string password)
        {
            return new ValidationResult("Invalid password format.");
        }

        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
        {
            return new ValidationResult(ErrorMessage);
        }

        if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsDigit) || !password.Any(ch => !char.IsLetterOrDigit(ch)))
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success!;
    }
}
