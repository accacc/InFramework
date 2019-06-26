using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Globalization;

namespace Derin.Core.Mvc.Filters
{
    public class DateTimeFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.RequestType == "GET")
            {
                //filterContext.Controller.ViewData.ModelState

                foreach (var parameter in filterContext.ActionParameters)
                {
                    var properties = parameter.Value.GetType().GetProperties();

                    foreach (var property in properties)
                    {
                        if (!property.PropertyType.IsValueType) continue;

                        Type type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                        if (property.PropertyType == typeof(System.DateTime) || property.PropertyType == typeof(DateTime?))
                        {
                            DateTime dateTime;

                            if (DateTime.TryParse(filterContext.HttpContext.Request.QueryString[property.Name], CultureInfo.CurrentUICulture, DateTimeStyles.None, out dateTime))
                                property.SetValue(parameter.Value, dateTime,null);
                        }
                    }

                }
            }
        }
    }
}
