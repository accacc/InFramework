using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Items
{ 


    public class ApiGetControllerMethodGenerator : ApiCsGetGenerator, IGenerateItem
    {


        public ApiGetControllerMethodGenerator(GeneratorContext context) : base(context)
        {
            this.FileType = VSFileType.ApiGetControllerMethod;
        }
        public void Execute()
        {

            CSMethod getMethod = new CSMethod($"{this.Context.className}", "IActionResult", "public");
            getMethod.IsAsync = true;
            getMethod.Attirubites.Add("HttpGet");
            getMethod.Attirubites.Add($"Route(\"api/{this.Context.ControllerName}/{this.Context.className}\")");
            getMethod.Parameters.Add(new CsMethodParameter() { Type = $"{this.Context.className}Request", Name = "request", Attirubite = "FromQuery" });


            StringBuilder getMethodBody = new StringBuilder();
            getMethodBody.AppendLine($"var response = await dispatcher.QueryAsync<{this.Context.className}Request, {this.Context.className}Response>(new {this.Context.className}Request());");
            getMethodBody.AppendLine($"return Ok(response);");
            getMethod.Body = getMethodBody.ToString();




            var methods = getMethod.GenerateCode().Template + Environment.NewLine;

            IFVsFile vsFile =  this.GetVsFile();

            this.Context.fileSystem.FormatCode(methods, vsFile.FileExtension, "Controller");



            var controllerPath = $@"{this.Context.VsManager.GetProjectPath(vsFile.ProjectName)}\{vsFile.Path}\{vsFile.FileName}.{vsFile.FileExtension}";

            CodeGenerationHelper.AddCodeBottom(controllerPath, methods);
        }
    }
}

