using IF.Web.Mvc.FluentHtml.Base;
using IF.Web.Mvc.FluentHtml.HtmlForm;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;

namespace IF.Core.Mvc.PageLayout
{


    public class AjaxFormBuilder : HtmlFormBuilderBase<AjaxForm, AjaxFormBuilder>
    {
        public AjaxFormBuilder(IHtmlHelper htmlHelper, object ModelId)
        //: base(htmlHelper, ModelId)
        {
            this.HtmlElement = new AjaxForm(htmlHelper, ModelId);
            this.HtmlElement.HtmlAttributes.Add("if-ajax", "true");
            this.HtmlElement.HtmlAttributes.Add("if-ajax-mode", "replace");
            this.HtmlElement.HtmlAttributes.Add("if-ajax-method", "post");
            this.HtmlElement.RouteValues = new RouteValueDictionary();

        }




        public AjaxFormBuilder Title(string ActionDescription)
        {
            this.HtmlElement.Title = ActionDescription;
            return this;
        }

        public AjaxFormBuilder AjaxOptions(Action<UnobtrusiveAjaxBuilder<HtmlForm>> configurator)
        {
            UnobtrusiveAjaxBuilder<HtmlForm> factory = new UnobtrusiveAjaxBuilder<HtmlForm>(this.HtmlElement);

            configurator(factory);
            return this;
        }

        public AjaxFormBuilder CloseAfterSuccessSubmit(bool IsEnabled)
        {
            if (IsEnabled)
            {
                this.AjaxOptions(ajax => ajax.OnSuccess("FormGlobalSuccess"));
            }
            else
            {
                this.HtmlElement.HtmlAttributes.Remove("if-ajax-success");
            }

            return this as AjaxFormBuilder;
        }
    }


    public class BootstrapAjaxFilterFormBuilder : HtmlFormBuilderBase<BootstrapAjaxFilterForm, BootstrapAjaxFilterFormBuilder>
    {
        public BootstrapAjaxFilterFormBuilder(IHtmlHelper htmlHelper, int ModelId)
        //: base(htmlHelper, ModelId)
        {
            this.HtmlElement = new BootstrapAjaxFilterForm(htmlHelper, ModelId);
            this.HtmlElement.HtmlAttributes.Add("if-ajax", "true");
            this.HtmlElement.HtmlAttributes.Add("if-ajax-mode", "replace");
            this.HtmlElement.HtmlAttributes.Add("if-ajax-method", "post");
            this.HtmlElement.RouteValues = new RouteValueDictionary();

        }

        public BootstrapAjaxFilterFormBuilder Title(string ActionDescription)
        {
            this.HtmlElement.Title = ActionDescription;
            return this;
        }

        public BootstrapAjaxFilterFormBuilder GenerateHiddenId(bool GenerateHiddenId)
        {
            this.HtmlElement.GenerateHiddenId = GenerateHiddenId;
            return this;
        }

        public BootstrapAjaxFilterFormBuilder AjaxOptions(Action<UnobtrusiveAjaxBuilder<BootstrapAjaxFilterForm>> configurator)
        {
            UnobtrusiveAjaxBuilder<BootstrapAjaxFilterForm> factory = new UnobtrusiveAjaxBuilder<BootstrapAjaxFilterForm>(this.HtmlElement);

            configurator(factory);
            return this;
        }
    }


    public class BootstrapAjaxGridFormBuilder : HtmlFormBuilderBase<BootstrapAjaxGridForm, BootstrapAjaxGridFormBuilder>
    {
        public BootstrapAjaxGridFormBuilder(HtmlHelper htmlHelper, int ModelId)
        //: base(htmlHelper, ModelId)
        {
            this.HtmlElement = new BootstrapAjaxGridForm(htmlHelper, ModelId);
            this.HtmlElement.HtmlAttributes.Add("if-ajax", "true");
            this.HtmlElement.HtmlAttributes.Add("if-ajax-mode", "replace");
            this.HtmlElement.HtmlAttributes.Add("if-ajax-method", "post");
            this.HtmlElement.RouteValues = new RouteValueDictionary();

        }

        public BootstrapAjaxGridFormBuilder Title(string ActionDescription)
        {
            this.HtmlElement.Title = ActionDescription;
            return this;
        }

        public BootstrapAjaxGridFormBuilder AjaxOptions(Action<UnobtrusiveAjaxBuilder<BootstrapAjaxGridForm>> configurator)
        {
            UnobtrusiveAjaxBuilder<BootstrapAjaxGridForm> factory = new UnobtrusiveAjaxBuilder<BootstrapAjaxGridForm>(this.HtmlElement);

            configurator(factory);
            return this;
        }
    }
}
