using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace Derin.Core.Mvc.Filters
{

    public enum KeepGridFilterOperationType
    {
        Get = 1,
        Set = 2,
    }

    public class KeepGridFilter : ActionFilterAttribute
    {
        private KeepGridFilterOperationType OperationType { get; set; }

        //private static ICookieContainer cookie = DependencyResolver.Current.GetService<ICookieContainer>();

        public KeepGridFilter(KeepGridFilterOperationType OperationType)
        {
            this.OperationType = OperationType;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            KeyValuePair<string, object> model = new KeyValuePair<string, object>();
            
            Type modelType = null;

            foreach (var parameter in filterContext.ActionParameters)
            {
                modelType = parameter.Value.GetType();

                if (modelType.BaseType.Name == "BaseFilteredGridModel`1")
                {
                    model = parameter;
                    break;
                }
            }

            if (model.Value != null)
            {
                switch (this.OperationType)
                {
                    case KeepGridFilterOperationType.Get:
                        Get(filterContext,model, modelType);
                        break;
                    case KeepGridFilterOperationType.Set:
                        Set(filterContext, model, modelType);
                        break;
                    default:
                        break;
                }



            }
        }

        private static void Set(ActionExecutingContext filterContext,KeyValuePair<string, object> model, Type modelType)
        {
            StringBuilder sb = new StringBuilder();

            var properties = modelType.GetProperties();

            foreach (var property in properties)
            {
                if (!property.PropertyType.IsValueType && property.PropertyType != typeof(string)) continue;
                if (property.PropertyType.IsEnum) continue;

                //sb.Append(property.Name + "=" + filterContext.HttpContext.Request[property.Name]);
                sb.Append(property.Name + "=" + property.GetValue(model.Value, null));
                sb.Append("&");
            }

            HttpCookie cookie = new HttpCookie(modelType.Name, sb.ToString());
            cookie.Expires = DateTime.Now.AddDays(30);
            filterContext.HttpContext.Response.SetCookie(cookie);
        }

        private static void Get(ActionExecutingContext filterContext,KeyValuePair<string, object> model, Type modelType)
        {
            var cookie = filterContext.HttpContext.Request.Cookies.Get(modelType.Name);
            string filterModelString = cookie != null ? cookie.Value : null;

            if (!String.IsNullOrEmpty(filterModelString))
            {
                var filterModelDictionary = filterModelString.Split('&')
                                                           .Select(p => p.Split('='))
                                                           .ToDictionary(p => p[0], p => p.Length > 1 ? p[1] : null);

                var properties = modelType.GetProperties();

                foreach (var property in properties)
                {
                    if (!property.PropertyType.IsValueType && property.PropertyType != typeof(string)) continue;
                    if (property.PropertyType.IsEnum) continue;
                    if (String.IsNullOrEmpty(filterModelDictionary[property.Name])) continue;

                    Type t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                    object safeValue = (filterModelDictionary[property.Name] == null) ? null : System.Convert.ChangeType(filterModelDictionary[property.Name], t);

                    property.SetValue(model.Value, safeValue, null);
                }
            }
        }

    }
}
