using IF.CodeGeneration.Application.Generator.List;
using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Update.Items
{
    public class ControllerMethodGenerator : CSUpdateGenerator, IGenerateItem
    {


        public ControllerMethodGenerator(GeneratorContext context) : base(context)
        {
            this.FileType = UpdateFileType.ControllerMethod;
        }
        public void Execute()
        {

            CSMethod getMethod = new CSMethod($"{this.Context.className}", "ActionResult", "public");
            getMethod.IsAsync = true;
            getMethod.Attirubites.Add("HttpGet");
            getMethod.Parameters.Add(new CsMethodParameter { Name = "Id", Type = "int" });


            StringBuilder getMethodBody = new StringBuilder();
            getMethodBody.AppendLine($"var response = dispatcher.Query<{this.Context.className}GetRequest, {this.Context.className}GetResponse>(new CampaignGetRequest(){{ Id = Id }});");
            getMethodBody.AppendLine($"var model = response.{this.Context.className}.MapTo<{this.Context.className}Model>();");
            getMethodBody.AppendLine($"return View(\"~/{this.Context.ViewBasePath}/_Form.cshtml\",model);");
            getMethod.Body = getMethodBody.ToString();

            CSMethod postMethod = new CSMethod($"{this.Context.className}", "ActionResult", "public");
            postMethod.Parameters.Add(new CsMethodParameter() { Type = $"{this.Context.className}Model", Name = "model" });
            postMethod.IsAsync = true;
            postMethod.Attirubites.Add("HttpPost");
            getMethodBody = new StringBuilder();
            getMethodBody.AppendLine($"var dto = model.MapTo<{this.Context.className}Dto>();");
            getMethodBody.AppendLine($"{this.Context.className}Command command = new {this.Context.className}Command();");
            getMethodBody.AppendLine($"command.Data = dto;");
            getMethodBody.AppendLine($"await dispatcher.CommandAsync(command);");
            getMethodBody.AppendLine($"this.ShowMessage(OperationType.Update);");
            getMethodBody.AppendLine($"return View(\"~/{this.Context.ViewBasePath}/_Form.cshtml\",model);");
            postMethod.Body = getMethodBody.ToString();


            var methods = getMethod.GenerateCode().Template + Environment.NewLine + postMethod.GenerateCode().Template + Environment.NewLine;

            this.Context.fileSystem.FormatCode(methods, "cs", "Controller");

            UpdateVsFile vsFile = this.GetVsFile();

            var controllerPath = $@"{this.Context.VsManager.GetProjectPath(vsFile.ProjectName)}\{vsFile.Path}\{this.Context.ControllerName}.{vsFile.FileExtension}";

            CodeGenerationHelper.AddCodeBottom(controllerPath, methods);
        }
    }
}