using System;
using Homebank.Core.Repositories;
using System.ComponentModel.DataAnnotations;
using Homebank.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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
            var actionContextAccessor = (IActionContextAccessor)validationContext.GetService(typeof(IActionContextAccessor));
			var templateRepository = (ITemplateRepository)validationContext.GetService(typeof(ITemplateRepository));

			var templateProperty = validationContext.ObjectType.GetProperty(_IdPropertyName);
			var templateId = Convert.ToInt32(templateProperty.GetValue(validationContext.ObjectInstance, null));

			var accountProperty = validationContext.ObjectType.GetProperty("AccountId");
			var accountId = Convert.ToInt32(accountProperty.GetValue(validationContext.ObjectInstance, null));

            if (templateRepository.IsNameUnique(int.Parse(actionContextAccessor.ActionContext.HttpContext.User.Identity.Name), templateId, (string)value, accountId))
            {
            	return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
	}
}