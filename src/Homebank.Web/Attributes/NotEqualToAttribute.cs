using System.ComponentModel.DataAnnotations;

namespace Homebank.Web.Attributes
{
    public class NotEqualToAttribute : ValidationAttribute
    {
        private readonly string _DependentProperty;

        public NotEqualToAttribute(string dependentProperty)
        {
            _DependentProperty = dependentProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dependentProperty = validationContext.ObjectType.GetProperty(_DependentProperty);
            var dependentValue = dependentProperty.GetValue(validationContext.ObjectInstance, null);

            if(!value.Equals(dependentValue))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}