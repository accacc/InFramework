//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace IF.Web.Mvc
//{
//    public class AjaxAwareRedirectResult : RedirectResult
//    {
//        public AjaxAwareRedirectResult(string url)
//            : base(url)
//        {
//        }

//        public override void ExecuteResult(ControllerContext context)
//        {
//            if (context.RequestContext.HttpContext.Request.IsAjaxRequest())
//            {
//                string destinationUrl = UrlHelper.GenerateContentUrl(Url, context.HttpContext);

//                JavaScriptResult result = new JavaScriptResult()
//                {
//                    Script = "window.location='" + destinationUrl + "';"
//                };
//                result.ExecuteResult(context);
//            }
//            else
//                base.ExecuteResult(context);
//        }
//    }
//}
