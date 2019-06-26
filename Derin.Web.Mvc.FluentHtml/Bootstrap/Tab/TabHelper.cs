using IF.Core.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace IF.Web.Mvc.FluentHtml.Bootstrap.Tab
{
    public static class TabHelper
    {
        public static BootstrapTabBuilder TabSecure(HtmlHelper HtmlHelper,string mainObjectTabName, string mainObjectViewName, object model, object routeValues, IList<PermissionMapDto> childActions)
        {
            var builder = new BootstrapTabBuilder(new BootstrapTab(HtmlHelper));


            builder.Items(i => i.Add()
                .Text(mainObjectTabName)
                .Content(HtmlHelper.Partial(mainObjectViewName, model).ToHtmlString())
                .Active(true)

                );





            if (childActions != null && childActions.Any())
            {

                foreach (PermissionMapDto childAction in childActions)
                {
                    var route = new RouteValueDictionary(routeValues);

                    route.Add("PermissionMapId", childAction.Id);

                    builder.Items(i => i.Add()
                                       .Text(childAction.Name)
                                       .Load(childAction.ActionName, childAction.ControllerName, routeValues));
                }

            }

            return builder;
        }


        public static BootstrapTabBuilder TabSecure(HtmlHelper HtmlHelper, object routeValues, IList<PermissionMapDto> childActions)
        {
            var builder = new BootstrapTabBuilder(new BootstrapTab(HtmlHelper));

            if (childActions != null && childActions.Any())
            {

                for (int i = 0; i < childActions.Count; i++)

                {
                    var route = new RouteValueDictionary(routeValues);

                    route.Add("PermissionMapId", childActions[i].Id);

                    builder.Items(item =>
                    {

                        var link = item.Add();


                        link.Text(childActions[i].Name).Load(childActions[i].ActionName, childActions[i].ControllerName, route);

                        if (i == 0)
                        {
                            link.Active(true);
                        }
                    }
                    );
                }
            }

            return builder;
        }
    }
}
