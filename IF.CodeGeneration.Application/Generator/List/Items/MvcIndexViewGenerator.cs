using System;
using System.Collections.Generic;
using System.Text;
using IF.CodeGeneration.Core;

namespace IF.CodeGeneration.Application.Generator.List.Items
{
    public class MvcIndexViewGenerator : CSListGenerator, IGenerateItem
    {
        public MvcIndexViewGenerator(GeneratorContext context) : base(context)
        {
            //this.Files.Add(new VsFile() { FileExtension = "cshtml", FileName = "Index", FileType = ListFileType.IndexView, Path = "" });
            this.FileType = ListFileType.IndexView;

        }

        public void Execute()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"@model List<{this.Context.nameSpaceName}.Models.{this.Context.className}GridModel>");
            builder.AppendLine();
            builder.AppendLine($"@{{ViewBag.Title = \"{this.Context.Title}\"; }}");
            builder.AppendLine();
            builder.AppendLine("@{");
            builder.AppendLine("Layout = \"~/Views/Shared/_GridLayout.cshtml\";");
            builder.AppendLine("}");
            builder.AppendLine();
            builder.AppendLine("@section GridView");
            builder.AppendLine("{");
            builder.AppendLine();
            builder.AppendLine($"@{{await Html.RenderPartialAsync(\"~/{this.Context.ViewBasePath}/_GridView.cshtml\", Model);}}");
            builder.AppendLine();
            builder.AppendLine("}");

            VsFile vsFile = this.GetVsFile();

            this.Context.fileSystem.FormatCode(builder.ToString(), vsFile.FileExtension, vsFile.FileName);            

            this.Context.VsManager.AddVisualStudio(vsFile.ProjectName, vsFile.Path, vsFile.FileName, vsFile.FileExtension);
        }
    }
}
