using IF.CodeGeneration.Core;
using IF.CodeGeneration.Language.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.List.Items
{
    public class ListControllerMethodGenerator :  ApplicationCodeGenerateItem
    {
        

        public  ListControllerMethodGenerator(ApplicationCodeGeneratorContext context) : base(context)
        {
            //this.Files.Add(new VsFile() { FileExtension = "cs", FileName = "_GridView", FileType = VSFileType.MvcMethods, Path = "" });

            this.FileType = VSFileType.ListMvcControllerMethods;
        }

        public override void Execute()
        {
            CSMethod method = new CSMethod(this.Context.className + "Index", "ActionResult", "public");
            method.IsAsync = true;

            StringBuilder methodBody = new StringBuilder();


            //var list = await this.dispatcher.QueryAsync<ApplicationRequest, ApplicationResponse>(new ApplicationRequest());
            //var model = list.MapTo<ApplicationGridModel>();
            //return View("~/Views/Security/Application/Index.cshtml", model);

            methodBody.AppendLine($"var list = await this.dispatcher.QueryAsync<{this.Context.className}Request, {this.Context.className}Response>(new {this.Context.className}Request());");
            methodBody.AppendLine($"var model = list.Data.MapTo<{this.Context.className}GridModel>();");
            methodBody.AppendFormat($"return View(\"~/{this.Context.RepositoryName}/Index.cshtml\", model);");
            method.Body = methodBody.ToString();

            this.Context.fileSystem.FormatCode(method.GenerateCode(), "cs", "", "");

            IFVsFile vsFile = this.GetVsFile();

            var controllerPath = $@"{this.Context.VsManager.GetProjectPath(vsFile.ProjectName)}\{vsFile.Path}\{this.Context.ControllerName}.{vsFile.FileExtension}";

            CodeGenerationHelper.AddCodeToClassBottom(controllerPath, method.GenerateCode().Template);            

            //this.Context.VsManager.AddVisualStudio(vsFile.ProjectName, vsFile.Path, this.Context.className);
        }

    }
}
