using System;
using System.Collections.Generic;
using System.Text;
using IF.CodeGeneration.Core;

namespace IF.CodeGeneration.Application.Generator.List.Items
{
    public class ListMvcIndexViewGenerator :  ApplicationCodeGenerateItem
    {
        public ListMvcIndexViewGenerator(ApplicationCodeGeneratorContext context) : base(context)
        {
            //this.Files.Add(new VsFile() { FileExtension = "cshtml", FileName = "Index", FileType = VSFileType.IndexView, Path = "" });
            this.FileType = VSFileType.ListIndexView;

        }

        public override void Execute()
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
            builder.AppendLine($"@{{await Html.RenderPartialAsync(\"~/{this.Context.RepositoryName}/_GridView.cshtml\", Model);}}");
            builder.AppendLine();
            builder.AppendLine("}");

            IFVsFile vsFile = this.GetVsFile();

            this.Context.fileSystem.FormatCode(builder.ToString(), vsFile.FileExtension, vsFile.FileName);            

            this.Context.VsManager.AddFile(vsFile.ProjectName, vsFile.Path, vsFile.FileName, vsFile.FileExtension);
        }
    }
}
