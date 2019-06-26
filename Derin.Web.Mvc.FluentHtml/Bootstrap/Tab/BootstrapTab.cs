using IF.Web.Mvc.FluentHtml.Base;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IF.Web.Mvc.FluentHtml.Bootstrap.Tab
{
    public class BootstrapTab : HtmlDivElement
    {

        public int TabIndex { get; set; }

        public BootstrapTab(HtmlHelper helper)
            : base(helper)
        {
            this.Items = new List<BootstrapTabLink>();
            this.TabIndex = 1;
        }


        public IList<BootstrapTabLink> Items { get; set; }

        public override void Build()
        {
            base.Build();

            this.Builder.AddCssClass("tabbable");

            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("nav nav-tabs");

            HtmlDivElement content = new HtmlDivElement(htmlHelper);
            content.Build();
            content.Builder.AddCssClass("tab-content");


            for (int i = 0; i < Items.Count; i++)
            {

                TagBuilder li = new TagBuilder("li");

                Items[i].HtmlAttributes.Add("data-target", String.Format("#tab_{0}_{1}", TabIndex, i));

                if (Items[i].HtmlAttributes["aria-expanded"].ToString() == "true")
                {
                    li.AddCssClass("active");
                }

                //if (!String.IsNullOrWhiteSpace(Items[i].Content))
                //{
                //    Items[i].HtmlAttributes.Remove("tabajax");
                //    Items[i].HtmlAttributes.Add("data-toggle","tabcontent");
                //}

                

                li.InnerHtml += Items[i].Render();
                ul.InnerHtml += li.ToString(TagRenderMode.Normal);


                HtmlDivElement tabPane = new HtmlDivElement(htmlHelper);
                tabPane.Id = String.Format("tab_{0}_{1}", TabIndex, i);
                tabPane.Build();                
                tabPane.Builder.AddCssClass("tab-pane");
                tabPane.Builder.AddCssClass("fade");

                if (Items[i].HtmlAttributes["aria-expanded"].ToString() == "true")
                {
                    tabPane.Builder.AddCssClass("active in");
                }

                if(!String.IsNullOrWhiteSpace(Items[i].Content))
                {
                    tabPane.Builder.InnerHtml += MvcHtmlString.Create(Items[i].Content);                    
                }
                
                content.Builder.InnerHtml += tabPane.Render();
            }


            this.Builder.InnerHtml += ul.ToString(TagRenderMode.Normal);
            this.Builder.InnerHtml += content.Render();

            //         <ul class="nav nav-pills">
            //    <li class="active">
            //        <a href="@Url.Action("MainInfo","Organization")" data-target="#tab_1_1" data-toggle="tabajax" aria-expanded="true">Main Info</a>
            //    </li>
            //    <li class="">
            //        <a href="@Url.Action("ContactInfo","Organization")" data-target="#tab_1_2" data-toggle="tabajax" aria-expanded="false">Contact Info</a>
            //    </li>
            //</ul>

            //<div class="tab-content">
            //    <div class="tab-pane fade active in" id="tab_1_1">
            //    </div>
            //    <div class="tab-pane fade" id="tab_1_2">
            //    </div>
            //</div>
        }
    }
}
