//using IF.Web.Mvc.FluentHtml.Base;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Microsoft.AspNetCore.Mvc.ViewFeatures;
//using System;
//using System.Linq;

//namespace IF.Web.Mvc.FluentHtml.Label
//{


//    public class IFLabel : HtmlElement
//    {
//        public bool IsRequired { get; set; }
//        public string labelText { get; set; }

//        public string htmlFieldName { get; set; }

        

//         public IFLabel(HtmlHelper htmlHelper, string Text,string For,ModelMetadata MetaData)
//            : base(htmlHelper)
//        {
//            this.labelText = Text;
//            this.htmlFieldName = For;
//            this.MetaData = MetaData;
//        }

//         public override void Build()
//         {
//             base.Build();

//             if (string.IsNullOrEmpty(labelText))
//             {
//                 labelText = MetaData.DisplayName ?? MetaData.PropertyName ?? htmlFieldName.Split('.').Last();
//             }

             

             
//             this.Builder.Attributes.Add(
//                 "for",
//                 TagBuilder.CreateSanitizedId(
//                     htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)
//                 )
//             );

//             this.Builder.AddCssClass("control-label");

            

//            //TODO:Caglar Fluent Validation metadatayi duzgun set etmiyor,o yuzden bu kisim AjaxApp.js icindeki SetAsteriks methoduyla yapildi.
//            //if (MetaData.IsRequired || IsRequired)
//            // {
//            //     var asteriskTag = new TagBuilder("span");
//            //     asteriskTag.Attributes.Add("class", "required");
//            //     asteriskTag.SetInnerText("*");
//            //     labelText += " " + asteriskTag.ToString(TagRenderMode.Normal);
//            // }



//             this.Builder.InnerHtml = labelText;

//             if (!String.IsNullOrWhiteSpace(Id))
//             {
//                 this.Builder.Attributes.Add("id", Id);
//             }

//             this.Builder.MergeAttributes(this.HtmlAttributes, true);

//         }
//         public override MvcHtmlString CreateHtml()
//         {
//             return MvcHtmlString.Create(this.Builder.ToString(TagRenderMode.Normal));
//         }

//         public override string GetTag()
//         {
//             return "label";
//         }
//    }
//}
