using System.ComponentModel.DataAnnotations;

namespace Noo.Api.Core.Validation.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class CssVariableAttribute : ValidationAttribute
{
    public bool AllowNull { get; set; } = false;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (AllowNull && value == null)
        {
            return ValidationResult.Success;
        }

        if (StringValidation.IsCSSVariable(value))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("Invalid CSS variable");
    }
}
