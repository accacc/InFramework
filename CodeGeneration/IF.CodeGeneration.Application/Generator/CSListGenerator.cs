﻿using IF.CodeGeneration.Application.Generator.List.Items;
using System.Collections.Generic;
using System.Linq;

namespace IF.CodeGeneration.Application.Generator.List
{
    public class CSListGenerator : CSApplicationCodeGeneratorEngineBase
    {
        //public List<IFVsFile> Files { get; set; }
        //public List<ApplicationCodeGenerateItem> Items { get; set; }

        //protected VSFileType FileType;

        

        public CSListGenerator(ApplicationCodeGeneratorContext context) : base(context)
        {
            //this.Files = new List<IFVsFile>();
            //this.Items = new List<ApplicationCodeGenerateItem>();
            this.UpdateContext();
            
        }

        public override void UpdateContext()
        {
            this.Context.Files.Clear();
            this.Items.Clear();
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cshtml", FileName = "_GridView", FileType = VSFileType.ListGridview, Path = $@"{this.Context.RepositoryName}" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Contract", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.ListContracts, Path = "Queries" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Persistence.EF", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.ListDataHandler, Path = "Queries" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = this.Context.className + "GridModel", FileType = VSFileType.ListMvcModel, Path = "Models" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Cqrs", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.ListHandler, Path = "Queries" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = "Security", FileType = VSFileType.ListMvcControllerMethods, Path = "Controllers" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cshtml", FileName = "Index", FileType = VSFileType.ListIndexView, Path = $@"{this.Context.RepositoryName}" });
        }

        public override void SetItemActive(VSFileType type)
        {
         

            switch (type)
            {
                case VSFileType.ListGridview:
                    this.Items.Add(new ListMvcGridViewGenerator(this.Context));
                    break;
                case VSFileType.ListContracts:
                    this.Items.Add(new ListContractClassGenerator(this.Context));
                    break;
                case VSFileType.ListDataHandler:
                    this.Items.Add(new ListDataHandlerClass(this.Context));
                    break;
                case VSFileType.ListMvcModel:
                    this.Items.Add(new ListMvcModelGenerator(this.Context));
                    break;
                case VSFileType.ListHandler:
                    this.Items.Add(new ListHandlerClassGenerator(this.Context));
                    break;
                case VSFileType.ListMvcControllerMethods:
                    this.Items.Add(new ListControllerMethodGenerator(this.Context));
                    break;
                case VSFileType.ListIndexView:
                    this.Items.Add(new ListMvcIndexViewGenerator(this.Context));
                    break;
                default:
                    break;
            }

        }       
    }
}
