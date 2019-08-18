//using IF.Web.Mvc.FluentHtml.Base;
//using Microsoft.AspNetCore.Mvc.ViewFeatures;

//namespace IF.Web.Mvc.FluentHtml.Validation
//{
//    public class ValidationMessage : HtmlElement
//    {
//        public string modelName { get; set; }
//        public string tag { get; set; }

//        public string ResourceClassKey { get; set; }

//        public string validationMessage { get; set; }
//        public ValidationMessage(HtmlHelper htmlHelper, string modelName,ModelMetadata MetaData,string Message = "")
//            : base(htmlHelper)
//        {
//            this.modelName = modelName;
//            this.MetaData = MetaData;
//            this.validationMessage = Message;
//        }


//        public override void Build()
//        {
//            base.Build();
//            this.Builder.InnerHtml = htmlHelper.ValidationMessage(modelName).ToString();
//        }

//        public override MvcHtmlString CreateHtml()
//        {
//            return MvcHtmlString.Create(this.Builder.ToString(TagRenderMode.Normal));

//        }


//        public override string GetTag()
//        {
//            return "div";
//        }
//    }
//}
