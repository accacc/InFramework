using IF.CodeGeneration.Application.Generator.Add.Items;
using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.CodeGeneration.Application.Generator
{
    public class CSInsertGenerator: CSGeneratorBase
    {

        public List<AddVsFile> Files { get; set; }
        public List<IGenerateItem> Items { get; set; }

        protected AddFileType FileType;

        public CSInsertGenerator(GeneratorContext context) : base(context)
        {
            this.Files = new List<AddVsFile>();
            this.Items = new List<IGenerateItem>();
            this.UpdateContext();
        }




        public void UpdateContext()
        {
            this.Files.Clear();
            this.Items.Clear();
            this.Files.Add(new AddVsFile() { ProjectName = "Contract", FileExtension = "cs", FileName = this.Context.className, FileType = AddFileType.ContractClass, Path = "Commands" });
            this.Files.Add(new AddVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = "Security", FileType = AddFileType.ControllerMethod, Path = "Controllers" });
            this.Files.Add(new AddVsFile() { ProjectName = "Persistence.EF", FileExtension = "cs", FileName = this.Context.className, FileType = AddFileType.DataHandler, Path = "Commands" });
            this.Files.Add(new AddVsFile() { ProjectName = "Cqrs", FileExtension = "cs", FileName = this.Context.className, FileType = AddFileType.Handler, Path = "Commands" });
            this.Files.Add(new AddVsFile() { ProjectName = "Admin.UI", FileExtension = "cshtml", FileName = "_Form", FileType = AddFileType.FormView, Path = $@"{this.Context.ViewBasePath}" });
            this.Files.Add(new AddVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = this.Context.className + "Model", FileType = AddFileType.MvcModels, Path = "Models" });
        }

        public void SetItemActive(AddFileType type)
        {


            switch (type)
            {
                case AddFileType.ContractClass:
                    
                    this.Items.Add(new ContractClassGenerator(this.Context));
                    break;
                case AddFileType.ControllerMethod:
                    this.Items.Add(new ControllerMethodGenerator(this.Context));
                    break;
                case AddFileType.DataHandler:
                    this.Items.Add(new DataHandlerGenerator(this.Context));
                    break;
                case AddFileType.FormView:
                    this.Items.Add(new MvcFormViewGenerator(this.Context));
                    break;
                case AddFileType.Handler:
                    this.Items.Add(new HandlerGenerator(this.Context));
                    break;
                case AddFileType.MvcModels:
                    this.Items.Add(new MvcModelGenerator(this.Context));
                    break;
                default:
                    break;
            }

        }

        public void Generate()
        {

            foreach (var item in Items)
            {
                item.Execute();
            }
        }

        public AddVsFile GetVsFile()
        {
            return this.Files.SingleOrDefault(f => f.FileType == this.FileType);
        }











    }
}
