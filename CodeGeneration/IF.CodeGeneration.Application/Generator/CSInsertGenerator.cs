using IF.CodeGeneration.Application.Generator.Add.Items;
using IF.CodeGeneration.Application.Generator.Update.Items;
using System.Collections.Generic;

namespace IF.CodeGeneration.Application.Generator
{
    public class CSInsertGenerator: CSApplicationCodeGeneratorEngineBase
    {
        public CSInsertGenerator(ApplicationCodeGeneratorContext context) : base(context)
        {
            
            this.UpdateContext();
        }

        public override void UpdateContext()
        {
            this.Context.Files.Clear();
            this.Items.Clear();
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Contract", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.AddContractClass, Path = "Commands" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = "Security", FileType = VSFileType.AddControllerMethod, Path = "Controllers" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Persistence.EF", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.AddDataHandler, Path = "Commands" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Cqrs", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.CommandHandler, Path = "Commands" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cshtml", FileName = "_Form", FileType = VSFileType.AddFormView, Path = $@"{this.Context.RepositoryName}" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = this.Context.className + "Model", FileType = VSFileType.AddMvcModels, Path = "Models" });
        }

        public override void SetItemActive(VSFileType type)
        {


            switch (type)
            {
                case VSFileType.AddContractClass:
                    
                    this.Items.Add(new AddContractClassGenerator(this.Context));
                    break;
                case VSFileType.AddControllerMethod:
                    this.Items.Add(new AddControllerMethodGenerator(this.Context));
                    break;
                case VSFileType.AddDataHandler:
                    this.Items.Add(new AddDataHandlerGenerator(this.Context));
                    break;
                case VSFileType.AddFormView:
                    this.Items.Add(new AddMvcFormViewGenerator(this.Context));
                    break;
                case VSFileType.CommandHandler:
                    this.Items.Add(new CommandHandlerGenerator(this.Context));
                    break;
                case VSFileType.AddMvcModels:
                    this.Items.Add(new AddMvcModelGenerator(this.Context));
                    break;
                default:
                    break;
            }

        }
    }
}
