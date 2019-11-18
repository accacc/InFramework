using IF.CodeGeneration.Application.Generator.List;
using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Get.Items
{
    public class ContractClassGenerator : CSGetGenerator, IGenerateItem
    {

        public ContractClassGenerator(GeneratorContext context) : base(context)
        {
            this.FileType = GetFileType.ContractClass;
        }

        public void Execute()
        {

            CSClass dtoClass = new CSClass();
            dtoClass.Name = this.Context.className + "Dto";
            //@class.NameSpace = namespaceName + ".Contract.Queries";
            dtoClass.Properties = new List<CSProperty>();

            foreach (var property in this.Context.classTree.Childs)
            {
                dtoClass.Properties.Add(GetClassProperty(property.Name.Split('\\')[2]));
            }



            CSClass requestClass = new CSClass();
            //requestClass.NameSpace = namespaceName + ".Contract.Queries";
            requestClass.BaseClass = "BaseRequest";
            requestClass.Name = this.Context.className + "Request";
            requestClass.Properties.Add(new CSProperty(typeof(int),"public","Id",false));

            CSClass responseClass = new CSClass();
            //responseClass.NameSpace = namespaceName + ".Contract.Queries";
            responseClass.BaseClass = "BaseResponse";
            responseClass.Name = this.Context.className + "Response";
            CSProperty dtoProperty = new CSProperty(null, "public", "Data", false);
            dtoProperty.PropertyTypeString = $"{this.Context.className}Dto";
            responseClass.Properties.Add(dtoProperty);



            CSInterface @interface = new CSInterface();
            @interface.Name = GetDataQueryIntarfaceName();
            @interface.InheritedInterfaces.Add($"IDataGetQueryAsync<{this.Context.className}Request,{this.Context.className}Response>");

            string classes = "";
            classes += "using IF.Core.Data;";
            classes += Environment.NewLine;
            classes += "using System.Collections.Generic;";
            classes += Environment.NewLine;
            classes += Environment.NewLine;
            classes += "namespace " + this.Context.nameSpaceName + ".Contract.Queries";
            classes += Environment.NewLine;
            classes += "{";
            classes += Environment.NewLine;
            classes += dtoClass.GenerateCode().Template + Environment.NewLine + requestClass.GenerateCode().Template + Environment.NewLine + responseClass.GenerateCode().Template + Environment.NewLine + @interface.GenerateCode().Template;
            classes += Environment.NewLine;
            classes += "}";

            this.Context.fileSystem.FormatCode(classes, "cs", this.Context.className);

            GetVsFile vsFile = this.GetVsFile();

            this.Context.VsManager.AddVisualStudio(vsFile.ProjectName, vsFile.Path, this.Context.className, vsFile.FileExtension);

        }
    }
}