using Microsoft.AspNetCore.Mvc.Rendering;

namespace IF.Web.Mvc.FluentHtml.HtmlForm
{
    public class HtmlFormBuilder : HtmlFormBuilderBase<HtmlForm, HtmlFormBuilder>
    {
        public HtmlFormBuilder(IHtmlHelper htmlHelper, int ModelId)
            //: base(htmlHelper, ModelId)
        {
            this.HtmlElement = new HtmlForm(htmlHelper, ModelId);
        }
    }
}
