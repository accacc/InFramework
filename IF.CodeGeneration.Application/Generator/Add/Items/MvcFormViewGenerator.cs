using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Add.Items
{


    public class MvcFormViewGenerator : CSInsertGenerator, IGenerateItem
    {


        public MvcFormViewGenerator(GeneratorContext context) : base(context)
        {
            //this.Files.Add(new VsFile() { FileExtension = "cshtml", FileName = "_GridView", FileType = VSFileType.Gridview, Path = "" });
            this.FileType = VSFileType.AddFormView;
        }

        public void Execute()
        {

            StringBuilder builder = new StringBuilder();


            builder.AppendLine("@{");
            builder.AppendLine("Layout = \"~/Views/Shared/_DialogFormLayout.cshtml\";");
            builder.AppendLine("}");

            builder.AppendLine($"@model {this.Context.nameSpaceName}.Models.{this.Context.className}Model");
            builder.AppendLine();

            builder.AppendLine("<form>");

            CSClass gridClass = GenerateClass();

            foreach (var item in gridClass.Properties)
            {
                if (item.Name == "Id") continue;

                string required = "required";

                if (item.IsNullable)
                {
                    required = "";
                }

                builder.AppendLine("<div class=\"row\">");
                builder.AppendLine("<div class=\"col-md-6\">");
                builder.AppendLine("<div class=\"form-group\">");
                builder.AppendLine($"<label for=\"{item.Name}\">{item.Name}</label>");
                builder.AppendLine($"<input type=\"text\" name=\"{item.Name}\" class=\"form-control\" value=\"@Model.{item.Name}\" {required} />");
                builder.AppendLine($"</div>");
                builder.AppendLine($"</div>");
                builder.AppendLine($"</div>");
            }


            builder.AppendLine("<div class=\"row\">");
            builder.AppendLine("<div class=\"col-md-6\">");
            builder.AppendLine("<button type=\"submit\" class=\"btn btn-primary\"");
            builder.AppendLine($"if-ajax-action=\"@Url.Action(Html.ActionName())\"");
            builder.AppendLine("if-ajax-form-submit=\"true\"");
            builder.AppendLine("if-ajax-method=\"post\"");
            builder.AppendLine(">");
            builder.AppendLine("Kaydet");
            builder.AppendLine("</button>");
            builder.AppendLine($"</div>");
            builder.AppendLine($"</div>");

            builder.AppendLine($"@Html.HiddenFor(model => model.Id)");


            builder.AppendLine("</form>");            

            IVsFile vsFile = this.GetVsFile();

            this.Context.fileSystem.FormatCode(builder.ToString(), vsFile.FileExtension, vsFile.FileName);

            this.Context.VsManager.AddVisualStudio(vsFile.ProjectName, vsFile.Path,vsFile.FileName, vsFile.FileExtension);
        }
    }

}
