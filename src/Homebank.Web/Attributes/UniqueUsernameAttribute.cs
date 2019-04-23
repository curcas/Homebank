using Homebank.Core.Interfaces.Repositories;
using Homebank.Core.Repositories;
using System.ComponentModel.DataAnnotations;

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
			var userRepository = (IUserRepository)validationContext.GetService(typeof(IUserRepository));

			if (!string.IsNullOrEmpty(_originalNamePropertyName))
			{
				var idProperty = validationContext.ObjectType.GetProperty(_originalNamePropertyName);
				var originalName = (string)idProperty.GetValue(validationContext.ObjectInstance, null);

				if (originalName == (string)value || userRepository.Get((string)value) == null)
				{
                    return ValidationResult.Success;
                }
			}

			if (userRepository.Get((string)value) == null)
			{
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
	}
}