using System;
using System.ComponentModel;
using System.Globalization;
using System.Web;
using IF.Core.Common;


namespace Derin.Core.Mvc.Cookie
{
    public class CookieContainer : ICookieContainer
    {
        private readonly HttpRequestBase _request;
        private readonly HttpResponseBase _response;

        public CookieContainer()
        {
            var httpContext = new HttpContextWrapper(HttpContext.Current);
            _request = httpContext.Request;
            _response = httpContext.Response;
        }

        public CookieContainer(HttpRequestBase request, HttpResponseBase response)
        {
            Guard.IsNotNull(request);
            Guard.IsNotNull(response);

            _request = request;
            _response = response;
        }

        #region ICookieContainer Members

        public bool Exists(string key)
        {
            Guard.IsNotEmpty(key);

            return _request.Cookies[key] != null;
        }

        public string GetValue(string key)
        {
            Guard.IsNotEmpty(key);

            HttpCookie cookie = _request.Cookies[key];
            return cookie != null ? cookie.Value : null;
        }

        public T GetValue<T>(string key)
        {
            string val = GetValue(key);

            if (val == null)
                return default(T);

            Type type = typeof(T);
            bool isNullable = type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
            if (isNullable)
                type = new NullableConverter(type).UnderlyingType;

            return (T)System.Convert.ChangeType(val, type, CultureInfo.InvariantCulture);
        }

        public void SetValue(string key, object value, DateTime expires)
        {
            Guard.IsNotEmpty(key);

            string strValue = CheckAndConvertValue(value);

            HttpCookie cookie = new HttpCookie(key, strValue) { Expires = expires };
            _response.Cookies.Set(cookie);
        }

        #endregion

        private static string CheckAndConvertValue(object value)
        {
            if (value == null)
                return null;

            if (value is string)
                return value.ToString();


            Type type = value.GetType();
            bool isTypeAllowed = false;

            if (type.IsValueType)
                isTypeAllowed = true;
            else
            {
                bool isNullable = type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
                if (isNullable)
                {
                    NullableConverter converter = new NullableConverter(type);
                    Type underlyingType = converter.UnderlyingType;
                    if (underlyingType.IsValueType)
                        isTypeAllowed = true;
                }
            }

            if (!isTypeAllowed)
                throw new NotSupportedException("Only value types and Nullable<ValueType> are allowed!");

            return (string)System.Convert.ChangeType(value, typeof(string), CultureInfo.InvariantCulture);
        }
    }
}
