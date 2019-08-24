//using IF.Web.Mvc.FluentHtml.Base;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace IF.Web.Mvc.FluentHtml.HtmlForm
//{
//    public class BootstrapAjaxGridFormBuilder : HtmlFormBuilderBase<BootstrapAjaxGridForm, BootstrapAjaxGridFormBuilder>
//    {
//        public BootstrapAjaxGridFormBuilder(IHtmlHelper htmlHelper, int ModelId)
//        //: base(htmlHelper, ModelId)
//        {
//            this.HtmlElement = new BootstrapAjaxGridForm(htmlHelper, ModelId);
//            this.HtmlElement.HtmlAttributes.Add("if-ajax", "true");
//            this.HtmlElement.HtmlAttributes.Add("if-ajax-mode", "replace");
//            this.HtmlElement.HtmlAttributes.Add("if-ajax-method", "post");
//            //this.HtmlElement.RouteValues = new RouteValueDictionary();

//        }

//        public BootstrapAjaxGridFormBuilder Title(string ActionDescription)
//        {
//            this.HtmlElement.Title = ActionDescription;
//            return this;
//        }

//        public BootstrapAjaxGridFormBuilder AjaxOptions(Action<UnobtrusiveAjaxBuilder<BootstrapAjaxGridForm>> configurator)
//        {
//            UnobtrusiveAjaxBuilder<BootstrapAjaxGridForm> factory = new UnobtrusiveAjaxBuilder<BootstrapAjaxGridForm>(this.HtmlElement);

//            configurator(factory);
//            return this;
//        }
//    }
//}
