using IF.CodeGeneration.Application.Generator.Get.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Get
{
    public class CSGetGenerator : CSGeneratorBase
    {

        public List<GetVsFile> Files { get; set; }
        public List<IGenerateItem> Items { get; set; }

        protected GetFileType FileType;

        public CSGetGenerator(GeneratorContext context) : base(context)
        {
            this.Files = new List<GetVsFile>();
            this.Items = new List<IGenerateItem>();
            this.UpdateContext();
        }




        public void UpdateContext()
        {
            this.Files.Clear();
            this.Items.Clear();
            this.Files.Add(new GetVsFile() { ProjectName = "Contract", FileExtension = "cs", FileName = this.Context.className, FileType = GetFileType.ContractClass, Path = "Queries" });
            this.Files.Add(new GetVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = "Security", FileType = GetFileType.ControllerMethod, Path = "Controllers" });
            this.Files.Add(new GetVsFile() { ProjectName = "Persistence.EF", FileExtension = "cs", FileName = this.Context.className, FileType = GetFileType.DataHandler, Path = "Queries" });
            this.Files.Add(new GetVsFile() { ProjectName = "Cqrs", FileExtension = "cs", FileName = this.Context.className +"Handler", FileType = GetFileType.Handler, Path = "Queries" });
            this.Files.Add(new GetVsFile() { ProjectName = "Admin.UI", FileExtension = "cshtml", FileName = "_FormView", FileType = GetFileType.FormView, Path = $@"{this.Context.ViewBasePath}" });
            this.Files.Add(new GetVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = this.Context.className + "Model", FileType = GetFileType.MvcModels, Path = "Models" });
        }

        public void SetItemActive(GetFileType type)
        {


            switch (type)
            {
                case GetFileType.ContractClass:
                    this.Items.Add(new ContractClassGenerator(this.Context));
                    break;
                case GetFileType.ControllerMethod:
                    this.Items.Add(new ControllerMethodGenerator(this.Context));
                    break;
                case GetFileType.DataHandler:
                    this.Items.Add(new DataHandlerGenerator(this.Context));
                    break;
                case GetFileType.FormView:
                    this.Items.Add(new MvcFormViewGenerator(this.Context));
                    break;
                case GetFileType.Handler:
                    this.Items.Add(new HandlerGenerator(this.Context));
                    break;
                case GetFileType.MvcModels:
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

        public GetVsFile GetVsFile()
        {
            return this.Files.SingleOrDefault(f => f.FileType == this.FileType);
        }











    }
}
