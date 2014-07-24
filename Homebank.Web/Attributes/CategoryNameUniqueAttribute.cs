using System;
using System.Configuration;
using System.Web;
using Homebank.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Homebank.Web.Attributes
{
	public class CategoryNameUniqueAttribute : ValidationAttribute
	{
		private readonly CategoryRepository _categoryRepository;
		private readonly string _IdPropertyName;

		public CategoryNameUniqueAttribute(string IdPropertyName)
		{
			_categoryRepository = new CategoryRepository(new DatabaseContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString));
			_IdPropertyName = IdPropertyName;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var idProperty = validationContext.ObjectType.GetProperty(_IdPropertyName);
			var categoryId = Convert.ToInt32(idProperty.GetValue(validationContext.ObjectInstance, null));

			if (_categoryRepository.IsNameUnique(int.Parse(HttpContext.Current.User.Identity.Name), categoryId, (string)value))
			{
				return null;
			}

			return new ValidationResult(null);
		}
	}
}