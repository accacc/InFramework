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
    public class IFDropDownListYearBuilder : IFDropDownListBuilder
    {

        public IFDropDownListYearBuilder(IHtmlHelper html, string name):base(html,name)
        {
            this.HtmlElement = new IFDropDownList(html,name);

            List<NameValueDto> years = new List<NameValueDto>();

            for (int i = 2020; i >= 1930; i--)
            {
                years.Add(new NameValueDto { Name = i.ToString(), Value = i.ToString() });
            }

            this.HtmlElement.selectList = years;
        }


        

    }
}
