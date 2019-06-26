using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Web.Mvc.FluentHtml.Base
{
    public abstract class HtmlFormElementBuilder<Element, Builder> : HtmlElementBuilder<Element, Builder>
        where Element : HtmlFormElement
        where Builder : HtmlFormElementBuilder<Element, Builder>

    {




        public Builder Name(string Name)
        {
            this.HtmlElement.Name = Name;
            return this as Builder;
        }

        //public Builder Label(Action<LabelFactory> configurator)
        //{
        //    LabelFactory factory = new LabelFactory(this.HtmlElement);
        //    configurator(factory);
        //    return this as Builder;
        //}


        //public Builder ValidationMessage(Action<ValidationMessageFactory> configurator)
        //{
        //    ValidationMessageFactory factory = new ValidationMessageFactory(this.HtmlElement);
        //    configurator(factory);
        //    return this as Builder;
        //}

        //public TextBoxBuilder Label()
        //{
        //    this.HtmlElement.Label = new LabelBuilder(this.HtmlElement.htmlHelper, "", this.HtmlElement.Name, this.HtmlElement.MetaData);
        //    return this;
        //}

        //public Builder IncludeValidationMessage(bool IncludeValidationMessage)
        //{
        //    this.HtmlElement.IncludeValidationMessage = IncludeValidationMessage;
        //    return this as Builder;
        //}

        //public MvcHtmlString RenderWithFormGroup(bool IncludeValidationMessage = true)
        //{
        //    return this.HtmlElement.RenderWithFormGroup();
        //}
    }
}
