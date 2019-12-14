using IF.CodeGeneration.Application.Generator.Items;
using IF.CodeGeneration.Application.Generator.List.Items;
using System.Collections.Generic;
using System.Linq;

namespace IF.CodeGeneration.Application.Generator.List
{
    public class ApiCsListGenerator : CSApplicationCodeGeneratorEngineBase
    {
        

        public ApiCsListGenerator(ApplicationCodeGeneratorContext context) : base(context)
        {
            this.UpdateContext();
            
        }

        public override void UpdateContext()
        {
            this.Context.Files.Clear();
            this.Items.Clear();
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Contract", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.ListContracts, Path = "Queries" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Cqrs", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.ListHandler, Path = "Queries" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Api", FileExtension = "cs", FileName = this.Context.ControllerName + "Controller", FileType = VSFileType.ApiListControllerMethods, Path = "Controllers" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Persistence.EF", FileExtension = "cs", FileName = $"{this.Context.RepositoryName}Repository", FileType = VSFileType.ListRepositorytMethods,Path= "Repositories"});

        }

        public override void SetItemActive(VSFileType type)
        {
         

            switch (type)
            {
                
                case VSFileType.ListContracts:
                    this.Items.Add(new ListContractClassGenerator(this.Context));
                    break;
                case VSFileType.ListRepositorytMethods:
                    this.Items.Add(new ListRepositoryMethodsGenerator(this.Context));
                    break;


                case VSFileType.ListHandler:
                    this.Items.Add(new ListHandlerClassGenerator(this.Context));
                    break;
                case VSFileType.ApiListControllerMethods:
                    this.Items.Add(new ApiListControllerMethodGenerator(this.Context));
                    break;
                
                default:
                    break;
            }

        }

       
    }
}
