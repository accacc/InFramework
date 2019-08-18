//using IF.Web.Mvc.FluentHtml.Base;
//using IF.Web.Mvc.FluentHtml.Validation;

//namespace IF.Web.Mvc.FluentHtml.Validaton
//{



//    public class ValidationMessageFactory
//    {
//        public HtmlFormElement formElement { get; set; }
//        public ValidationMessageFactory(HtmlFormElement formElement)
//        {
//            this.formElement = formElement;
//        }

//        public ValidationMessageBuilder Add()
//        {

//            ValidationMessage validationMessage = new ValidationMessage(this.formElement.htmlHelper, this.formElement.Name, this.formElement.MetaData, "");
//            formElement.ValidationMessage = validationMessage;
//            return new ValidationMessageBuilder(validationMessage);
//        }
//    }


//}
