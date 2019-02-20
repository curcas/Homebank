using System;
using System.Web;
using System.Web.Mvc;
using Homebank.Core.Repositories;
using System.ComponentModel.DataAnnotations;

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
			var categoryRepository = (CategoryRepository)DependencyResolver.Current.GetService(typeof(CategoryRepository));

			var idProperty = validationContext.ObjectType.GetProperty(_IdPropertyName);
			var categoryId = Convert.ToInt32(idProperty.GetValue(validationContext.ObjectInstance, null));

			if (categoryRepository.IsNameUnique(int.Parse(HttpContext.Current.User.Identity.Name), categoryId, (string)value))
			{
				return null;
			}

			return new ValidationResult(null);
		}
	}
}