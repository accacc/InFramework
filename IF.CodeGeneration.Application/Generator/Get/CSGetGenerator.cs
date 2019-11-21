using IF.CodeGeneration.Application.Generator.Get.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Get
{
    public class CSGetGenerator : CSGeneratorBase
    {

        public List<IFVsFile> Files { get; set; }
        public List<IGenerateItem> Items { get; set; }

        protected VSFileType FileType;

        public CSGetGenerator(GeneratorContext context) : base(context)
        {
            this.Files = new List<IFVsFile>();
            this.Items = new List<IGenerateItem>();
            this.UpdateContext();
        }




        public void UpdateContext()
        {
            this.Files.Clear();
            this.Items.Clear();
            this.Files.Add(new IFVsFile() { ProjectName = "Contract", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.GetContractClass, Path = "Queries" });
            this.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = "Security", FileType = VSFileType.GetControllerMethod, Path = "Controllers" });
            this.Files.Add(new IFVsFile() { ProjectName = "Persistence.EF", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.GetDataHandler, Path = "Queries" });
            this.Files.Add(new IFVsFile() { ProjectName = "Cqrs", FileExtension = "cs", FileName = this.Context.className +"Handler", FileType = VSFileType.GetHandler, Path = "Queries" });
            this.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cshtml", FileName = "_FormView", FileType = VSFileType.GetFormView, Path = $@"{this.Context.ViewBasePath}" });
            this.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = this.Context.className + "Model", FileType = VSFileType.GetMvcModels, Path = "Models" });
        }

        public void SetItemActive(VSFileType type)
        {


            switch (type)
            {
                case VSFileType.GetContractClass:
                    this.Items.Add(new ContractClassGenerator(this.Context));
                    break;
                case VSFileType.GetControllerMethod:
                    this.Items.Add(new ControllerMethodGenerator(this.Context));
                    break;
                case VSFileType.GetDataHandler:
                    this.Items.Add(new DataHandlerGenerator(this.Context));
                    break;
                case VSFileType.GetFormView:
                    this.Items.Add(new MvcFormViewGenerator(this.Context));
                    break;
                case VSFileType.GetHandler:
                    this.Items.Add(new HandlerGenerator(this.Context));
                    break;
                case VSFileType.GetMvcModels:
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

        public IFVsFile GetIFVsFile()
        {
            return this.Files.SingleOrDefault(f => f.FileType == this.FileType);
        }











    }
}
