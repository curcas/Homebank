using System;
using Homebank.Core.Repositories;
using System.ComponentModel.DataAnnotations;
using Homebank.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Homebank.Web.Attributes
{
	public class CategoryNameUniqueAttribute : ValidationAttribute
	{
		private readonly string _IdPropertyName;

		public CategoryNameUniqueAttribute(string IdPropertyName)
		{
			_IdPropertyName = IdPropertyName;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
            var actionContextAccessor = (IActionContextAccessor)validationContext.GetService(typeof(IActionContextAccessor));
            var categoryRepository = (ICategoryRepository)validationContext.GetService(typeof(ICategoryRepository));

			var idProperty = validationContext.ObjectType.GetProperty(_IdPropertyName);
			var categoryId = Convert.ToInt32(idProperty.GetValue(validationContext.ObjectInstance, null));

            if (categoryRepository.IsNameUnique(int.Parse(actionContextAccessor.ActionContext.HttpContext.User.Identity.Name), categoryId, (string)value))
            {
            	return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
	}
}