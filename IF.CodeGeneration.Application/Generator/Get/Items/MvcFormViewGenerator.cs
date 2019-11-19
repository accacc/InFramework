using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Get.Items
{


    public class MvcFormViewGenerator : CSGetGenerator, IGenerateItem
    {


        public MvcFormViewGenerator(GeneratorContext context) : base(context)
        {
            //this.Files.Add(new VsFile() { FileExtension = "cshtml", FileName = "_GridView", FileType = ListFileType.Gridview, Path = "" });
            this.FileType = GetFileType.FormView;
        }

        public void Execute()
        {

            StringBuilder builder = new StringBuilder();


            builder.AppendLine("@{");
            builder.AppendLine("Layout = \"~/Views/Shared/_DialogFormLayout.cshtml\";");
            builder.AppendLine("}");

            builder.AppendLine($"@model {this.Context.nameSpaceName}.Models.{this.Context.className}Model");
            builder.AppendLine();

            builder.AppendLine("<div>");

            CSClass gridClass = GenerateClass();

            foreach (var item in gridClass.Properties)
            {               

                builder.AppendLine("<div class=\"row\">");
                builder.AppendLine("<div class=\"col-md-6\">");
                builder.AppendLine($"<label for=\"{item.Name}\">{item.Name}</label>");
                builder.AppendLine($"@Model.{item.Name}");
                builder.AppendLine($"</div>");
                builder.AppendLine($"</div>");
            }


            


            builder.AppendLine("</div>");

            GetVsFile vsFile = this.GetVsFile();

            this.Context.fileSystem.FormatCode(builder.ToString(), vsFile.FileExtension, vsFile.FileName);

            this.Context.VsManager.AddVisualStudio(vsFile.ProjectName, vsFile.Path,vsFile.FileName, vsFile.FileExtension);
        }
    }

}
