using IF.Core.Data;
using IF.Web.Mvc.FluentHtml.Base;
using IF.Web.Mvc.FluentHtml.Extension;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace IF.Web.Mvc.FluentHtml
{
   

    public class IFDropDownList : HtmlFormElement
    {
       

        public HtmlHelper html { get; set; }
        public bool allowMultiple { get; set; }

        public object selectedValue { get; set; }
        public IEnumerable<NameValueDto> selectList { get; set; }

        public string optionLabel { get; set; }

        public string Url { get; set; }

        public string CascadeFrom { get; set; }
        public IFDropDownList(IHtmlHelper html,string name) : base(html,name)

        {
            this.html = (HtmlHelper)html;
        }
        public override void Build()
        {
            base.Build();

            StringBuilder sb = new StringBuilder();

            string optionPattern = @"<option {0} value=""{1}"">{2}</option>";

            string selectedPattern = @"selected=""selected""";

            if (!String.IsNullOrWhiteSpace(optionLabel))
            {
                sb.Append(String.Format(optionPattern, selectedPattern,"-1", optionLabel));
            }

            if (!String.IsNullOrWhiteSpace(CascadeFrom))
            {
                this.Builder.Attributes.Add("if-ajax-cascadefrom",this.CascadeFrom);
            }

            if (String.IsNullOrWhiteSpace(this.Url))
            {
                if (selectList != null)
                {
                    foreach (var item in selectList)
                    {
                        string selected = String.Empty;

                        if (selectedValue != null && item.Value == selectedValue.ToString())
                        {
                            selected = selectedPattern;
                        }

                        sb.Append(String.Format(optionPattern, selected, item.Value, item.Name));
                    }
                }                
            }
            else
            {
                this.Builder.Attributes.Add("if-ajax-url", this.Url);
                this.Builder.Attributes.Add("if-ajax-selected", this.selectedValue.ToString());
            }

            this.Builder.InnerHtml.AppendHtml(sb.ToString());

        }


    

        public override string GetTag()
        {
            return "select";
        }

        public override HtmlString CreateHtml()
        {
            return this.Builder.Render();
        }
    }
}
