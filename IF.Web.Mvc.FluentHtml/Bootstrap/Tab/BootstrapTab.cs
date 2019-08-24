using IF.Web.Mvc.FluentHtml.Base;
using IF.Web.Mvc.FluentHtml.Extension;
using IF.Web.Mvc.FluentHtml.HtmlForm;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;

namespace IF.Web.Mvc.FluentHtml.Bootstrap.Tab
{
    public class BootstrapTab : HtmlDivElement
    {

        public int TabIndex { get; set; }

        public BootstrapTab(IHtmlHelper helper)
            : base(helper)
        {
            this.Items = new List<BootstrapTabLink>();
            this.TabIndex = 1;
        }


        public IList<BootstrapTabLink> Items { get; set; }

        public override void Build()
        {
            base.Build();

            //this.Builder.AddCssClass("tabbable");

            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("nav nav-pills");

            HtmlDivElement content = new HtmlDivElement(htmlHelper);
            content.Build();
            content.Builder.AddCssClass("tab-content");


            for (int i = 0; i < Items.Count; i++)
            {

                TagBuilder li = new TagBuilder("li");

                li.AddCssClass("nav-item");

                if (Items[i].Active)
                {
                    Items[i].CssClass = "nav-link active";
                }
                else
                {
                    Items[i].CssClass = "nav-link";
                }

                

                Items[i].HtmlAttributes.Add("data-target", String.Format("#tab_{0}_{1}", TabIndex, i));

                li.InnerHtml.AppendHtml(Items[i].Render());
                ul.InnerHtml.AppendHtml(li);


                HtmlDivElement tabPane = new HtmlDivElement(htmlHelper);
                tabPane.Id = String.Format("tab_{0}_{1}", TabIndex, i);
                tabPane.Build();                
                tabPane.Builder.AddCssClass("tab-pane fade");

                if (Items[i].Active)
                {
                    tabPane.Builder.AddCssClass("show active");
                }
               

                if (Items[i].Content!=null)
                {
                    try
                    {
                        var writer = new System.IO.StringWriter();

                        writer.WriteContent<object>(Items[i].Content, HtmlEncoder.Default, null, false);
                        var c = new HtmlString(writer.ToString());
                        tabPane.Builder.InnerHtml.AppendHtml(c);
                    }
                    catch (Exception ex)
                    {

                        tabPane.Builder.InnerHtml.Append("Content Render Fail! " + ex.GetBaseException().Message);
                    }
                }
                
                content.Builder.InnerHtml.AppendHtml(tabPane.Render());
            }


            this.Builder.InnerHtml.AppendHtml(ul);
            this.Builder.InnerHtml.AppendHtml(content.Render());

        }
    }
}
