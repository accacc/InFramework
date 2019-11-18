//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace IF.CodeGeneration.Application.Generator.Add.Items
//{
//    class MvcFormViewGenerator
//    {
//    }

//    public void GenerateMvcFormView(string className, string namespaceName, ClassTree classTree, Type classType)
//    {

//        StringBuilder builder = new StringBuilder();


//        builder.AppendLine("@{");
//        builder.AppendLine("Layout = \"~/Views/Shared/_DialogFormLayout.cshtml\";");
//        builder.AppendLine("}");

//        builder.AppendLine($"@model {namespaceName}.Models.{className}Model");
//        builder.AppendLine();

//        builder.AppendLine("<form>");

//        CSClass gridClass = GenerateClass();

//        foreach (var item in gridClass.Properties)
//        {
//            if (item.Name == "Id") continue;

//            string required = "required";

//            if (item.IsNullable)
//            {
//                required = "";
//            }

//            builder.AppendLine("<div class=\"row\">");
//            builder.AppendLine("<div class=\"col-md-6\">");
//            builder.AppendLine("<div class=\"form-group\">");
//            builder.AppendLine($"<label for=\"{item.Name}\">{item.Name}</label>");
//            builder.AppendLine($"<input type=\"text\" name=\"{item.Name}\" class=\"form-control\" value=\"@Model.{item.Name}\" {required} />");
//            builder.AppendLine($"</div>");
//            builder.AppendLine($"</div>");
//            builder.AppendLine($"</div>");
//        }


//        builder.AppendLine("<div class=\"row\">");
//        builder.AppendLine("<div class=\"col-md-6\">");
//        builder.AppendLine("<button type=\"submit\" class=\"btn btn-primary\"");
//        builder.AppendLine($"if-ajax-action=\"@Url.Action(Html.ActionName())\"");
//        builder.AppendLine("if-ajax-form-submit=\"true\"");
//        builder.AppendLine("if-ajax-method=\"post\"");
//        builder.AppendLine("Kaydet");
//        builder.AppendLine("</button>");
//        builder.AppendLine($"</div>");
//        builder.AppendLine($"</div>");

//        builder.AppendLine($"@Html.HiddenFor(model => model.Id)");


//        builder.AppendLine("</form>");

//        this.Context.fileSystem.FormatCode(builder.ToString(), "cshtml", "_Form");
//    }
//}
