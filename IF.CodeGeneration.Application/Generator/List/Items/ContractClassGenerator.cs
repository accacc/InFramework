using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.List.Items
{
    public class ContractClassGenerator : CSListGenerator, IGenerateItem
    {
        public ContractClassGenerator(FileSystemCodeFormatProvider fileSystem, string className, string nameSpaceName, ClassTree classTree, Type classType)
            : base(fileSystem, className, nameSpaceName, classTree, classType)
        {
            this.File = new VsFile() { FileExtension = "cs", FileName = "_GridView", FileType = ListFileType.Contracts, Path = "" };
        }
        public  void Execute()
        {

            CSClass @class = new CSClass();
            @class.Name = className + "Dto";
            //@class.NameSpace = namespaceName + ".Contract.Queries";
            @class.Properties = new List<CSProperty>();

            foreach (var property in classTree.Childs)
            {
                @class.Properties.Add(GetClassProperty(property.Name.Split('\\')[2]));
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
            @interface.Name = GetDataQueryIntarfaceName();
            @interface.InheritedInterfaces.Add($"IDataGetQueryAsync<{className}Request,{className}Response>");

            string classes = "";
            classes += "using IF.Core.Data;";
            classes += Environment.NewLine;
            classes += "using System.Collections.Generic;";
            classes += Environment.NewLine;
            classes += Environment.NewLine;
            classes += "namespace " + nameSpaceName + ".Contract.Queries";
            classes += Environment.NewLine;
            classes += "{";
            classes += Environment.NewLine;
            classes += @class.GenerateCode().Template + Environment.NewLine + requestClass.GenerateCode().Template + Environment.NewLine + responseClass.GenerateCode().Template + Environment.NewLine + @interface.GenerateCode().Template;
            classes += Environment.NewLine;
            classes += "}";

            fileSystem.FormatCode(classes, "cs", className);

        }

        public bool IsActive()
        {
            throw new NotImplementedException();
        }
    }
}
