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
    public class IFDropDownListMonthBuilder : IFDropDownListBuilder
    {

        public IFDropDownListMonthBuilder(IHtmlHelper html, string name):base(html,name)
        {
            this.HtmlElement = new IFDropDownList(html,name);

            List<NameValueDto> months = new List<NameValueDto>();

            for (int i = 1; i <= 12; i++)
            {
                months.Add(new NameValueDto { Name = i.ToString(), Value = i.ToString() });
            }

            this.HtmlElement.selectList = months;
        }


        

    }
}
