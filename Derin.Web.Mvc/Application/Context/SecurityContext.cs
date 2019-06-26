//using IF.Core.Cache;
//using IF.Core.Data;
//using Derin.Core.Mvc;
//using IF.Core.Security;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Web;
//using IF.Core.Mvc;

//namespace Derin.Core.Mvc.Application.Context
//{
//    public class SecurityContext: ISecurityContext
//    {

//        ICacheService cacheService;
//        ISecurityService securityService;
//        public SecurityContext(ICacheService cacheService, ISecurityService securityService)
//        {
//            this.cacheService = cacheService;
//            this.securityService = securityService;
//        }

//        public IEnumerable<PermissionDto> Permissions()
//        {

//            return cacheService.Get<PermissionListResponse>("PermissionList",
//                () => securityService.GetPermissionList(new PermissionListRequest())).Permissions;
//        }

//        public IEnumerable<PermissionMapDto> PermissionMaps()
//        {
//            return cacheService.Get<PermissionMapListResponse>("PermissionMapList",
//                () => securityService.GetPermissionMapList(new PermissionMapListRequest())).PermissionMaps;

//        }

      

//        public IEnumerable<PermissionMapDto> PermissionMapsTree(int[] filter = null)
//        {
//            var maps = this.PermissionMaps();

//            var tree = TreeHelper.GetAvailableItems(maps, filter);

//            return TreeHelper.ListToTree(tree, null);
//        }

//        public IEnumerable<PermissionDto> PermissionTree(int[] filter = null)
//        {
//            var maps = this.Permissions();

//            var tree = TreeHelper.GetAvailableItems(maps, filter);

//            return TreeHelper.ListToTree(tree, null);
//        }

//        public string ControllerName
//        {
//            get { return HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString().ToLower(new CultureInfo("en-US", false)); }
//        }

//        public string ActionName
//        {
//            get { return HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString().ToLower(new CultureInfo("en-US", false)); }
//        }

//        public PermissionMapDto CurrentAction()
//        {


//            PermissionMapDto currentAction = null;

//            string actionName = HttpContext.Current.Session["ActionName"].ToString().ToLower(new CultureInfo("en-US", false));
//            string controllerName = HttpContext.Current.Session["ControllerName"].ToString().ToLower(new CultureInfo("en-US", false));

//            currentAction = this.Get(actionName, controllerName);

//            return currentAction;

//        }


//        public IList<PermissionMapDto> CurrentActionChilds(ActionWidgetType widgetType)
//        {

//            var currentAction = this.CurrentAction();

//            if (currentAction != null && currentAction.Childs != null && currentAction.Childs.Any())
//            {
//                return currentAction.Childs.Where(a => a.WidgetType == (int)widgetType).ToList();
//            }
//            else
//            {
//                return null;
//            }


//        }


//        private PermissionMapDto Get(string actionName, string controllerName)
//        {
//            var permission = this.PermissionMaps().Where(a => a.ActionName == actionName && a.ControllerName == controllerName).FirstOrDefault();

//            if (permission == null) return permission;

//            permission.Childs = this.PermissionMaps().Where(p => p.ParentId == permission.Id).ToList();

//            return permission;
//        }

        

      
//    }
//}
