using IF.CodeGeneration.Application.Generator.Update.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Update
{
    public class CSUpdateGenerator : CSGeneratorBase
    {

        public List<UpdateVsFile> Files { get; set; }
        public List<IGenerateItem> Items { get; set; }

        protected UpdateFileType FileType;

        public CSUpdateGenerator(GeneratorContext context) : base(context)
        {
            this.Files = new List<UpdateVsFile>();
            this.Items = new List<IGenerateItem>();
            this.UpdateContext();
        }




        public void UpdateContext()
        {
            this.Files.Clear();
            this.Items.Clear();
            this.Files.Add(new UpdateVsFile() { ProjectName = "Contract", FileExtension = "cs", FileName = this.Context.className, FileType = UpdateFileType.ContractClass, Path = "Commands" });
            this.Files.Add(new UpdateVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = "Security", FileType = UpdateFileType.ControllerMethod, Path = "Controllers" });
            this.Files.Add(new UpdateVsFile() { ProjectName = "Persistence.EF", FileExtension = "cs", FileName = this.Context.className, FileType = UpdateFileType.DataHandler, Path = "Commands" });
            this.Files.Add(new UpdateVsFile() { ProjectName = "Cqrs", FileExtension = "cs", FileName = this.Context.className, FileType = UpdateFileType.Handler, Path = "Commands" });
            this.Files.Add(new UpdateVsFile() { ProjectName = "Admin.UI", FileExtension = "cshtml", FileName = "_Form", FileType = UpdateFileType.FormView, Path = $@"{this.Context.ViewBasePath}" });
            this.Files.Add(new UpdateVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = this.Context.className + "Model", FileType = UpdateFileType.MvcModels, Path = "Models" });
        }

        public void SetItemActive(UpdateFileType type)
        {


            switch (type)
            {
                case UpdateFileType.ContractClass:
                    this.Items.Add(new ContractClassGenerator(this.Context));
                    break;
                case UpdateFileType.ControllerMethod:
                    this.Items.Add(new ControllerMethodGenerator(this.Context));
                    break;
                case UpdateFileType.DataHandler:
                    this.Items.Add(new DataHandlerGenerator(this.Context));
                    break;
                case UpdateFileType.FormView:
                    this.Items.Add(new MvcFormViewGenerator(this.Context));
                    break;
                case UpdateFileType.Handler:
                    this.Items.Add(new HandlerGenerator(this.Context));
                    break;
                case UpdateFileType.MvcModels:
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

        public UpdateVsFile GetVsFile()
        {
            return this.Files.SingleOrDefault(f => f.FileType == this.FileType);
        }











    }
}
