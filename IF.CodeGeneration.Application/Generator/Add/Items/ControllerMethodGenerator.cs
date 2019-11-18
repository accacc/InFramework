//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace IF.CodeGeneration.Application.Generator.Add.Items
//{
//    class ControllerMethodGenerator
//    {
//    }

//    public void GenerateControllerMethods(string className, string namespaceName, ClassTree classTree, Type classType)
//    {

//        CSMethod getMethod = new CSMethod($"{className}", "ActionResult", "public");
//        getMethod.IsAsync = true;
//        getMethod.Attirubites.Add("HttpGet");
//        StringBuilder methodBody = new StringBuilder();
//        methodBody.AppendLine($"return View(\"~/Views/Application/_Form.cshtml\", new {className}Model());");
//        getMethod.Body = methodBody.ToString();

//        CSMethod postMethod = new CSMethod($"{className}", "ActionResult", "public");
//        postMethod.Parameters.Add(new CsMethodParameter() { Type = $"{className}Model", Name = "model" });
//        postMethod.IsAsync = true;
//        postMethod.Attirubites.Add("HttpPost");
//        methodBody = new StringBuilder();
//        methodBody.AppendLine($"var dto = model.MapTo<{className}Dto>();");
//        methodBody.AppendLine($"{className}Command command = new {className}Command();");
//        methodBody.AppendLine($"command.Data = dto;");
//        methodBody.AppendLine($"await dispatcher.CommandAsync(command);");
//        methodBody.AppendLine($"this.ShowMessage(OperationType.Insert);");
//        methodBody.AppendLine($"return View(\"~/Views/Application/_Form.cshtml\",model);");
//        postMethod.Body = methodBody.ToString();


//        var methods = getMethod.GenerateCode().Template + Environment.NewLine + postMethod.GenerateCode().Template + Environment.NewLine;

//        this.Context.fileSystem.FormatCode(methods, "cs", "Controller");
//    }
//}
