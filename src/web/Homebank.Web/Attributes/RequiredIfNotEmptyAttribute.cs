using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
                return null;
            }

            if (!string.IsNullOrEmpty(dependentValue?.ToString()) && !string.IsNullOrEmpty(value?.ToString()))
            {
                return null;
            }

            return new ValidationResult(null);
        }
    }
}