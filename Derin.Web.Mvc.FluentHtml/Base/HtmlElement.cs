using System;
using System.ComponentModel;
using System.Web.Mvc;
using System.Web.Routing;

namespace IF.Web.Mvc.FluentHtml.Base
{
    public abstract class HtmlElement : IHtmlElement,IChildElement
    {

        [EditorBrowsable(EditorBrowsableState.Never)]
        public HtmlHelper htmlHelper
        {
            get;
            set;
        }

         public IHtmlElement Parent
        {
            get;
            private set;
        }

         public void AcceptParent(IHtmlElement parent)
         {
             this.Parent = parent;

         }


        public TagBuilder Builder { get; set; }


        public HtmlElement(HtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            this.HtmlAttributes = new RouteValueDictionary();
        }


        public bool SetId { get; set; }        

        public ModelMetadata MetaData { get; set; }
        public string CssClass { get; set; }
        public string Id { get; set; }

        public RouteValueDictionary HtmlAttributes { get; set; }
        public string InnerText { get; set; }

        public virtual MvcHtmlString Render()
        {
            this.Build();

            if (this.Parent != null && this.Parent is IContainerElement)
            {
                this.Parent.Build();
                (this.Parent as IContainerElement).AcceptChild(this);
                return this.Parent.CreateHtml();                
            }
            else
            {
                return this.CreateHtml();
            }
        }
        public abstract string GetTag();

        public abstract MvcHtmlString CreateHtml();

        public virtual void Build()
        {
            if (this.Builder == null)
            {

                this.Builder = new TagBuilder(this.GetTag());

                if (!String.IsNullOrWhiteSpace(this.CssClass))
                {
                    this.Builder.AddCssClass(this.CssClass);
                }

                if (!String.IsNullOrWhiteSpace(this.Id))
                {
                    this.Builder.Attributes.Add("id",this.Id);
                }                
            }
        }
    }
}

