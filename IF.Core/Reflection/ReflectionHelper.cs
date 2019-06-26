using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FOProjectTemplate.Core.Reflection
{
    public static class ReflectionHelper
    {
        public  static List<Type> FindDerivedObjects(string @namespace,Type type)
        {
            var assembly = Assembly.Load(@namespace);
            var list = FindDerivedTypes(assembly, type).ToList();
            return list;
        }

        public static IEnumerable<Type> FindDerivedTypes(Assembly assembly, Type baseType)
        {
            return assembly.GetTypes().Where(t => baseType.IsAssignableFrom(t))
                //.Where(g => g.BaseType.IsGenericType == true)
                ;
        }

        public static object GetGenericListInstance(Type objectType)
        {
            Type listType = typeof(List<>);

            Type[] listTypeGenericArguments = { objectType };

            Type genericType = listType.MakeGenericType(listTypeGenericArguments);

            object genericList = Activator.CreateInstance(genericType);

            return genericList;
        }

        public static void SetPropertyValueFromString(object target, string propertyName, string propertyValue)
        {
            PropertyInfo propertyInfo = target.GetType().GetProperty(propertyName);

            if (propertyInfo == null) return;

            Type propertyType = propertyInfo.PropertyType;

            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (propertyValue == null)
                {
                    propertyInfo.SetValue(target, null, null);
                    return;
                }

                propertyType = new NullableConverter(propertyInfo.PropertyType).UnderlyingType;
            }

            propertyInfo.SetValue(target, System.Convert.ChangeType(propertyValue, propertyType), null);
        }

        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(TSource source, Expression<Func<TSource, TProperty>> propertyLambda)
        {
            if (object.Equals(propertyLambda, null))
            {
                throw new NullReferenceException("Field is required");
            }

            MemberExpression expression = null;

            if (propertyLambda.Body is MemberExpression)
            {
                expression = (MemberExpression)propertyLambda.Body;
            }
            else if (propertyLambda.Body is UnaryExpression)
            {
                expression = (MemberExpression)((UnaryExpression)propertyLambda.Body).Operand;
            }
            else
            {
                const string Format = "Expression '{0}' not supported.";
                string message = string.Format(Format, propertyLambda);

                throw new ArgumentException(message, "Field");
            }

            PropertyInfo propInfo = expression.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyLambda.ToString()));

            Type type = typeof(TSource);

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a property that is not from type {1}.",
                    propertyLambda.ToString(),
                    type));

            return propInfo;

        }
    }
}
