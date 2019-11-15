using System;
using System.Collections.Generic;
using System.Text;
using IF.CodeGeneration.Core;

namespace IF.CodeGeneration.Application.Generator.List.Items
{
    public class MvcIndexViewGenerator : CSListGenerator, IGenerateItem
    {
        public MvcIndexViewGenerator(FileSystemCodeFormatProvider fileSystem, string className, string nameSpaceName, ClassTree classTree, Type classType) : base(fileSystem, className, nameSpaceName, classTree, classType)
        {
            this.File = new VsFile() { FileExtension = "cshtml", FileName = "Index", FileType = ListFileType.IndexView, Path = "" };
        }

        public void Execute()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"@model List<{nameSpaceName}.Models.{className}GridModel>");
            builder.AppendLine();
            builder.AppendLine($"@{{ViewBag.Title = \"{Title}\"; }}");
            builder.AppendLine();
            builder.AppendLine("@{");
            builder.AppendLine("Layout = \"~/Views/Shared/_GridLayout.cshtml\";");
            builder.AppendLine();
            builder.AppendLine("@section GridView");
            builder.AppendLine("{");
            builder.AppendLine();
            builder.AppendLine($"@{{await Html.RenderPartialAsync(\"~/Views/Security/{className}/_GridView.cshtml\", Model);}}");
            builder.AppendLine();
            builder.AppendLine("}");

            //@model List<Gedik.SSO.Models.ApplicationGridModel>

            //@{ViewBag.Title = "Uygulama Yönetimi"; }

            //@{
            //    Layout = "~/Views/Shared/_GridLayout.cshtml";
            //}

            //@section GridView
            //{
            //    @{await Html.RenderPartialAsync("~/Views/Security/Application/_GridView.cshtml", Model);}
            //}
        }
    }
}
