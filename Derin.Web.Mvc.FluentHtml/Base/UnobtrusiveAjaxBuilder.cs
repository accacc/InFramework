using System;
using System.Web.Mvc.Ajax;

namespace IF.Web.Mvc.FluentHtml.Base
{
    public class UnobtrusiveAjaxBuilder<Element> : HtmlElementBuilder<HtmlElement>
        where Element : HtmlElement
    {

        public UnobtrusiveAjaxBuilder(Element htmlElement)
        {
            this.HtmlElement = htmlElement;
        }

        public UnobtrusiveAjaxBuilder<Element> GridViewId(string gridViewId)
        {
            this.HtmlElement.HtmlAttributes.Add("if-ajax-gridview-id", gridViewId);
            return this;
        }

        public UnobtrusiveAjaxBuilder<Element> Title(string title)
        {

            this.HtmlElement.HtmlAttributes.Add("if-ajax-title", title);
            return this;
        }




        public UnobtrusiveAjaxBuilder<Element> Confirm(string Confirm)
        {
            this.HtmlElement.HtmlAttributes["if-ajax-confirm"] = Confirm;
            return this;

        }

        public UnobtrusiveAjaxBuilder<Element> HttpMethod(string HttpMethod)
        {
            this.HtmlElement.HtmlAttributes["if-ajax-method"] = HttpMethod;
            return this;

        }
        
        public UnobtrusiveAjaxBuilder<Element> ExtraDataFunc(string extraDataFunc)
        {
            this.HtmlElement.HtmlAttributes.Add("if-ajax-extradatafunc", extraDataFunc);
            return this;
        }


        public UnobtrusiveAjaxBuilder<Element> InsertionMode(InsertionMode insertionMode)
        {

            if (insertionMode == System.Web.Mvc.Ajax.InsertionMode.InsertAfter)
            {
                this.HtmlElement.HtmlAttributes["if-ajax-insertion-mode"] = "after";
            }
            else if (insertionMode == System.Web.Mvc.Ajax.InsertionMode.InsertBefore)
            {
                this.HtmlElement.HtmlAttributes["if-ajax-insertion-mode"] = "before";
            }
            else if (insertionMode == System.Web.Mvc.Ajax.InsertionMode.ReplaceWith)
            {
                this.HtmlElement.HtmlAttributes["if-ajax-insertion-mode"] = "replace-with";
            }
            else if (insertionMode == System.Web.Mvc.Ajax.InsertionMode.Replace)
            {
                this.HtmlElement.HtmlAttributes["if-ajax-insertion-mode"] = "replace";
            }
            else
            {
                this.HtmlElement.HtmlAttributes["if-ajax-insertion-mode"] = "replace";
            }

            return this;

        }



        //OnBegin - xhr
        //OnComplete - xhr, status
        //OnSuccess - data, status, xhr
        //OnFailure - xhr, status, error


        public UnobtrusiveAjaxBuilder<Element> OnBegin(string OnBegin)
        {
            this.HtmlElement.HtmlAttributes["if-ajax-onbefore-func"] = OnBegin;
            return this;

        }

        public UnobtrusiveAjaxBuilder<Element> OnComplete(string OnComplete)
        {
            this.HtmlElement.HtmlAttributes["if-ajax-oncomplete-func"] = OnComplete;
            return this;

        }

        public UnobtrusiveAjaxBuilder<Element> OnFailure(string OnFailure)
        {
            this.HtmlElement.HtmlAttributes["if-ajax-onerror-func"] = OnFailure;
            return this;

        }

        public UnobtrusiveAjaxBuilder<Element> OnSuccess(string OnSuccess)
        {
            this.HtmlElement.HtmlAttributes["if-ajax-onsuccess-func"] = OnSuccess;
            return this;

        }

        public UnobtrusiveAjaxBuilder<Element> Url(string Url)
        {
            this.HtmlElement.HtmlAttributes["if-ajax-url"] = Url;
            return this;

        }

        public UnobtrusiveAjaxBuilder<Element> UpdatedTargetId(string UpdatedTargetId)
        {
            this.HtmlElement.HtmlAttributes["if-ajax-update-id"] = UpdatedTargetId;
            return this;

        }

        public UnobtrusiveAjaxBuilder<Element> ShowDialog()
        {
            this.HtmlElement.HtmlAttributes.Remove("if-ajax-update-id");
            this.HtmlElement.HtmlAttributes.Remove("if-ajax-show-dialog");
            this.HtmlElement.HtmlAttributes.Add("if-ajax-update-id", Guid.NewGuid().ToString());
            this.HtmlElement.HtmlAttributes.Add("if-ajax-show-dialog", "true");
            return this;
        }

        public UnobtrusiveAjaxBuilder<Element> RefreshGridOnSuccess()
        {
            this.HtmlElement.HtmlAttributes.Remove("if-ajax-refresh-grid");
            this.HtmlElement.HtmlAttributes.Add("if-ajax-refresh-grid", "true");
            return this;
        }

    }
}
 