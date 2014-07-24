using System;
using System.Configuration;
using System.Runtime.InteropServices.WindowsRuntime;
using Homebank.Helpers;
using Homebank.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Homebank.Web.Attributes
{
	public class PasswordValidAttribute : ValidationAttribute
	{
		private readonly UserRepository _userRepository;

		public PasswordValidAttribute()
		{
			_userRepository = new UserRepository(new DatabaseContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString));
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null)
			{
				return null;
			}

			var usernameProperty = validationContext.ObjectType.GetProperty("Name");
			var username = Convert.ToString(usernameProperty.GetValue(validationContext.ObjectInstance, null));

			var user = _userRepository.Get(username);

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