using System.ComponentModel.DataAnnotations;

namespace Noo.Api.Core.Validation.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class RichTextAttribute : ValidationAttribute
{
    public bool AllowEmpty { get; set; } = false;

    public bool AllowNull { get; set; } = false;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            if (AllowNull)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Rich text cannot be null");
        }

        if (AllowEmpty && RichTextValidation.IsNonEmptyRichText(value))
        {
            return ValidationResult.Success;
        }

        if (RichTextValidation.IsRichText(value))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("Invalid rich text");
    }
}
