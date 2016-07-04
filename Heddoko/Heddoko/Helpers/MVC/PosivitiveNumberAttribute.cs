using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Heddoko
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PosivitiveNumberAttribute : ValidationAttribute
    {
        public bool AllowNull { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (AllowNull
             && value == null)
            {
                return ValidationResult.Success;
            }

            if (value is Decimal)
            {
                decimal currentValue = (Decimal)value;

                if (currentValue > 0)
                {
                    return ValidationResult.Success;
                }
            }
            if (value is int)
            {
                int currentValue = (int)value;

                if (currentValue > 0)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}