using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Web.Mvc.FluentHtml.Base
{
    public interface IContainerElement
    {

        IList<IHtmlElement> Childs { get; }
        void AcceptChild(IHtmlElement child);
    }
}
