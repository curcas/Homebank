using System;
using System.ComponentModel.DataAnnotations;
namespace Homebank.Web.Attributes
{
	public class DateRangeAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is DateTime)
			{
				var date = (DateTime) value;

				if (date >= new DateTime(1900, 1, 1) && date <= new DateTime(2100, 1, 1))
				{
					return null;
				}
			}

			return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
		}
	}
}