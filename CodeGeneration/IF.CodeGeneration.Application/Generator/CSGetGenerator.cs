﻿using IF.CodeGeneration.Application.Generator.Get.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Get
{
    public class CSGetGenerator : CSApplicationCodeGeneratorEngineBase
    {


        public CSGetGenerator(ApplicationCodeGeneratorContext context) : base(context)
        {
           
            this.UpdateContext();
        }




        public override void UpdateContext()
        {
            this.Context.Files.Clear();
            this.Items.Clear();
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Contract", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.GetContractClass, Path = "Queries" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = "Security", FileType = VSFileType.GetControllerMethod, Path = "Controllers" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Persistence.EF", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.GetDataHandler, Path = "Queries" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Cqrs", FileExtension = "cs", FileName = this.Context.className +"Handler", FileType = VSFileType.GetHandler, Path = "Queries" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cshtml", FileName = "_FormView", FileType = VSFileType.GetFormView, Path = $@"{this.Context.RepositoryName}" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = this.Context.className + "Model", FileType = VSFileType.GetMvcModels, Path = "Models" });
        }

        public override void SetItemActive(VSFileType type)
        {


            switch (type)
            {
                case VSFileType.GetContractClass:
                    this.Items.Add(new GetContractClassGenerator(this.Context));
                    break;
                case VSFileType.GetControllerMethod:
                    this.Items.Add(new GetControllerMethodGenerator(this.Context));
                    break;
                case VSFileType.GetDataHandler:
                    this.Items.Add(new GetDataHandlerGenerator(this.Context));
                    break;
                case VSFileType.GetFormView:
                    this.Items.Add(new GetMvcFormViewGenerator(this.Context));
                    break;
                case VSFileType.GetHandler:
                    this.Items.Add(new GetHandlerGenerator(this.Context));
                    break;
                case VSFileType.GetMvcModels:
                    this.Items.Add(new GetMvcModelGenerator(this.Context));
                    break;
                default:
                    break;
            }

        }
    }
}
