using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.List.Items
{
    public class ControllerMethodGenerator : CSListGenerator, IGenerateItem
    {
        

        public  ControllerMethodGenerator(FileSystemCodeFormatProvider fileSystem, string className, string nameSpaceName, ClassTree classTree, Type classType)
            :base(fileSystem,className,nameSpaceName,classTree,classType)
        {
            this.File = new VsFile() { FileExtension = "cs", FileName = "_GridView", FileType = ListFileType.MvcMethods, Path = "" };


        }

        public void Execute()
        {
            CSMethod method = new CSMethod(className + "Index", "ActionResult", "public");
            method.IsAsync = true;

            StringBuilder methodBody = new StringBuilder();


            //var list = await this.dispatcher.QueryAsync<ApplicationRequest, ApplicationResponse>(new ApplicationRequest());
            //var model = list.MapTo<ApplicationGridModel>();
            //return View("~/Views/Security/Application/Index.cshtml", model);

            methodBody.AppendLine($"var list = await this.dispatcher.QueryAsync<{className}Request, {className}Response>(new {className}Request());");
            methodBody.AppendLine($"var model = list.Data.MapTo<{className}GridModel>();");
            methodBody.AppendFormat($"return View(\"~/Views/{className}/Index.cshtml\", model);");
            method.Body = methodBody.ToString();

            fileSystem.FormatCode(method.GenerateCode(), "cs");
        }

    }
}
