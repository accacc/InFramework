using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.HtmlForm
{
    public class HtmlFormBuilder : HtmlFormBuilderBase<HtmlForm, HtmlFormBuilder>
    {
        public HtmlFormBuilder(HtmlHelper htmlHelper, int ModelId)
            //: base(htmlHelper, ModelId)
        {
            this.HtmlElement = new HtmlForm(htmlHelper, ModelId);
        }
    }
}
