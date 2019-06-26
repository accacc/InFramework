using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Web.Mvc.FluentHtml.Base
{
    public abstract class HtmlFormElement : HtmlElement
    {
        //public IFLabel Label { get; set; }

        //public ValidationMessage ValidationMessage { get; set; }

        //public bool IncludeValidationMessage { get; set; }

        public string Name { get; set; }




        public HtmlFormElement(IHtmlHelper html, string Name)
            : base(html)
        {
            //this.MetaData = metaData;
            this.HtmlAttributes.Add("name",Name);
            this.Id = Name;

        }


        //public MvcHtmlString RenderWithFormGroup()
        //{
        //    BootstrapFormGroup group = new BootstrapFormGroup(this.htmlHelper);

        //    if (this.Label != null)
        //    {
        //        group.AcceptChild(this.Label);
        //    }

        //    group.AcceptChild(this);

        //    if (this.IncludeValidationMessage && this.ValidationMessage != null)
        //    {
        //        group.AcceptChild(this.ValidationMessage);
        //    }

        //    return group.Render();
        //}


    }
}
