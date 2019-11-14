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
    public class CSListGenerator:CSGeneratorBase
    {

        

        public CSListGenerator(FileSystemCodeFormatProvider fileSystem):base(fileSystem)
        {
            
        }

        public void GenerateMvcGridView(string className, string namespaceName, ClassTree classTree, Type classType)
        {


            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"@model List<{namespaceName}.Models.{className}GridModel>");
            builder.AppendLine();


            builder.AppendLine("<table>");
            builder.AppendLine("<tr>");
            builder.AppendLine("<td>");

            builder.AppendLine("<a class=\"btn btn-primary\"");
            builder.AppendLine($"href=\"@Url.Action(\"{className}Create\")\"");
            builder.AppendLine("if-ajax=\"true\"");
            builder.AppendLine("if-ajax-method=\"get\"");
            builder.AppendLine("if-ajax-mode=\"replace\"");
            builder.AppendLine("if-ajax-show-dialog=\"true\"");
            builder.AppendLine("if-ajax-modal-id=\"@Guid.NewGuid()\">");
            builder.AppendLine("Ekle");
            builder.AppendLine("</a>");

            builder.AppendLine("</td>");
            builder.AppendLine("</tr>");
            builder.AppendLine("</table>");


            builder.AppendLine("<table class=\"table table-striped table-sm\">");
            builder.AppendLine("<tr>");

            CSClass gridClass = GenerateClass(className, classTree, classType);

            foreach (var item in gridClass.Properties)
            {
                builder.AppendLine("<th>");
                builder.AppendLine(item.Name);
                builder.AppendLine("</th>");
            }


            builder.AppendLine("</tr>");

            builder.AppendLine("@if(Model != null && Model.Any())");
            builder.AppendLine("{");
            builder.AppendLine("@foreach (var item in Model)");
            builder.AppendLine("{");
            builder.AppendLine("<tr>");

            foreach (var item in gridClass.Properties)
            {
                builder.AppendLine("<td>");
                builder.AppendLine($"@Html.DisplayFor(modelItem => item.{item.Name})");
                builder.AppendLine("</td>");
            }


            builder.AppendLine("<td>");

            builder.AppendLine("<a class=\"btn btn-primary\"");
            builder.AppendLine($"href=\"@Url.Action(\"{className}Edit\")\"");
            builder.AppendLine("if-ajax=\"true\"");
            builder.AppendLine("if-ajax-method=\"get\"");
            builder.AppendLine("if-ajax-mode=\"replace\"");
            builder.AppendLine("if-ajax-show-dialog=\"true\"");
            builder.AppendLine("if-ajax-modal-id=\"@Guid.NewGuid()\">");
            builder.AppendLine("Düzenle");
            builder.AppendLine("</a>");

            builder.AppendLine("</td>");

            builder.AppendLine("</tr>");

            builder.AppendLine("}");
            builder.AppendLine("}");
            builder.AppendLine("else");
            builder.AppendLine("{");
            builder.AppendLine("@:Veri bulunamadı, Lütfen Kriter seçiniz");
            builder.AppendLine("}");
            builder.AppendLine("</table>");

            fileSystem.FormatCode(builder.ToString(), "cshtml", "_GridView");
        }

        public void GenerateMvcIndexView(string className, string namespaceName, string title,ClassTree classTree, Type classType)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"@model List<{namespaceName}.Models.{className}GridModel>");
            builder.AppendLine();
            builder.AppendLine($"@{{ViewBag.Title = \"{title}\"; }}");
            builder.AppendLine();
            builder.AppendLine("@{");
            builder.AppendLine("Layout = \"~/Views/Shared/_GridLayout.cshtml\";");
            builder.AppendLine();
            builder.AppendLine("@section GridView");
            builder.AppendLine("{");
            builder.AppendLine();
            builder.AppendLine($"@{{await Html.RenderPartialAsync(\"~/Views/Security/{className}/_GridView.cshtml\", Model);}}");
            builder.AppendLine();
            builder.AppendLine("}");

            //@model List<Gedik.SSO.Models.ApplicationGridModel>

            //@{ViewBag.Title = "Uygulama Yönetimi"; }

            //@{
            //    Layout = "~/Views/Shared/_GridLayout.cshtml";
            //}

            //@section GridView
            //{
            //    @{await Html.RenderPartialAsync("~/Views/Security/Application/_GridView.cshtml", Model);}
            //}

            fileSystem.FormatCode(builder.ToString(), "cshtml", "Index");

        }

        public void GenerateControllerMethods(string className, string namespaceName, ClassTree classTree, Type classType)
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


        public void GenerateMvcModels(string className, string namespaceName, ClassTree classTree, Type classType)
        {
            CSClass gridClass = GenerateClass(className + "GridModel", classTree, classType);
            gridClass.NameSpace = namespaceName + "Models";
            fileSystem.FormatCode(gridClass.GenerateCode(), "cs");
        }

        public void GenerateHandlerClass(string className, string namespaceName, ClassTree classTree, Type classType)
        {
            CSClass @class = new CSClass();
            @class.Name = className + "Handler";
            @class.NameSpace = namespaceName + ".Queries.Cqrs";
            @class.Usings.Add("IF.Core.Data");
            @class.Usings.Add($"{namespaceName}.Contract.Queries");
            @class.Usings.Add("System.Threading.Tasks");
            @class.Usings.Add($"{namespaceName}.Persistence.EF.Queries");

            @class.InheritedInterfaces.Add($"IQueryHandlerAsync<{className}Request, {className}Response>");

            var repositoryProperty = new CSProperty("private", "query", false);
            repositoryProperty.PropertyTypeString = GetDataQueryIntarfaceName(className);
            repositoryProperty.IsReadOnly = true;
            @class.Properties.Add(repositoryProperty);


            CSMethod constructorMethod = new CSMethod(@class.Name, "", "public");
            constructorMethod.Parameters.Add(new CsMethodParameter() { Name = "query", Type = GetDataQueryIntarfaceName(className) });
            StringBuilder methodBody = new StringBuilder();
            methodBody.AppendFormat("this.query = query;");
            methodBody.AppendLine();
            constructorMethod.Body = methodBody.ToString();
            @class.Methods.Add(constructorMethod);

            CSMethod handleMethod = new CSMethod("Handle", className + "Response", "public");
            handleMethod.IsAsync = true;
            handleMethod.Parameters.Add(new CsMethodParameter() { Name = "request", Type = className + "Request" });
            handleMethod.Body += $"return await this.query.GetAsync(request);" + Environment.NewLine;

            @class.Methods.Add(handleMethod);

            fileSystem.FormatCode(@class.GenerateCode(), "cs");

        }

        public void GenerateDataQueryHandlerClass(string className, string namespaceName, ClassTree classTree, Type classType)
        {
            CSClass @class = new CSClass();

            @class.Name = GetDataQueryClassName(className);
            @class.NameSpace = namespaceName + ".Persistence.EF.Queries";

            @class.Usings.Add($"{namespaceName}.Contract.Queries");
            @class.Usings.Add($"{namespaceName}.Persistence.EF.Models");
            @class.Usings.Add("System.Threading.Tasks");
            @class.Usings.Add($"IF.Persistence");
            @class.Usings.Add($"System.Linq");
            @class.Usings.Add($"Microsoft.EntityFrameworkCore");


            @class.InheritedInterfaces.Add(GetDataQueryIntarfaceName(className));

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


            CSMethod handleMethod = new CSMethod("Get", className + "Response", "public");
            handleMethod.IsAsync = true;
            handleMethod.Parameters.Add(new CsMethodParameter() { Name = "request", Type = className + "Request" });
            handleMethod.Body += $"var data = await this.repository.GetQuery<{classType.Name}>()" + Environment.NewLine;
            handleMethod.Body += $".Select(x => new {className}Dto" + Environment.NewLine;
            handleMethod.Body += $"{{" + Environment.NewLine;

            foreach (var property in classTree.Childs)
            {
                CSProperty classProperty = GetClassProperty(classType, property.Name.Split('\\')[2]);
                handleMethod.Body += $"{classProperty.Name} = x.{classProperty.Name}," + Environment.NewLine;
            }

            handleMethod.Body += $"}}).ToListAsync();" + Environment.NewLine;

            handleMethod.Body += $"return new {className}Response {{ Data = data }};" + Environment.NewLine;

            @class.Methods.Add(handleMethod);

            fileSystem.FormatCode(@class.GenerateCode(), "cs");
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



            CSClass requestClass = new CSClass();
            //requestClass.NameSpace = namespaceName + ".Contract.Queries";
            requestClass.BaseClass = "BaseRequest";
            requestClass.Name = className + "Request";

            CSClass responseClass = new CSClass();
            //responseClass.NameSpace = namespaceName + ".Contract.Queries";
            responseClass.BaseClass = "BaseResponse";
            responseClass.Name = className + "Response";
            CSProperty dtoProperty = new CSProperty(null, "public", "Data", false);
            dtoProperty.PropertyTypeString = String.Format("List<{0}Dto>", className);
            responseClass.Properties.Add(dtoProperty);



            CSInterface @interface = new CSInterface();
            @interface.Name = GetDataQueryIntarfaceName(className);
            @interface.InheritedInterfaces.Add($"IDataGetQueryAsync<{className}Request,{className}Response>");

            string classes = "";
            classes += "using IF.Core.Data;";
            classes += Environment.NewLine;
            classes += "using System.Collections.Generic;";
            classes += Environment.NewLine;
            classes += Environment.NewLine;
            classes += "namespace " + namespaceName + ".Contract.Queries";
            classes += Environment.NewLine;
            classes += "{";
            classes += Environment.NewLine;
            classes += @class.GenerateCode().Template + Environment.NewLine + requestClass.GenerateCode().Template + Environment.NewLine + responseClass.GenerateCode().Template + Environment.NewLine + @interface.GenerateCode().Template;
            classes += Environment.NewLine;
            classes += "}";

            fileSystem.FormatCode(classes, "cs", className);

        }

        private static string GetDataQueryIntarfaceName(string className)
        {
            return $"I{className}DataQueryAsync";
        }

        private static string GetDataQueryClassName(string className)
        {
            return $"{className}DataQueryAsync";
        }

       
    }
}
