﻿using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.List.Items
{
    public class ContractClassGenerator : CSListGenerator, IGenerateItem
    {

       
        public ContractClassGenerator(GeneratorContext context) : base(context)
        {
            //this.Files.Add(new VsFile() { FileExtension = "cs", FileName = "_GridView", FileType = ListFileType.Contracts, Path = "" });
            this.FileType = ListFileType.Contracts;
        }
        public  void Execute()
        {

            CSClass @class = new CSClass();
            @class.Name = this.Context.className + "Dto";
            //@class.NameSpace = namespaceName + ".Contract.Queries";
            @class.Properties = new List<CSProperty>();

            foreach (var property in this.Context.classTree.Childs)
            {
                @class.Properties.Add(GetClassProperty(property.Name.Split('\\')[2]));
            }



            CSClass requestClass = new CSClass();
            //requestClass.NameSpace = namespaceName + ".Contract.Queries";
            requestClass.BaseClass = "BaseRequest";
            requestClass.Name = this.Context.className + "Request";

            CSClass responseClass = new CSClass();
            //responseClass.NameSpace = namespaceName + ".Contract.Queries";
            responseClass.BaseClass = "BaseResponse";
            responseClass.Name = this.Context.className + "Response";
            CSProperty dtoProperty = new CSProperty(null, "public", "Data", false);
            dtoProperty.PropertyTypeString = String.Format("List<{0}Dto>", this.Context.className);
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
            classes += @class.GenerateCode().Template + Environment.NewLine + requestClass.GenerateCode().Template + Environment.NewLine + responseClass.GenerateCode().Template + Environment.NewLine + @interface.GenerateCode().Template;
            classes += Environment.NewLine;
            classes += "}";

            this.Context.fileSystem.FormatCode(classes, "cs", this.Context.className);

            ListVsFile vsFile = this.GetVsFile();

            this.Context.VsManager.AddVisualStudio(vsFile.ProjectName, vsFile.Path, this.Context.className,vsFile.FileExtension);

        }

        

    }
}
