using System;
using System.ComponentModel.DataAnnotations;

namespace Heddoko
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PosivitiveNumberAttribute : ValidationAttribute
    {
        private bool AllowNull { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (AllowNull
                && value == null)
            {
                return ValidationResult.Success;
            }

            if (value is decimal)
            {
                decimal currentValue = (decimal) value;

                if (currentValue > 0)
                {
                    return ValidationResult.Success;
                }
            }
            if (value is int)
            {
                int currentValue = (int) value;

                if (currentValue > 0)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}