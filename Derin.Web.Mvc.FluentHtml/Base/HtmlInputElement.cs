using System;
using System.Web.Mvc;


namespace IF.Web.Mvc.FluentHtml.Base
{



    public abstract class HtmlInputElement : HtmlFormElement
    {
        
        public bool useViewData { get; set; }

        public bool isExplicitValue { get; set; }

        public string Format { get; set; }

        public InputType InputType { get; set; }

        public string Value { get; set; }




        public HtmlInputElement(HtmlHelper html, string Name, string Value, ModelMetadata metaData, InputType InputType)
            : base(html,Name,metaData)
        {
            this.InputType = InputType;
            this.Value = Value;
            this.SetId = true;
        }

        public override void Build()
        {

            base.Build();

            if (String.IsNullOrWhiteSpace(Name))
            {
                Name = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(Name);

                if (String.IsNullOrEmpty(Name))
                {
                    throw new ArgumentException("name");
                }
            }

            Builder.MergeAttributes(this.HtmlAttributes);

            Builder.MergeAttribute("type", HtmlHelper.GetInputTypeString(this.InputType));
            Builder.MergeAttribute("name", Name, true);

            string valueParameter = htmlHelper.FormatValue(Value, Format);

            //bool usedModelState = false;

            string attemptedValue = (string)htmlHelper.GetModelStateValue(Name, typeof(string));

            Builder.MergeAttribute("value", attemptedValue ?? ((useViewData) ? htmlHelper.EvalString(Name, Format) : valueParameter), isExplicitValue);




            ModelState modelState;

            if (htmlHelper.ViewData.ModelState.TryGetValue(Name, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    Builder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }
            }

            Builder.AddCssClass("form-control");

            Builder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(Name, MetaData));

            if (!String.IsNullOrWhiteSpace(Id))
            {
                Builder.Attributes.Add("id", Id);
            }
            else
            {
                if (SetId)
                {
                    Builder.GenerateId(Name);
                }
            }
        }

        public override MvcHtmlString CreateHtml()
        {
            return MvcHtmlString.Create(this.Builder.ToString(TagRenderMode.SelfClosing));

        }


        public override string GetTag()
        {
            return "input";
        }
    }
}
