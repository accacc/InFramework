using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Derin.Core.Reflection;

namespace Derin.DynamicData
{
    public static class NameValueCollectionExtensions
    {
        public static Dictionary<string, string> ToDictionary(this NameValueCollection source)
        {
            return source?.Cast<string>().Select(s => new { Key = s, Value = source.Get(s) }).ToDictionary(p => p.Key, p => p.Value);
        }

        public static T GetQueryValue<T>(this NameValueCollection queryString, string key, T defaultValue = default(T))
        {
            string stringValue = queryString[key];

            return !string.IsNullOrEmpty(stringValue) ? (T)TypeExtensions.ChangeType(stringValue, typeof(T)) : defaultValue;
        }
    }
}