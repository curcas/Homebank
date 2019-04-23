using System;
using System.ComponentModel.DataAnnotations;

namespace Homebank.Web.Attributes
{
	public class DateRangeAttribute : ValidationAttribute
	{
        public bool Nullable { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
            if(Nullable && value == null)
            {
                return ValidationResult.Success;
            }

			if (value is DateTime || value is DateTime?)
			{
				var date = (DateTime) value;

				if (date >= new DateTime(1900, 1, 1) && date <= new DateTime(2100, 1, 1))
				{
                    return ValidationResult.Success;
                }
			}

			return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
		}
	}
}