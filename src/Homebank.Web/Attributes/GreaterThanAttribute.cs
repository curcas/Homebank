using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Homebank.Web.Attributes
{
    public class GreaterThanAttribute : ValidationAttribute
    {
        private readonly string _DependentProperty;

        public GreaterThanAttribute(string dependentProperty)
        {
            _DependentProperty = dependentProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dependentProperty = validationContext.ObjectType.GetProperty(_DependentProperty);
            var dependentValue = dependentProperty.GetValue(validationContext.ObjectInstance, null);

            if(Comparer<object>.Default.Compare(value, dependentValue) >= 1)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}