using IF.Core.Data;
using IF.Web.Mvc.FluentHtml.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Web.Mvc.FluentHtml.DropDownList
{
    public class IFDropDownListBuilder : HtmlFormElementBuilder<IFDropDownList, IFDropDownListBuilder>
    {

        public IFDropDownListBuilder(IHtmlHelper html, string name)

        {
            this.HtmlElement = new IFDropDownList(html,name);
        }


        public IFDropDownListBuilder allowMultiple(bool allowMultiple)
        {
            this.HtmlElement.allowMultiple = allowMultiple;
            return this;
        }

        public IFDropDownListBuilder SelectList(IEnumerable<NameValueDto> selectList)
        {
            this.HtmlElement.selectList = selectList;
            return this;
        }

        public IFDropDownListBuilder OptionLabel(string optionLabel)
        {
            this.HtmlElement.optionLabel = optionLabel;
            return this;
        }

        public IFDropDownListBuilder SelectedValue(object selectedValue)
        {
            this.HtmlElement.selectedValue = selectedValue;
            return this;
        }

        public IFDropDownListBuilder Url(string url)
        {
            this.HtmlElement.Url = url;
            return this;
        }

        public IFDropDownListBuilder CascadeFrom(string CascadeFrom)
        {
            this.HtmlElement.CascadeFrom = CascadeFrom;
            return this;
        }

    }
}
