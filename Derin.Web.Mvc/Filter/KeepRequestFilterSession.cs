using Derin.Core.Mvc.Filter;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Derin.Core.Mvc.Filters
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
                filterContext.HttpContext.Session[requestType.Name] = requestInstance;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            //if (RequestType == KeepRequestType.Pos)
            //{
            //    for (int i = 0; i < filterContext.Controller.TempData.Count; i++)
            //    {
            //        var currentRequestType = filterContext.Controller.TempData.ElementAt(i).Value.GetType();
                    
            //        //if (typeof(PosRequest).IsAssignableFrom(currentRequestType))
            //        //{
            //        //    filterContext.Controller.TempData.Keep(filterContext.Controller.TempData.ElementAt(i).Key);
            //        //}
            //    }
            //}
            //else
            if (RequestType == KeepRequestType.Last)
            {

                for (int i = 0; i < filterContext.HttpContext.Session.Count; i++)
                {
                    if (filterContext.HttpContext.Session[i].GetType() == requestType)
                    {
                        filterContext.HttpContext.Session.Remove(requestType.Name);
                    }
                }
            }

        }

    }
}
