﻿using IF.CodeGeneration.Application.Generator.List;
using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Get.Items
{
    public class ControllerMethodGenerator : CSGetGenerator, IGenerateItem
    {


        public ControllerMethodGenerator(GeneratorContext context) : base(context)
        {
            this.FileType = GetFileType.ControllerMethod;
        }
        public void Execute()
        {

            CSMethod getMethod = new CSMethod($"{this.Context.className}", "ActionResult", "public");
            getMethod.IsAsync = true;
            getMethod.Attirubites.Add("HttpGet");
            getMethod.Parameters.Add(new CsMethodParameter { Name = "Id", Type = "int" });


            StringBuilder getMethodBody = new StringBuilder();
            getMethodBody.AppendLine($"var response = await dispatcher.QueryAsync<{this.Context.className}Request, {this.Context.className}Response>(new {this.Context.className}Request(){{ Id = Id }});");
            getMethodBody.AppendLine($"var model = response.Data.MapTo<{this.Context.className}Model>();");
            getMethodBody.AppendLine($"return View(\"~/{this.Context.ViewBasePath}/_FormView.cshtml\",model);");
            getMethod.Body = getMethodBody.ToString();




            var methods = getMethod.GenerateCode().Template + Environment.NewLine;

            GetVsFile vsFile = this.GetVsFile();

            this.Context.fileSystem.FormatCode(methods, vsFile.FileExtension, "Controller");

            

            var controllerPath = $@"{this.Context.VsManager.GetProjectPath(vsFile.ProjectName)}\{vsFile.Path}\{this.Context.ControllerName}.{vsFile.FileExtension}";

            CodeGenerationHelper.AddCodeBottom(controllerPath, methods);
        }
    }
}