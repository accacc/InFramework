//using IF.Web.Mvc.FluentHtml.Base;
//using IF.Web.Mvc.FluentHtml.Validation;
//using System.Web.Mvc;

//namespace IF.Web.Mvc.FluentHtml.Validaton
//{
//    public class ValidationMessageBuilder : HtmlElementBuilder<ValidationMessage, ValidationMessageBuilder>
//    {


//        public ValidationMessageBuilder(ValidationMessage validationMessage)
//        {
//            this.HtmlElement = validationMessage;
//        }

//        public ValidationMessageBuilder(HtmlHelper htmlHelper, string modelName, ModelMetadata MetaData, string Message = "")
//        {
//            this.HtmlElement = new ValidationMessage(htmlHelper, modelName, MetaData,Message);
//        }

//        public new MvcHtmlString Render()
//        {
//            return this.HtmlElement.Render();
//        }



//        public ValidationMessageBuilder Message(string Message)
//        {
//            this.HtmlElement.validationMessage = Message;
//            return this;
//        }

//        public ValidationMessageBuilder Tag(string Tag)
//        {
//            this.HtmlElement.tag = Tag;
//            return this;
//        }
//    }
//}
