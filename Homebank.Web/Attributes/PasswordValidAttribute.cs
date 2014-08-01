using System;
using System.Web.Mvc;
using Homebank.Helpers;
using Homebank.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Homebank.Web.Attributes
{
	public class PasswordValidAttribute : ValidationAttribute
	{

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var userRepository = (UserRepository)DependencyResolver.Current.GetService(typeof(UserRepository));

			if (value == null)
			{
				return null;
			}

			var usernameProperty = validationContext.ObjectType.GetProperty("Name");
			var username = Convert.ToString(usernameProperty.GetValue(validationContext.ObjectInstance, null));

			var user = userRepository.Get(username);

			if (user == null)
			{
				return null;
			}

			if (user.Password == StringHelpers.Hash((string) value + user.Salt))
			{
				return null;
			}

			return new ValidationResult(null);
		}
	}
}