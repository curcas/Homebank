using System;
using System.Web;
using System.Web.Mvc;
using Homebank.Core.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Homebank.Web.Attributes
{
	public class TemplateNameUniqueAttribute : ValidationAttribute
	{
		private readonly string _IdPropertyName;

		public TemplateNameUniqueAttribute(string IdPropertyName)
		{
			_IdPropertyName = IdPropertyName;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var templateRepository = (TemplateRepository)DependencyResolver.Current.GetService(typeof(TemplateRepository));

			var templateProperty = validationContext.ObjectType.GetProperty(_IdPropertyName);
			var templateId = Convert.ToInt32(templateProperty.GetValue(validationContext.ObjectInstance, null));

			var accountProperty = validationContext.ObjectType.GetProperty("AccountId");
			var accountId = Convert.ToInt32(accountProperty.GetValue(validationContext.ObjectInstance, null));

			if (templateRepository.IsNameUnique(int.Parse(HttpContext.Current.User.Identity.Name), templateId, (string)value, accountId))
			{
				return null;
			}

			return new ValidationResult(null);
		}
	}
}