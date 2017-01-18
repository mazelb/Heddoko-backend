/**
 * @file CompareTodayAttribute.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.ComponentModel.DataAnnotations;
using DAL;

namespace Heddoko
{
    public enum CompareEquality
    {
        Greater,
        Less
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CompareTodayAttribute : ValidationAttribute
    {
        public CompareTodayAttribute(CompareEquality type)
        {
            Type = type;
        }

        private CompareEquality Type { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime)
            {
                DateTime currentValue = (DateTime) value;
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