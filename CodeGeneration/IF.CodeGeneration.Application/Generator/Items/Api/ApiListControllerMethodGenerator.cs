using IF.CodeGeneration.Core;
using IF.CodeGeneration.Language.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.List.Items
{
    public class ApiListControllerMethodGenerator : ApplicationCodeGenerateItem
    {
        

        public  ApiListControllerMethodGenerator(ApplicationCodeGeneratorContext context) : base(context)
        {
            //this.Files.Add(new VsFile() { FileExtension = "cs", FileName = "_GridView", FileType = VSFileType.MvcMethods, Path = "" });

            this.FileType = VSFileType.ApiListControllerMethods;
        }

        public override void Execute()
        {
            CSMethod getMethod = new CSMethod($"{this.Context.className}", "IActionResult", "public");
            getMethod.IsAsync = true;
            getMethod.Attirubites.Add("HttpGet");
            getMethod.Attirubites.Add($"Route(\"api/{this.Context.ControllerName}/{this.Context.className}\")");
            getMethod.Parameters.Add(new CsMethodParameter() { Type = $"{this.Context.className}Request", Name = "request", Attirubite = "FromQuery" });


            StringBuilder getMethodBody = new StringBuilder();
            getMethodBody.AppendLine($"var response = await dispatcher.QueryAsync<{this.Context.className}Request, {this.Context.className}Response>(request);");
            getMethodBody.AppendLine($"return Ok(response);");
            getMethod.Body = getMethodBody.ToString();




            var methods = getMethod.GenerateCode().Template + Environment.NewLine;

            IFVsFile vsFile = this.GetVsFile();

            this.Context.fileSystem.FormatCode(methods, vsFile.FileExtension,vsFile.FileName,"");

            var controllerPath = $@"{this.Context.VsManager.GetProjectPath(vsFile.ProjectName)}\{vsFile.Path}\{vsFile.FileName}.{vsFile.FileExtension}";

            CodeGenerationHelper.AddCodeToClassBottom(controllerPath, methods);


            
        }

    }
}
