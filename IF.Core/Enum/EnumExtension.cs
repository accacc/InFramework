using IF.Core.Attributes;
using System;
using System.Reflection;

namespace IF.Core.Enum
{
    public static class EnumExtension
    {
        public static string GetStringValue(this System.Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            ObjectDescriptionAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(ObjectDescriptionAttribute), false) as ObjectDescriptionAttribute[];
            return attribs.Length > 0 ? attribs[0].Value : null;
        }

    }
}
