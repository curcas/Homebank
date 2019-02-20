using System;
using System.Web;
using System.Web.Mvc;
using Homebank.Core.Repositories;
using System.ComponentModel.DataAnnotations;

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
			var accountRepository = (AccountRepository)DependencyResolver.Current.GetService(typeof(AccountRepository));

			var idProperty = validationContext.ObjectType.GetProperty(_IdPropertyName);
			var accountId = Convert.ToInt32(idProperty.GetValue(validationContext.ObjectInstance, null));

			if (accountRepository.IsNameUnique(int.Parse(HttpContext.Current.User.Identity.Name), accountId, (string)value))
			{
				return null;
			}

			return new ValidationResult(null);
		}
	}
}