//using IF.Web.Mvc.Filter;
//using System;
//using System.Linq;
//using System.Web.Mvc;

//namespace IF.Web.Mvc.Filters
//{
   


//    public class KeepRequestFilterTempData : ActionFilterAttribute
//    {
//        private Type requestType { get; set; }
//        private KeepRequestType RequestType { get; set; }


//        public KeepRequestFilterTempData(Type RequestType, KeepRequestType IsFirstRequest = KeepRequestType.Alive)
//        {
//            this.requestType = RequestType;
//            this.RequestType = IsFirstRequest;
//        }

//        public override void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            if (RequestType == KeepRequestType.First)
//            {
//                var requestInstance = Activator.CreateInstance(requestType);
//                filterContext.Controller.TempData[requestType.Name] = requestInstance;
//            }
//        }

//        public override void OnActionExecuted(ActionExecutedContext filterContext)
//        {

//            //if (RequestType == KeepRequestType.Pos)
//            //{
//            //    for (int i = 0; i < filterContext.Controller.TempData.Count; i++)
//            //    {
//            //        var currentRequestType = filterContext.Controller.TempData.ElementAt(i).Value.GetType();
                    
//            //        //if (typeof(PosRequest).IsAssignableFrom(currentRequestType))
//            //        //{
//            //        //    filterContext.Controller.TempData.Keep(filterContext.Controller.TempData.ElementAt(i).Key);
//            //        //}
//            //    }
//            //}
//            //else
//            if (RequestType != KeepRequestType.Last)
//            {

//                for (int i = 0; i < filterContext.Controller.TempData.Count; i++)
//                {
//                    if (filterContext.Controller.TempData.ElementAt(i).Value.GetType() == requestType)
//                    {
//                        filterContext.Controller.TempData.Keep(filterContext.Controller.TempData.ElementAt(i).Key);
//                    }
//                }
//            }

//        }

//    }
//}
