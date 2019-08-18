using IF.Core.Mvc;
using IF.Web.Mvc.FluentHtml.Base;

namespace IF.Web.Mvc.FluentHtml.Link
{
    public class ActionLinkBuilderBase<Element, Builder> : HtmlRouteableElementBuilder<Element, Builder>
        where Element : ActionLink
        where Builder : ActionLinkBuilderBase<Element, Builder>
    {

        public ActionLinkBuilderBase(ActionLink actionLink)
        {
            this.HtmlElement = actionLink as Element;
        }
        public Builder Text(string Text)
        {            
            this.HtmlElement.Text = Text;
            return this as Builder;
        }

        public Builder IconClassName(string IconClassName)
        {
            this.HtmlElement.IconClassName = IconClassName;
            return this as Builder;
        }

        public Builder QueryString(string QueryString)
        {
            this.HtmlElement.QueryString = QueryString;
            return this as Builder;
        }

        public Builder RenderType(ActionWidgetRenderType type)
        {
            this.HtmlElement.RenderType = type;
            return this as Builder;
        }
    }
}
