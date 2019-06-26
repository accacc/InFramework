using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.Base
{
    public interface IHtmlElementBuilder
    {
        MvcHtmlString Render();
    }
}
 