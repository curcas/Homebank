using System.ComponentModel.DataAnnotations;

namespace Homebank.Web.Attributes
{
    public class RequiredIfNotEmptyAttribute : ValidationAttribute
    {
        private readonly string _DependentProperty;

        public RequiredIfNotEmptyAttribute(string dependentProperty)
        {
            _DependentProperty = dependentProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dependentProperty = validationContext.ObjectType.GetProperty(_DependentProperty);
            var dependentValue = dependentProperty.GetValue(validationContext.ObjectInstance, null);

            if (string.IsNullOrEmpty(dependentValue?.ToString()))
            {
                return ValidationResult.Success;
            }

            if (!string.IsNullOrEmpty(dependentValue?.ToString()) && !string.IsNullOrEmpty(value?.ToString()))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}