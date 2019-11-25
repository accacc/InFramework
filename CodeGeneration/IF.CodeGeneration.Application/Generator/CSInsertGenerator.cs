using IF.CodeGeneration.Application.Generator.Add.Items;

using System.Collections.Generic;

namespace IF.CodeGeneration.Application.Generator
{
    public class CSInsertGenerator: CSGeneratorBase
    {
        public CSInsertGenerator(GeneratorContext context) : base(context)
        {
            this.Files = new List<IFVsFile>();
            this.Items = new List<IGenerateItem>();
            this.UpdateContext();
        }

        public  void UpdateContext()
        {
            this.Files.Clear();
            this.Items.Clear();
            this.Files.Add(new IFVsFile() { ProjectName = "Contract", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.AddContractClass, Path = "Commands" });
            this.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = "Security", FileType = VSFileType.AddControllerMethod, Path = "Controllers" });
            this.Files.Add(new IFVsFile() { ProjectName = "Persistence.EF", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.AddDataHandler, Path = "Commands" });
            this.Files.Add(new IFVsFile() { ProjectName = "Cqrs", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.AddHandler, Path = "Commands" });
            this.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cshtml", FileName = "_Form", FileType = VSFileType.AddFormView, Path = $@"{this.Context.ViewBasePath}" });
            this.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = this.Context.className + "Model", FileType = VSFileType.AddMvcModels, Path = "Models" });
        }

        public void SetItemActive(VSFileType type)
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
                case VSFileType.AddHandler:
                    this.Items.Add(new AddHandlerGenerator(this.Context));
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
