using IF.CodeGeneration.Application.Generator.Items;
using IF.CodeGeneration.Application.Generator.Items.Repository;
using IF.CodeGeneration.Application.Generator.Update.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator
{

    public class ApiCsUpdateGenerator : CSApplicationCodeGeneratorEngineBase
    {
        public ApiCsUpdateGenerator(ApplicationCodeGeneratorContext context) : base(context)
        {
            
            this.Items = new List<ApplicationCodeGenerateItem>();
            this.UpdateContext();
        }




        public override void UpdateContext()
        {
            this.Context.Files.Clear();
            this.Items.Clear();
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Contract", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.UpdateContractClass, Path = "Commands" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Api", FileExtension = "cs", FileName = this.Context.ControllerName +"Controller", FileType = VSFileType.ApiUpdateControllerMethod, Path = "Controllers" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Persistence.EF", FileExtension = "cs", FileName = $"{this.Context.RepositoryName}Repository", FileType = VSFileType.ApiUpdateRepositoryClass, Path = "Repositories" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Cqrs", FileExtension = "cs", FileName = $"{this.Context.className}CommandHandler", FileType = VSFileType.CommandHandler, Path = "Commands" });
            
        }

        public override void SetItemActive(VSFileType type)
        {


            switch (type)
            {
                case VSFileType.UpdateContractClass:
                    this.Items.Add(new UpdateContractClassGenerator(this.Context));
                    break;
                case VSFileType.ApiUpdateControllerMethod:
                    this.Items.Add(new ApiUpdateControllerMethodGenerator(this.Context));
                    break;
                case VSFileType.ApiUpdateRepositoryClass:
                    this.Items.Add(new ApiUpdateRepositoryGenerator(this.Context));
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
