using System;
using System.Configuration;
using System.Web;
using Homebank.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Homebank.Web.Attributes
{
	public class AccountNameUniqueAttribute : ValidationAttribute
	{
		private readonly AccountRepository _accountRepository;
		private readonly string _IdPropertyName;

		public AccountNameUniqueAttribute(string IdPropertyName)
		{
			_accountRepository = new AccountRepository(new DatabaseContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString));
			_IdPropertyName = IdPropertyName;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var idProperty = validationContext.ObjectType.GetProperty(_IdPropertyName);
			var accountId = Convert.ToInt32(idProperty.GetValue(validationContext.ObjectInstance, null));

			if (_accountRepository.IsNameUnique(int.Parse(HttpContext.Current.User.Identity.Name), accountId, (string)value))
			{
				return null;
			}

			return new ValidationResult(null);
		}
	}
}