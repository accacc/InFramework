using System.Collections.Generic;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.Base
{
    public interface IContainerElement
    {

        IList<IHtmlElement> Childs { get; }
        void AcceptChild(IHtmlElement child);        
    }

    public interface IChildElement
    {
        IHtmlElement Parent { get; }

        void AcceptParent(IHtmlElement parent);
    }
}
