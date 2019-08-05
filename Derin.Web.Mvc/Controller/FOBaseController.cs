using Derin.Core.Mvc.ActionResult;
using Derin.Core.Mvc.Exception;
using IF.Core.Configuration;
using IF.Core.Data;
using IF.Core.Exception;
using System.IO;
using System.Threading;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Derin.Core.Mvc.Controller
{

    public class IFBaseController : System.Web.Mvc.Controller
    {


        public ICultureSettingsService cultureSettingsService = DependencyResolver.Current.GetService<ICultureSettingsService>();
        



        public bool IsModelStateHandlingActive { get; set; }
        public IFBaseController()
        {
            this.IsModelStateHandlingActive = true;
        }



        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = this.cultureSettingsService.SetCultureSettings();

            base.Initialize(requestContext);
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            AntiForgeryConfig.SuppressXFrameOptionsHeader = true;

            if (!filterContext.IsChildAction)
            {
                this.Session["ActionName"] = filterContext.ActionDescriptor.ActionName;
                this.Session["ControllerName"] = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            }


            if (filterContext.HttpContext.Request.HttpMethod == "POST" && !ModelState.IsValid && IsModelStateHandlingActive)
            {
                throw new ModelStateException(ModelState);
            }

            base.OnActionExecuting(filterContext);
        }




        public RedirectResult AjaxRedirect(string url)
        {
            return new AjaxAwareRedirectResult(url);
        }

        public EmptyResult OperationResult(OperationType operationType = OperationType.Success)
        {
            string message = "Unknown";

            switch (operationType)
            {
                case OperationType.Insert:
                    message = IF.Core.Resources.V2.RecordAdded;
                    break;
                case OperationType.Update:
                    message = IF.Core.Resources.V2.RecordUpdated;
                    break;
                case OperationType.Delete:
                    message = IF.Core.Resources.V2.RecordDeleted;
                    break;
                case OperationType.Success:
                    message = IF.Core.Resources.V2.OperationSuccessful;
                    break;
                default:
                    message = IF.Core.Resources.V2.OperationSuccessful;
                    break;
            }


            this.ShowMessage(MessageType.Success, message);

            return new EmptyResult();
        }


        public void ShowMessage(OperationType operationType = OperationType.Success)
        {
            string message = "Unknown";

            switch (operationType)
            {
                case OperationType.Insert:
                    message = IF.Core.Resources.V2.RecordAdded;
                    break;
                case OperationType.Update:
                    message = IF.Core.Resources.V2.RecordUpdated;
                    break;
                case OperationType.Delete:
                    message = IF.Core.Resources.V2.RecordDeleted;
                    break;
                case OperationType.Success:
                    message = IF.Core.Resources.V2.OperationSuccessful;
                    break;
                default:
                    message = IF.Core.Resources.V2.OperationSuccessful;
                    break;
            }


            this.ShowMessage(MessageType.Success, message);

        }

        public void ShowMessage(MessageType messageType, string message, bool showAfterRedirect = false)
        {
            var messageTypeKey = messageType.ToString();

            if (showAfterRedirect)
            {
                this.TempData[messageTypeKey] = message;
            }
            else
            {
                this.ViewData[messageTypeKey] = message;
            }
        }


        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {

                var viewData = filterContext.Controller.ViewData;
                var tempData = filterContext.Controller.TempData;
                var response = filterContext.HttpContext.Response;




                foreach (var messageType in System.Enum.GetNames(typeof(MessageType)))
                {
                    //var message = viewData.ContainsKey(messageType) ? viewData[messageType] : null;

                    var message = viewData.ContainsKey(messageType)
                                ? viewData[messageType]
                                : tempData.ContainsKey(messageType)
                                    ? tempData[messageType]
                                    : null;

                    if (message != null)
                    {
                        var result = new JsonResultEntry();
                        result.AddMessage(message.ToString());
                        response.AddHeader("X-Message-Type", messageType.ToLower());
                        response.AddHeader("X-Message", new JavaScriptSerializer().Serialize(result));
                        return;
                    }
                }

            }

        }

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            filterContext.ExceptionHandled = true;

            if (filterContext.Exception != null)
            {

                JsonResultEntry result = GetExceptionMessage(filterContext);


                if (result.IsRedirecToLogout)
                {
                    RedirectToLogut(filterContext);
                }
                else
                {

                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ViewResult viewResult = new ViewResult();
                        viewResult.ViewName = result.TemplateViewName;
                        viewResult.MasterName = result.TemplateMasterViewName;
                        viewResult.ViewBag.ErrorMessages = result;
                        filterContext.Result = viewResult;
                    }

                }

                string actionName = filterContext.RouteData.Values["action"].ToString();
                string controllerName = filterContext.RouteData.Values["controller"].ToString();

                if (!(filterContext.Exception is BusinessException) && !(filterContext.Exception is ModelStateException))
                {
                    try
                    {
                        var exception = filterContext.Exception;

                        while (exception.InnerException != null)
                            exception = exception.InnerException;

                        //frameworkServices.LogService.Error("ACTION NAME : " + actionName + Environment.NewLine + "CONTROLLER NAME : " + controllerName + Environment.NewLine + filterContext.Exception.Message);

                    }
                    catch
                    {

                    }


                }
            }
        }

        private JsonResultEntry GetExceptionMessage(ExceptionContext filterContext)
        {
            JsonResultEntry result = new JsonResultEntry();

            result.TemplateViewName = "~/Views/Shared/Error.cshtml";
            result.TemplateMasterViewName = "~/Views/Shared/_ErrorLayout.cshtml";

            if (filterContext.Exception is BusinessException)
            {
                result.AddException(filterContext.Exception);

            }
            else if (filterContext.Exception is ModelStateException)
            {
                ModelStateException exception = (ModelStateException)filterContext.Exception;
                result.AddModelState(exception.ModelStateDictionary);
            }
            //else if (filterContext.Exception is HttpAntiForgeryException)
            //{
            //    result.TemplateViewName = "~/Views/Shared/_OutBoundRequest.cshtml";
            //    result.AddMessage("OutBoundRequest");
            //    result.IsRedirecToLogout = true;

            //}
            //else if (filterContext.Exception is ArgumentException)
            //{
            //    result.TemplateViewName = "~/Views/Shared/_NotValid.cshtml";
            //    result.AddMessage("_NotValidRequest");
            //    result.IsRedirecToLogout = true;
            //}
            else
            {
                result.AddMessage(filterContext.Exception.Message);
            }

            return result;

        }

        private static void RedirectToLogut(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new AjaxAwareRedirectResult("~/saml/Logout" + "?returnUrl=" + filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.RawUrl));
            }
            else
            {

                filterContext.Result = new RedirectResult("~/saml/Logout" + "?returnUrl=" + filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.RawUrl));
            }
        }


        public void SetSessionRequest<T>(object data) where T : KeepData
        {
            this.HttpContext.Session[typeof(T).Name] = data;
        }


        private void SetSessionRequest(object data)
        {
            this.HttpContext.Session[data.GetType().Name] = data;
        }

        public T GetSessionRequest<T>() where T : KeepData
        {
            T request = this.HttpContext.Session[typeof(T).Name] as T;
            return request;
        }



        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
