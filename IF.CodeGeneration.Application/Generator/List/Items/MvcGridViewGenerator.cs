using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.List.Items
{
    public class MvcGridViewGenerator: CSListGenerator, IGenerateItem
    {


        public MvcGridViewGenerator(GeneratorContext context):base(context)
        {
            //this.Files.Add(new VsFile() { FileExtension = "cshtml", FileName = "_GridView", FileType = ListFileType.Gridview, Path = "" });
            this.FileType = ListFileType.Gridview;
        }

        public void Execute()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"@model List<{this.Context.nameSpaceName}.Models.{this.Context.className}GridModel>");
            builder.AppendLine();


            builder.AppendLine("<table>");
            builder.AppendLine("<tr>");
            builder.AppendLine("<td>");

            builder.AppendLine("<a class=\"btn btn-primary\"");
            builder.AppendLine($"href=\"@Url.Action(\"{this.Context.className}Async\")\"");
            builder.AppendLine("if-ajax=\"true\"");
            builder.AppendLine("if-ajax-method=\"get\"");
            builder.AppendLine("if-ajax-mode=\"replace\"");
            builder.AppendLine("if-ajax-show-dialog=\"true\"");
            builder.AppendLine("if-ajax-modal-id=\"@Guid.NewGuid()\">");
            builder.AppendLine("Ekle");
            builder.AppendLine("</a>");

            builder.AppendLine("</td>");
            builder.AppendLine("</tr>");
            builder.AppendLine("</table>");


            builder.AppendLine("<table class=\"table table-striped table-sm\">");
            builder.AppendLine("<tr>");

            CSClass gridClass = base.GenerateClass();

            foreach (var item in gridClass.Properties)
            {
                builder.AppendLine("<th>");
                builder.AppendLine(item.Name);
                builder.AppendLine("</th>");
            }

            builder.AppendLine("<th>");
            builder.AppendLine("Düzenle");
            builder.AppendLine("</th>");


            builder.AppendLine("</tr>");

            builder.AppendLine("@if(Model != null && Model.Any())");
            builder.AppendLine("{");
            builder.AppendLine("@foreach (var item in Model)");
            builder.AppendLine("{");
            builder.AppendLine("<tr>");

            foreach (var item in gridClass.Properties)
            {
                builder.AppendLine("<td>");
                builder.AppendLine($"@Html.DisplayFor(modelItem => item.{item.Name})");
                builder.AppendLine("</td>");
            }


            builder.AppendLine("<td>");

            builder.AppendLine("<a class=\"btn btn-primary\"");
            builder.AppendLine($"href=\"@Url.Action(\"{this.Context.className}Edit\")\"");
            builder.AppendLine("if-ajax=\"true\"");
            builder.AppendLine("if-ajax-method=\"get\"");
            builder.AppendLine("if-ajax-mode=\"replace\"");
            builder.AppendLine("if-ajax-show-dialog=\"true\"");
            builder.AppendLine("if-ajax-modal-id=\"@Guid.NewGuid()\">");
            builder.AppendLine("Düzenle");
            builder.AppendLine("</a>");

            builder.AppendLine("</td>");

            builder.AppendLine("<td>");

            builder.AppendLine("<a class=\"btn btn-primary\"");
            builder.AppendLine($"href=\"@Url.Action(\"{this.Context.className}Get\")\"");
            builder.AppendLine("if-ajax=\"true\"");
            builder.AppendLine("if-ajax-method=\"get\"");
            builder.AppendLine("if-ajax-mode=\"replace\"");
            builder.AppendLine("if-ajax-show-dialog=\"true\"");
            builder.AppendLine("if-ajax-modal-id=\"@Guid.NewGuid()\">");
            builder.AppendLine("Göster");
            builder.AppendLine("</a>");

            builder.AppendLine("</td>");

            builder.AppendLine("</tr>");

            builder.AppendLine("}");
            builder.AppendLine("}");
            builder.AppendLine("else");
            builder.AppendLine("{");
            builder.AppendLine("@:Veri bulunamadı, Lütfen Kriter seçiniz");
            builder.AppendLine("}");
            builder.AppendLine("</table>");

            this.Context.fileSystem.FormatCode(builder.ToString(), "cshtml", "_GridView");

            ListVsFile vsFile = this.GetVsFile();

            this.Context.VsManager.AddVisualStudio(vsFile.ProjectName, vsFile.Path, vsFile.FileName, vsFile.FileExtension);
        }

       
    }
}
