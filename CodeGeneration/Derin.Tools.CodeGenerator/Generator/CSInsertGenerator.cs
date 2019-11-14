using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using IF.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Tools.CodeGenerator.Generator
{
    public class CSInsertGenerator: CSGeneratorBase
    {
        public CSInsertGenerator(FileSystemCodeFormatProvider fileSystem):base(fileSystem)
        {
            
        }


        public void GenerateContractClasses(string className, string namespaceName, ClassTree classTree, Type classType)
        {

            CSClass @class = new CSClass();
            @class.Name = className + "Dto";
            //@class.NameSpace = namespaceName + ".Contract.Queries";
            @class.Properties = new List<CSProperty>();

            foreach (var property in classTree.Childs)
            {
                @class.Properties.Add(GetClassProperty(classType, property.Name.Split('\\')[2]));
            }



            CSClass commandClass = new CSClass();
            commandClass.BaseClass = base.BaseCommandName;
            commandClass.Name = className + "Command";
            CSProperty dtoProperty = new CSProperty(null, "public", "Data", false);
            dtoProperty.PropertyTypeString = $"{className}Dto";
            commandClass.Properties.Add(dtoProperty);



            CSInterface @interface = new CSInterface();
            @interface.Name = GetDataInsertCommandIntarfaceName(className);
            @interface.InheritedInterfaces.Add($"IDataInsertCommandAsync<{className}Command>");

            string classes = "";
            classes += "using IF.Core.Data;";
            classes += Environment.NewLine;
            classes += "using System.Collections.Generic;";
            classes += Environment.NewLine;
            classes += Environment.NewLine;
            classes += "namespace " + namespaceName + ".Contract.Commands";
            classes += Environment.NewLine;
            classes += "{";
            classes += Environment.NewLine;
            classes += @class.GenerateCode().Template + Environment.NewLine + commandClass.GenerateCode().Template + Environment.NewLine + @interface.GenerateCode().Template;
            classes += Environment.NewLine;
            classes += "}";

            fileSystem.FormatCode(classes, "cs", className);

        }

        public void GenerateDataQueryHandlerClass(string className, string namespaceName, ClassTree classTree, Type classType)
        {
            CSClass @class = new CSClass();

            @class.Name = GetDataQueryClassName(className);
            @class.NameSpace = namespaceName + ".Persistence.EF.Commands";

            @class.Usings.Add($"{namespaceName}.Contract.Commands");
            @class.Usings.Add($"{namespaceName}.Persistence.EF.Models");
            @class.Usings.Add("System.Threading.Tasks");
            @class.Usings.Add($"IF.Persistence");
            @class.Usings.Add($"System.Linq");
            @class.Usings.Add($"Microsoft.EntityFrameworkCore");


            @class.InheritedInterfaces.Add(GetDataInsertCommandIntarfaceName(className));

            var repositoryProperty = new CSProperty(typeof(IRepository), "private", "repository", false);
            repositoryProperty.IsReadOnly = true;
            @class.Properties.Add(repositoryProperty);


            CSMethod constructorMethod = new CSMethod(@class.Name, "", "public");
            constructorMethod.Parameters.Add(new CsMethodParameter() { Name = "repository", Type = "IRepository" });
            StringBuilder methodBody = new StringBuilder();
            methodBody.AppendFormat("this.repository = repository;");
            methodBody.AppendLine();
            constructorMethod.Body = methodBody.ToString();
            @class.Methods.Add(constructorMethod);


            CSMethod handleMethod = new CSMethod("Execute", "void", "public");
            handleMethod.IsAsync = true;
            handleMethod.Parameters.Add(new CsMethodParameter() { Name = "command", Type = className + "Command" });


            handleMethod.Body += $"{classType.Name} entity = new {classType.Name}();" + Environment.NewLine;
            

            foreach (var property in classTree.Childs)
            {
                CSProperty classProperty = GetClassProperty(classType, property.Name.Split('\\')[2]);
                handleMethod.Body += $"entity.{classProperty.Name} = command.Data.{classProperty.Name};" + Environment.NewLine;
            }

            handleMethod.Body += $"this.repository.Add(entity);" + Environment.NewLine;

            handleMethod.Body += $"await this.repository.UnitOfWork.SaveChangesAsync();" + Environment.NewLine;
            handleMethod.Body += $"command.Data.Id = entity.Id;" + Environment.NewLine;

            @class.Methods.Add(handleMethod);

            fileSystem.FormatCode(@class.GenerateCode(), "cs");
        }


        public void GenerateHandlerClass(string className, string namespaceName, ClassTree classTree, Type classType)
        {
            CSClass @class = new CSClass();
            @class.Name = className + "DataHandler";
            @class.NameSpace = namespaceName + ".Commands.Cqrs";
            @class.Usings.Add("IF.Core.Data");
            @class.Usings.Add($"{namespaceName}.Contract.Commands");
            @class.Usings.Add("System.Threading.Tasks");
            @class.Usings.Add($"{namespaceName}.Persistence.EF.Commands");

            @class.InheritedInterfaces.Add($"ICommandHandlerAsync<{className}Command>");

            var repositoryProperty = new CSProperty("private", "dataCommand", false);
            repositoryProperty.PropertyTypeString = GetDataInsertCommandIntarfaceName(className);
            repositoryProperty.IsReadOnly = true;
            @class.Properties.Add(repositoryProperty);


            CSMethod constructorMethod = new CSMethod(@class.Name, "", "public");            
            constructorMethod.Parameters.Add(new CsMethodParameter() { Name = "dataCommand", Type = GetDataInsertCommandIntarfaceName(className) });
            StringBuilder methodBody = new StringBuilder();
            methodBody.AppendFormat("this.dataCommand = dataCommand;");
            methodBody.AppendLine();
            constructorMethod.Body = methodBody.ToString();
            @class.Methods.Add(constructorMethod);

            CSMethod handleMethod = new CSMethod("Handle","void", "public");
            handleMethod.IsAsync = true;
            handleMethod.Parameters.Add(new CsMethodParameter() { Name = "command", Type = $"{className}Command" });
            handleMethod.Body += $"await this.dataCommand.ExecuteAsync(command);" + Environment.NewLine;

            @class.Methods.Add(handleMethod);

            fileSystem.FormatCode(@class.GenerateCode(), "cs");

        }


        public void GenerateControllerMethods(string className, string namespaceName, ClassTree classTree, Type classType)
        {

            //    public ActionResult RoleCreate()
            //    {
            //        return View("~/Views/Security/RolePermission/RoleCreateForm.cshtml", new RoleFormModel());
            //    }

            //var campaignDto = model.MapTo<Dto.CampaignDto>();

            //CampaignInsertCommand campaignInsertCommand = new CampaignInsertCommand();
            //campaignInsertCommand.Campaign = campaignDto;

            //dispatcher.Command(campaignInsertCommand);


            //this.ShowMessage(OperationType.Insert);
            //return View("_Tab", model);

            CSMethod getMethod = new CSMethod($"{className}Create", "ActionResult", "public");
            getMethod.IsAsync = true;
            getMethod.Attirubites.Add("HttpGet");
            StringBuilder methodBody = new StringBuilder();          
            methodBody.AppendLine($"return View(\"~/Views/Application/_Form.cshtml\", new {className}Model());");            
            getMethod.Body = methodBody.ToString();

            CSMethod postMethod = new CSMethod($"{className}Create", "ActionResult", "public");
            postMethod.Parameters.Add(new CsMethodParameter() { Type= $"{className}Model",Name="model" });
            postMethod.IsAsync = true;
            postMethod.Attirubites.Add("HttpPost");
            methodBody = new StringBuilder();
            methodBody.AppendLine($"var dto = model.MapTo<{className}Dto>();");
            methodBody.AppendLine($"{className}Command command = new {className}Command();");
            methodBody.AppendLine($"command.Data = dto;");
            methodBody.AppendLine($"await dispatcher.CommandAsync(command);");
            methodBody.AppendLine($"this.ShowMessage(OperationType.Insert);");
            methodBody.AppendLine($"return View(\"~/Views/Application/_Form.cshtml\",model);");
            postMethod.Body = methodBody.ToString();


            var methods = getMethod.GenerateCode().Template + Environment.NewLine + postMethod.GenerateCode().Template + Environment.NewLine;

            fileSystem.FormatCode(methods, "cs","Controller");
        }

        public void GenerateMvcModels(string className, string namespaceName, ClassTree classTree, Type classType)
        {
            CSClass gridClass = GenerateClass(className + "Model", classTree, classType);
            gridClass.NameSpace = namespaceName;
            fileSystem.FormatCode(gridClass.GenerateCode(), "cs");
        }

        private string GetDataInsertCommandIntarfaceName(string className)
        {
            return $"I{className}DataCommandAsync";
        }

        private static string GetDataQueryClassName(string className)
        {
            return $"{className}DataCommandAsync";
        }
    }
}
