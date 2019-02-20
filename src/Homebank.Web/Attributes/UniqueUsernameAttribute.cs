using Homebank.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Homebank.Web.Attributes
{
	public class UniqueUsernameAttribute : ValidationAttribute
	{
		private readonly string _originalNamePropertyName;

		public UniqueUsernameAttribute(string originalNamePropertyName)
		{
			_originalNamePropertyName = originalNamePropertyName;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var userRepository = (UserRepository)DependencyResolver.Current.GetService(typeof(UserRepository));

			if (!string.IsNullOrEmpty(_originalNamePropertyName))
			{
				var idProperty = validationContext.ObjectType.GetProperty(_originalNamePropertyName);
				var originalName = (string)idProperty.GetValue(validationContext.ObjectInstance, null);

				if (originalName == (string)value || userRepository.Get((string)value) == null)
				{
					return null;
				}
			}

			if (userRepository.Get((string)value) == null)
			{
				return null;
			}

			return new ValidationResult(null);
		}
	}
}