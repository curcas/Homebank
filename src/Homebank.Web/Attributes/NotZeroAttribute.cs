using System.ComponentModel.DataAnnotations;
namespace Homebank.Web.Attributes
{
	public class NotZeroAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is int && (int)value != 0)
			{
				return null;
			}

			if (value is decimal && (decimal)value != 0)
			{
				return null;
			}

			return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
		}
	}
}