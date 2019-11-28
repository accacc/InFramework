using IF.CodeGeneration.Application.Generator.Update.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.CodeGeneration.Application.Generator.Update
{
    public class CSUpdateGenerator : CSGeneratorBase
    {      
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
            this.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cshtml", FileName = "_Form", FileType = VSFileType.UpdateFormView, Path = $@"{this.Context.RepositoryName}" });
            this.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = this.Context.className + "Model", FileType = VSFileType.UpdateMvcModels, Path = "Models" });
        }

        public void SetItemActive(VSFileType type)
        {


            switch (type)
            {
                case VSFileType.UpdateContractClass:
                    this.Items.Add(new UpdateContractClassGenerator(this.Context));
                    break;
                case VSFileType.UpdateControllerMethod:
                    this.Items.Add(new UpdateControllerMethodGenerator(this.Context));
                    break;
                case VSFileType.UpdateDataHandler:
                    this.Items.Add(new UpdateDataHandlerGenerator(this.Context));
                    break;
                case VSFileType.UpdateFormView:
                    this.Items.Add(new UpdateMvcFormViewGenerator(this.Context));
                    break;
                case VSFileType.UpdateHandler:
                    this.Items.Add(new UpdateHandlerGenerator(this.Context));
                    break;
                case VSFileType.UpdateMvcModels:
                    this.Items.Add(new UpdateMvcModelGenerator(this.Context));
                    break;
                default:
                    break;
            }

        }

        
    }
}
