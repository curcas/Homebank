using System;
using System.Configuration;
using Homebank.Entities;
using Homebank.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Homebank.Web.Attributes
{
	public class UniqueUsernameAttribute : ValidationAttribute
	{
		private readonly UserRepository _userRepository;
		private readonly string _originalNamePropertyName;

		public UniqueUsernameAttribute(string originalNamePropertyName)
		{
			_userRepository = new UserRepository(new DatabaseContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString));
			_originalNamePropertyName = originalNamePropertyName;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (!string.IsNullOrEmpty(_originalNamePropertyName))
			{
				var idProperty = validationContext.ObjectType.GetProperty(_originalNamePropertyName);
				var originalName = (string)idProperty.GetValue(validationContext.ObjectInstance, null);

				if (originalName == (string) value || _userRepository.Get((string) value) == null)
				{
					return null;
				}
			}

			if (_userRepository.Get((string) value) == null)
			{
				return null;
			}

			return new ValidationResult(null);
		}
	}
}