using System.ComponentModel.DataAnnotations;

namespace Homebank.Web.Attributes
{
	public class NotZeroAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is int && (int)value != 0)
			{
                return ValidationResult.Success;
            }

			if (value is decimal && (decimal)value != 0)
			{
                return ValidationResult.Success;
            }

			return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
		}
	}
}