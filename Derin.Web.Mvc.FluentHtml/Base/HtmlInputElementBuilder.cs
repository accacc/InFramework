using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Web.Mvc.FluentHtml.Base
{
    public abstract class HtmlInputElementBuilder<Element, Builder> : HtmlFormElementBuilder<Element, Builder>
        where Element : HtmlInputElement
        where Builder : HtmlInputElementBuilder<Element, Builder>
    {


        public Builder Value(string Value)
        {
            this.HtmlElement.Value = Value;
            return this as Builder;
        }
    }



}
