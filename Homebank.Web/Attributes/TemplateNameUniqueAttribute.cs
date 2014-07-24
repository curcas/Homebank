using System;
using System.Configuration;
using System.Web;
using Homebank.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Homebank.Web.Attributes
{
	public class TemplateNameUniqueAttribute : ValidationAttribute
	{
		private readonly TemplateRepository _templateRepository;
		private readonly string _IdPropertyName;

		public TemplateNameUniqueAttribute(string IdPropertyName)
		{
			_templateRepository = new TemplateRepository(new DatabaseContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString));
			_IdPropertyName = IdPropertyName;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var templateProperty = validationContext.ObjectType.GetProperty(_IdPropertyName);
			var templateId = Convert.ToInt32(templateProperty.GetValue(validationContext.ObjectInstance, null));

			var accountProperty = validationContext.ObjectType.GetProperty("AccountId");
			var accountId = Convert.ToInt32(accountProperty.GetValue(validationContext.ObjectInstance, null));

			if (_templateRepository.IsNameUnique(int.Parse(HttpContext.Current.User.Identity.Name), templateId, (string)value, accountId))
			{
				return null;
			}

			return new ValidationResult(null);
		}
	}
}