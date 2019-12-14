using IF.CodeGeneration.Application.Generator.Add.Items;
using IF.CodeGeneration.Application.Generator.Items;
using IF.CodeGeneration.Application.Generator.Items.Repository;
using IF.CodeGeneration.Application.Generator.Update.Items;
using System.Collections.Generic;

namespace IF.CodeGeneration.Application.Generator
{
    public class ApiCsAddGeneratorEngine : CSApplicationCodeGeneratorEngineBase
    {
        public ApiCsAddGeneratorEngine(ApplicationCodeGeneratorContext context) : base(context)
        {
            this.UpdateContext();
        }

        public override void UpdateContext()
        {
            this.Items.Clear();


            this.Context.Files.Clear();
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Contract", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.AddContractClass, Path = "Commands" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Api", FileExtension = "cs", FileName = this.Context.ControllerName + "Controller", FileType = VSFileType.ApiAddControllerMethod, Path = "Controllers" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Persistence.EF", FileExtension = "cs", FileName = $"{this.Context.RepositoryName}Repository", FileType = VSFileType.AddRepositoryClass, Path = "Repositories" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Cqrs", FileExtension = "cs", FileName = $"{this.Context.className}CommandHandler", FileType = VSFileType.CommandHandler, Path = "Commands" });
            
        }

        public override void SetItemActive(VSFileType type)
        {


            switch (type)
            {
                case VSFileType.AddContractClass:
                    
                    this.Items.Add(new AddContractClassGenerator(this.Context));
                    break;
                case VSFileType.ApiAddControllerMethod:
                    this.Items.Add(new ApiAddControllerMethodGenerator(this.Context));
                    break;
                case VSFileType.AddRepositoryClass:
                    this.Items.Add(new AddRepositoryGenerator(this.Context));
                    break;
                
                case VSFileType.CommandHandler:
                    this.Items.Add(new CommandHandlerGenerator(this.Context));
                    break;
                
                default:
                    break;
            }

        }

        

      











    }
}
