using Homebank.Core.Interfaces.Repositories;
using Homebank.Core.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Homebank.Web.Attributes
{
	public class UsernameValidAttribute : ValidationAttribute
	{
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userRepository = (IUserRepository)validationContext.GetService(typeof(IUserRepository));

            if (userRepository.Get((string)value) != null)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
	}
}