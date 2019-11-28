using IF.CodeGeneration.Application.Generator.List;
using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Add.Items
{
    public class AddControllerMethodGenerator : CSInsertGenerator, IGenerateItem
    {


        public AddControllerMethodGenerator(GeneratorContext context) : base(context)
        {
            this.FileType = VSFileType.AddControllerMethod;
        }
        public void Execute()
        {

            CSMethod getMethod = new CSMethod($"{this.Context.className}", "ActionResult", "public");
            getMethod.IsAsync = true;
            getMethod.Attirubites.Add("HttpGet");
            StringBuilder methodBody = new StringBuilder();
            methodBody.AppendLine($"return View(\"~/{this.Context.RepositoryName}/_Form.cshtml\", new {this.Context.className}Model());");
            getMethod.Body = methodBody.ToString();

            CSMethod postMethod = new CSMethod($"{this.Context.className}", "ActionResult", "public");
            postMethod.Parameters.Add(new CsMethodParameter() { Type = $"{this.Context.className}Model", Name = "model" });
            postMethod.IsAsync = true;
            postMethod.Attirubites.Add("HttpPost");
            methodBody = new StringBuilder();
            methodBody.AppendLine($"var dto = model.MapTo<{this.Context.className}Dto>();");
            methodBody.AppendLine($"{this.Context.className}Command command = new {this.Context.className}Command();");
            methodBody.AppendLine($"command.Data = dto;");
            methodBody.AppendLine($"await dispatcher.CommandAsync(command);");
            methodBody.AppendLine($"this.ShowMessage(OperationType.Insert);");
            methodBody.AppendLine($"return View(\"~/{this.Context.RepositoryName}/_Form.cshtml\",model);");
            postMethod.Body = methodBody.ToString();


            var methods = getMethod.GenerateCode().Template + Environment.NewLine + postMethod.GenerateCode().Template + Environment.NewLine;

            this.Context.fileSystem.FormatCode(methods, "cs", "Controller");

            IFVsFile vsFile = this.GetVsFile();

            var controllerPath = $@"{this.Context.VsManager.GetProjectPath(vsFile.ProjectName)}\{vsFile.Path}\{this.Context.ControllerName}.{vsFile.FileExtension}";

            CodeGenerationHelper.AddCodeToClassBottom(controllerPath, methods);
        }
    }
}