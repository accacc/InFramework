using IF.Web.Mvc.FluentHtml.Base;
using System.Collections.Generic;
using System.Web.Mvc;
using IF.Web.Mvc.FluentHtml.Link;

namespace IF.Web.Mvc.FluentHtml.ContextMenu
{
    public class Menu: HtmlElement
    {

        public IEnumerable<ActionLink> Actions { get; set; }
        public Menu(HtmlHelper htmlHelper,IEnumerable<ActionLink> Actions,string Id) : base(htmlHelper)
        {
            this.Actions = Actions;
            this.Id = Id;
        }


    //    <div id = "inputsheet-context-menu" >
    //    < ul class="dropdown-menu" role="menu">
    //        <li>
    //            @Html.IF().ContextActionLink("TeamLeaders", "TeamLeaders", "InputSheet").IconClassName("icon-user").Render()
    //        </li>
    //        @*<li class="divider"> </li>*@
    //    </ul>
    //</div>
        

        public override MvcHtmlString CreateHtml()
        {
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("dropdown-menu");
            ul.Attributes.Add("role","menu");

            foreach (var action in this.Actions)
            {
                TagBuilder li = new TagBuilder("li");
                li.InnerHtml = action.Render().ToString();
                ul.InnerHtml += li.ToString(TagRenderMode.Normal);

            }

            this.Builder.InnerHtml = ul.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(this.Builder.ToString(TagRenderMode.Normal));
        }

        public override string GetTag()
        {
            return ("div");
        }
    }
}
