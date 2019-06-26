using IF.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace IF.Core.Enum
{
    public static class EnumHelper
    {
        public static Dictionary<TEnum, string> GetEnumValues<TEnum>(Type resourceType)
        {
            bool isEnum = IsEnum<TEnum>();

            if (!isEnum)
                throw new ArgumentException("T must be an enumerated type");

            if (resourceType == null)
                resourceType = GetResourceType<TEnum>();

            var enumDictionary = new Dictionary<TEnum, string>();

            Type enumType = GetUnderlyingType<TEnum>();
            IEnumerable<TEnum> values = System.Enum.GetValues(enumType).Cast<TEnum>();

            foreach (var value in values)
            {
                string localizedString = GetLocalizedString<TEnum>(resourceType, value);
                enumDictionary.Add(value, localizedString);
            }

            return enumDictionary;
        }

        public static bool IsEnum<T>()
        {
            return GetUnderlyingType<T>().IsEnum;
        }

        public static Type GetUnderlyingType<T>()
        {
            Type realModelType = typeof(T);
            Type underlyingType = Nullable.GetUnderlyingType(realModelType);

            if (underlyingType != null)
                return underlyingType;

            return realModelType;
        }

        private static Type GetResourceType<TEnum>()
        {
            var localizationAttribute = GetUnderlyingType<TEnum>()
                .GetCustomAttributes(typeof(LocalizationEnumAttribute), false)
                .FirstOrDefault();

            if (localizationAttribute != null)
                return ((LocalizationEnumAttribute)localizationAttribute).ResourceClassType;

            return null;
        }

        public static string GetLocalizedString<TEnum>(Type resourceType, TEnum value)
        {
            Type type = GetUnderlyingType<TEnum>();
            string resourceKey = string.Format("{0}_{1}", type.Name, value.ToString());

            string stringValue = LookupResource(resourceType, resourceKey);

            if (stringValue == null)
            {
                stringValue = GetDescription<TEnum>(value);
            }

            return stringValue;
        }

        private static string GetDescription<TEnum>(object value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }


        public static string LookupResource(Type resourceType, string resourceKey)
        {
            if (resourceType == null)
                return null;

            PropertyInfo property = resourceType.GetProperties().FirstOrDefault(p => p.PropertyType == typeof(System.Resources.ResourceManager));

            if (property != null)
                return ((ResourceManager)property.GetValue(null, null)).GetString(resourceKey);

            return null;
        }

        public static TEnum GetEnumFromStringValue<TEnum>(string value)
        {
            bool isEnum = EnumHelper.IsEnum<TEnum>();

            if (!isEnum) throw new ArgumentException("T must be an enumerated type");

            TEnum result = default(TEnum);

            FieldInfo[] infos = typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (FieldInfo item in infos)
            {
                if (item.GetCustomAttributes(typeof(ObjectDescriptionAttribute), false).Select(p => (ObjectDescriptionAttribute)p).Where(p => p.Value.Equals(value)).Count() > 0)
                {
                    result = (TEnum)item.GetValue(typeof(TEnum));
                    break;
                }
                else if (item.Name.Equals(value))
                {
                    result = (TEnum)item.GetValue(typeof(TEnum));
                    break;
                }
            }
            return result;
        }

        public static string GetLocalizedStringFromStringValue<TEnum>(Type resourceType, string value)
        {
            bool isEnum = EnumHelper.IsEnum<TEnum>();

            if (!isEnum) throw new ArgumentException("T must be an enumerated type");

            return EnumHelper.GetLocalizedString<TEnum>(resourceType, EnumHelper.GetEnumFromStringValue<TEnum>(value));
        }

    }
}
