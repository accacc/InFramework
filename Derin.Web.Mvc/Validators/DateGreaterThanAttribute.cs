using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Derin.Web.Mvc.Validators
{
    public sealed class DateGreaterThanAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly bool allowEqualDates;
        private const string defaultErrorMessage = "'{0}' must be greater than '{1}'";
        private string basePropertyName;

        public DateGreaterThanAttribute(string basePropertyName, bool allowEqualDates = false)
            : base(defaultErrorMessage)
        {
            this.basePropertyName = basePropertyName;
            this.allowEqualDates = allowEqualDates;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(defaultErrorMessage, name, basePropertyName);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var basePropertyInfo = validationContext.ObjectType.GetProperty(basePropertyName);
            var fromDate = (DateTime)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);
            var toDate = (DateTime)value;

            if (fromDate < toDate)
            {
                return ValidationResult.Success;
            }

            if (fromDate == toDate)
            {
                if (this.allowEqualDates)
                {
                    return ValidationResult.Success;
                }
            }


            var message = FormatErrorMessage(validationContext.DisplayName);
            return new ValidationResult(message);

        }


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules
            (ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationType = "greaterthan";
            rule.ValidationParameters.Add("otherfield", basePropertyName);
            rule.ValidationParameters.Add("allowequaldates", this.allowEqualDates);
            yield return rule;
        }
    }


}
