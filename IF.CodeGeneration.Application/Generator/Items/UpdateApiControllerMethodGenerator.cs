using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Items
{
    

    public class UpdateApiControllerMethodGenerator : CsApiUpdateGenerator, IGenerateItem
    {


        public UpdateApiControllerMethodGenerator(GeneratorContext context) : base(context)
        {
            this.FileType = VSFileType.UpdateApiControllerMethod;
        }
        public void Execute()
        {



        //     [HttpPost]
        //[Route("api/Role/CreateRole")]
        //public async Task<IActionResult> CreateRole([FromBody] RoleCreateCommand command)
        //{
        //    await _dispatcher.CommandAsync(command);
        //    return Ok(command);
        //}

      

            CSMethod postMethod = new CSMethod($"{this.Context.className}", "IActionResult", "public");
            postMethod.Parameters.Add(new CsMethodParameter() { Type = $"{this.Context.className}Command", Name = "command" ,Attirubite="FromBody" });
            postMethod.IsAsync = true;
            postMethod.Attirubites.Add("HttpPost");
            postMethod.Attirubites.Add($"Route(\"api/{this.Context.ControllerName}/{this.Context.className}\")");

            StringBuilder postMethodBody = new StringBuilder();
            postMethodBody.AppendLine($"await dispatcher.CommandAsync(command);");
            postMethodBody.AppendLine($"return Ok(command);");

            postMethod.Body = postMethodBody.ToString();


            var methods = postMethod.GenerateCode().Template + Environment.NewLine;

            IFVsFile vsFile = this.GetVsFile();

            this.Context.fileSystem.FormatCode(methods,vsFile.FileExtension , vsFile.FileName);            

            var controllerPath = $@"{this.Context.VsManager.GetProjectPath(vsFile.ProjectName)}\{vsFile.Path}\{vsFile.FileName}.{vsFile.FileExtension}";

            CodeGenerationHelper.AddCodeBottom(controllerPath, methods);
        }
    }
}
