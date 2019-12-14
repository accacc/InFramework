using IF.CodeGeneration.Application.Generator.Get.Items;
using IF.CodeGeneration.Application.Generator.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator
{


    public class ApiCsGetGenerator : CSApplicationCodeGeneratorEngineBase
    {


        public ApiCsGetGenerator(ApplicationCodeGeneratorContext context) : base(context)
        {

            this.UpdateContext();
        }




        public override void UpdateContext()
        {
            this.Items.Clear();

            this.Context.Files.Clear();            
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Contract", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.GetContractClass, Path = "Queries" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Api", FileExtension = "cs", FileName = this.Context.ControllerName + "Controller", FileType = VSFileType.ApiGetControllerMethod, Path = "Controllers" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Cqrs", FileExtension = "cs", FileName = this.Context.className + "QueryHandler", FileType = VSFileType.GetHandler, Path = "Queries" });
            this.Context.Files.Add(new IFVsFile() { ProjectName = "Persistence.EF", FileExtension = "cs", FileName = $"{this.Context.RepositoryName}Repository", FileType = VSFileType.GetRepositorytMethods, Path = "Repositories" });

        }

        public override void SetItemActive(VSFileType type)
        {


            switch (type)
            {
                case VSFileType.GetContractClass:
                    this.Items.Add(new GetContractClassGenerator(this.Context));
                    break;
                case VSFileType.ApiGetControllerMethod:
                    this.Items.Add(new ApiGetControllerMethodGenerator(this.Context));
                    break;                
                case VSFileType.GetHandler:
                    this.Items.Add(new GetHandlerGenerator(this.Context));
                    break;

                case VSFileType.GetRepositorytMethods:
                    this.Items.Add(new GetRepositoryMethodsGenerator(this.Context));
                    break;

                    


                default:
                    break;
            }

        }
    }
}
