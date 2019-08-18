using IF.Core.Mvc.PageLayout;
using IF.Web.Mvc.FluentHtml.DropDownList;
using IF.Web.Mvc.FluentHtml.Link;
using IF.Web.Mvc.FluentHtml.Modal.Bootstrap;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace IF.Web.Mvc.FluentHtml.Extension
{
    public class HtmlElementFactory
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected IHtmlHelper HtmlHelper
        {
            get;
            set;
        }


        public HtmlElementFactory(IHtmlHelper helper)
        {
            this.HtmlHelper = helper;
        }

        public ModalBuilder BootstrapModal()
        {
            return new ModalBuilder(HtmlHelper);
        }

        public IFDropDownListBuilder DropDownList(string name)
        {
            return new IFDropDownListBuilder(HtmlHelper,name);
        }

        public IFDropDownListDaysBuilder DropDownListDays(string name)
        {
            return new IFDropDownListDaysBuilder(HtmlHelper, name);
        }

        public IFDropDownListMonthBuilder DropDownListMonth(string name)
        {
            return new IFDropDownListMonthBuilder(HtmlHelper, name);
        }

        public IFDropDownListYearBuilder DropDownListYear(string name)
        {
            return new IFDropDownListYearBuilder(HtmlHelper, name);
        }

        public AjaxLinkBuilder AjaxLink(string Text, string ActionName, string ControllerName)
        {
            var builder = new AjaxLinkBuilder(new ActionLink(this.HtmlHelper, Text, ActionName, ControllerName));
            //builder.HtmlAttributes(new  { @class = "btn btn-primary btn-sm margin-bottom-20" });
            return builder;
        }


        public BootstrapAjaxFilterFormBuilder BootstrapAjaxFilterForm(int ModelId = 0)
        {
            return new BootstrapAjaxFilterFormBuilder(this.HtmlHelper, ModelId)
                .Id("GridFilterForm")
                ;
        }
    }
}
