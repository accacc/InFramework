using IF.Web.Mvc.Filter;
using IF.Web.Mvc.Session;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace IF.Web.Mvc.Filters
{



    public class KeepRequestFilter : ActionFilterAttribute
    {
        private Type requestType { get; set; }
        private KeepRequestType RequestType { get; set; }


        public KeepRequestFilter(Type RequestType, KeepRequestType IsFirstRequest = KeepRequestType.Alive)
        {
            this.requestType = RequestType;
            this.RequestType = IsFirstRequest;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (RequestType == KeepRequestType.First)
            {
                var requestInstance = Activator.CreateInstance(requestType);
                filterContext.HttpContext.Session.SetObject(requestType.Name,requestInstance);
            }
        }

        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{          
        //    if (RequestType == KeepRequestType.Last)
        //    {
        //        foreach (var item in filterContext.HttpContext.Session.Keys)
        //        {
        //            if (filterContext.HttpContext.Session.get.GetType() == requestType)
        //            {
        //                filterContext.HttpContext.Session.Remove(requestType.Name);
        //            }
        //        }

        //        for (int i = 0; i < filterContext.HttpContext.Session; i++)
        //        {
                    
        //        }
        //    }

        //}

    }
}
