using Derin.Core.Mvc.Session;
using IF.Core.Security;
using System;
using System.Web.Mvc;

namespace Derin.Core.Mvc.Filters
{

   

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public abstract class MvcAuthorization : FilterAttribute, IAuthorizationFilter, IInFrameworkAuthorization
    {

        public bool IsOnlyAuthenticated { get; set; }
        public string DependedActionName { get; set; }
        public bool DenyDummyUser { get; set; }
        public bool AllowAnonymous { get; set; }
        public string PermissionCode { get; set; }

        

        public abstract void OnAuthorization(AuthorizationContext filterContext);




    }
}
