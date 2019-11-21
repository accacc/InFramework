using IF.CodeGeneration.Application.Generator.Update.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Update
{
    public class CSUpdateGenerator : CSGeneratorBase
    {

        public List<IFVsFile> Files { get; set; }
        public List<IGenerateItem> Items { get; set; }

        protected VSFileType FileType;

        public CSUpdateGenerator(GeneratorContext context) : base(context)
        {
            this.Files = new List<IFVsFile>();
            this.Items = new List<IGenerateItem>();
            this.UpdateContext();
        }




        public void UpdateContext()
        {
            this.Files.Clear();
            this.Items.Clear();
            this.Files.Add(new IFVsFile() { ProjectName = "Contract", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.UpdateContractClass, Path = "Commands" });
            this.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = "Security", FileType = VSFileType.UpdateControllerMethod, Path = "Controllers" });
            this.Files.Add(new IFVsFile() { ProjectName = "Persistence.EF", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.UpdateDataHandler, Path = "Commands" });
            this.Files.Add(new IFVsFile() { ProjectName = "Cqrs", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.UpdateHandler, Path = "Commands" });
            this.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cshtml", FileName = "_Form", FileType = VSFileType.UpdateFormView, Path = $@"{this.Context.ViewBasePath}" });
            this.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = this.Context.className + "Model", FileType = VSFileType.UpdateMvcModels, Path = "Models" });
        }

        public void SetItemActive(VSFileType type)
        {


            switch (type)
            {
                case VSFileType.UpdateContractClass:
                    this.Items.Add(new ContractClassGenerator(this.Context));
                    break;
                case VSFileType.UpdateControllerMethod:
                    this.Items.Add(new ControllerMethodGenerator(this.Context));
                    break;
                case VSFileType.UpdateDataHandler:
                    this.Items.Add(new DataHandlerGenerator(this.Context));
                    break;
                case VSFileType.UpdateFormView:
                    this.Items.Add(new MvcFormViewGenerator(this.Context));
                    break;
                case VSFileType.UpdateHandler:
                    this.Items.Add(new HandlerGenerator(this.Context));
                    break;
                case VSFileType.UpdateMvcModels:
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

        public IFVsFile GetVsFile()
        {
            return this.Files.SingleOrDefault(f => f.FileType == this.FileType);
        }











    }
}
