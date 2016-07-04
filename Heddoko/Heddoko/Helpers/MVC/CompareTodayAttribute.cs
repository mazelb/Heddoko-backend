using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DAL;

namespace Heddoko
{
    public enum CompareEquality
    {
        Greater,
        Less
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CompareTodayAttribute : ValidationAttribute
    {
        public CompareTodayAttribute(CompareEquality type)
        {
            Type = type;
        }

        public CompareEquality Type { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime)
            {
                DateTime currentValue = (DateTime)value;
                DateTime otherValue = DateTime.Now;

                switch (Type)
                {
                    case CompareEquality.Greater:
                        if (currentValue >= otherValue.StartOfDay())
                        {
                            return ValidationResult.Success;
                        }
                        break;
                    case CompareEquality.Less:
                        if (currentValue <= otherValue.EndOfDay())
                        {
                            return ValidationResult.Success;
                        }
                        break;
                }
            }
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}