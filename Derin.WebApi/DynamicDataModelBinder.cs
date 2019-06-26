using IF.DynamicData;
using System;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace Derin.WebApi
{
    public class DynamicDataJsonModelBinder : IModelBinder
    {
        

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException(nameof(actionContext));
            }

            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            string content = actionContext.Request.Content.ReadAsStringAsync().Result;

            bindingContext.Model = DynamicDataHelper.Parse(content);

            return true;
        }

        
    }

    public class DynamicDataQueryStringModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException(nameof(actionContext));
            }

            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var queryString = HttpUtility.ParseQueryString(actionContext.Request.RequestUri.Query);

            bindingContext.Model = DynamicDataHelper.Parse<DynamicDataRequest>(queryString);


            return true;
        }
        
    }
}
