using IF.Web.Mvc.FluentHtml.HtmlForm;
using System.Web.Mvc;

namespace IF.Core.Mvc.PageLayout
{
    public class AjaxForm : HtmlForm
    {


        public AjaxForm(HtmlHelper htmlHelper, object ModelId)
            : base(htmlHelper, ModelId)
        {

        }


        public bool CloseAfterSuccessSubmit { get; set; }
        


    }
}
