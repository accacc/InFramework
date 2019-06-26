using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Web.Mvc.FluentHtml.Base
{
    public interface IChildElement
    {
        IHtmlElement Parent { get; }

        void AcceptParent(IHtmlElement parent);
    }
}
