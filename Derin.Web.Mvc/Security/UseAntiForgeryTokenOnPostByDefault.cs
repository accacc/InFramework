using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Derin.Core.Mvc.Security
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class UseAntiForgeryTokenOnPostByDefault : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (ShouldValidateAntiForgeryTokenManually(filterContext))
            {
                var authorizationContext = new AuthorizationContext(filterContext.Controller.ControllerContext,filterContext.ActionDescriptor);

                new ValidateAntiForgeryTokenAttribute().OnAuthorization(authorizationContext);
            }

            base.OnActionExecuting(filterContext);
        }

        private bool ShouldValidateAntiForgeryTokenManually(ActionExecutingContext filterContext)
        {
            var httpMethod = filterContext.HttpContext.Request.HttpMethod;

            if (httpMethod != "POST") return false;

            var antiForgeryAttributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(ValidateAntiForgeryTokenAttribute), false);

            if (antiForgeryAttributes.Length > 0) return false;

            var ignoreAntiForgeryAttributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(BypassAntiForgeryTokenAttribute), false);

            if (ignoreAntiForgeryAttributes.Length > 0) return false;

            return true;
        }
    }



    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ValidateAntiForgeryTokenOnAllPosts : AuthorizeAttribute
    {
        public const string HTTP_HEADER_NAME = "x-RequestVerificationToken";

        public static string GetAntiForgeryToken()
        {
            string cookieToken, formToken;
            AntiForgery.GetTokens(null, out cookieToken, out formToken);
            return cookieToken + ":" + formToken;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.ActionName == "Login")
                return;

            var request = filterContext.HttpContext.Request;

            //  Only validate POSTs
            if (request.HttpMethod == WebRequestMethods.Http.Post)
            {

                var headerTokenValue = request.Headers[HTTP_HEADER_NAME];

                // Ajax POSTs using jquery have a header set that defines the token.
                // However using unobtrusive ajax the token is still submitted normally in the form.
                // if the header is present then use it, else fall back to processing the form like normal
                if (headerTokenValue != null)
                {
                    var antiForgeryCookie = request.Cookies[AntiForgeryConfig.CookieName];

                    var cookieValue = antiForgeryCookie != null
                        ? antiForgeryCookie.Value
                        : null;

                    AntiForgery.Validate(cookieValue, headerTokenValue);
                }
                else
                {
                    new ValidateAntiForgeryTokenAttribute()
                        .OnAuthorization(filterContext);
                }
            }
        }
    }

    
}
