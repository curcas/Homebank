using System;
using Homebank.Core.Repositories;
using System.ComponentModel.DataAnnotations;
using Homebank.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Homebank.Web.Attributes
{
	public class AccountNameUniqueAttribute : ValidationAttribute
	{
		private readonly string _IdPropertyName;

		public AccountNameUniqueAttribute(string IdPropertyName)
		{
			_IdPropertyName = IdPropertyName;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
            var actionContextAccessor = (IActionContextAccessor)validationContext.GetService(typeof(IActionContextAccessor));
            var accountRepository = (IAccountRepository)validationContext.GetService(typeof(IAccountRepository));

			var idProperty = validationContext.ObjectType.GetProperty(_IdPropertyName);
			var accountId = Convert.ToInt32(idProperty.GetValue(validationContext.ObjectInstance, null));

            if (accountRepository.IsNameUnique(int.Parse(actionContextAccessor.ActionContext.HttpContext.User.Identity.Name), accountId, (string)value))
            {
            	return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
	}
}