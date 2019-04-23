using System;
using Homebank.Core.Helpers;
using Homebank.Core.Repositories;
using System.ComponentModel.DataAnnotations;
using Homebank.Core.Interfaces.Repositories;

namespace Homebank.Web.Attributes
{
	public class PasswordValidAttribute : ValidationAttribute
	{
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var userRepository = (IUserRepository)validationContext.GetService(typeof(IUserRepository));

			if (value == null)
			{
                return ValidationResult.Success;
            }

			var usernameProperty = validationContext.ObjectType.GetProperty("Name");
			var username = Convert.ToString(usernameProperty.GetValue(validationContext.ObjectInstance, null));

			var user = userRepository.Get(username);

			if (user == null)
			{
                return ValidationResult.Success;
            }

			if (user.Password == StringHelpers.Hash((string) value + user.Salt))
			{
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
	}
}