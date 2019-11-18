//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace IF.CodeGeneration.Application.Generator.Add.Items
//{
//    class HandlerGenerator
//    {
//    }

//    public void GenerateHandlerClass(string className, string namespaceName, ClassTree classTree, Type classType)
//    {
//        CSClass @class = new CSClass();
//        @class.Name = className + "DataHandler";
//        @class.NameSpace = namespaceName + ".Commands.Cqrs";
//        @class.Usings.Add("IF.Core.Data");
//        @class.Usings.Add($"{namespaceName}.Contract.Commands");
//        @class.Usings.Add("System.Threading.Tasks");
//        @class.Usings.Add($"{namespaceName}.Persistence.EF.Commands");

//        @class.InheritedInterfaces.Add($"ICommandHandlerAsync<{className}Command>");

//        var repositoryProperty = new CSProperty("private", "dataCommand", false);
//        repositoryProperty.PropertyTypeString = GetDataInsertCommandIntarfaceName(className);
//        repositoryProperty.IsReadOnly = true;
//        @class.Properties.Add(repositoryProperty);


//        CSMethod constructorMethod = new CSMethod(@class.Name, "", "public");
//        constructorMethod.Parameters.Add(new CsMethodParameter() { Name = "dataCommand", Type = GetDataInsertCommandIntarfaceName(className) });
//        StringBuilder methodBody = new StringBuilder();
//        methodBody.AppendFormat("this.dataCommand = dataCommand;");
//        methodBody.AppendLine();
//        constructorMethod.Body = methodBody.ToString();
//        @class.Methods.Add(constructorMethod);

//        CSMethod handleMethod = new CSMethod("Handle", "void", "public");
//        handleMethod.IsAsync = true;
//        handleMethod.Parameters.Add(new CsMethodParameter() { Name = "command", Type = $"{className}Command" });
//        handleMethod.Body += $"await this.dataCommand.ExecuteAsync(command);" + Environment.NewLine;

//        @class.Methods.Add(handleMethod);

//        this.Context.fileSystem.FormatCode(@class.GenerateCode(), "cs");

//    }
//}
