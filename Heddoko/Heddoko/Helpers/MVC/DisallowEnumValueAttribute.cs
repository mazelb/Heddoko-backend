using System;
using System.ComponentModel.DataAnnotations;

namespace Heddoko.Helpers.MVC
{
    public class DisallowEnumValueAttribute : ValidationAttribute
    {
        public object DissallowedEnum { get; }

        public Type EnumType { get; private set; }

        public DisallowEnumValueAttribute(Type enumType, object dissallowedEnum)
        {
            if (!enumType.IsEnum)
                throw new ArgumentException("Type must be an enum", nameof(enumType));

            DissallowedEnum = dissallowedEnum;
            EnumType = enumType;
        }
        
        public override bool IsValid(object value)
        {
            return !DissallowedEnum.Equals(value);
        }
    }
}