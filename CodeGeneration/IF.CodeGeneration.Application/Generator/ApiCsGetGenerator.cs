using IF.CodeGeneration.Application.Generator.Get.Items;
using IF.CodeGeneration.Application.Generator.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator
{


    public class ApiCsGetGenerator : CSGeneratorBase
    {


        public ApiCsGetGenerator(GeneratorContext context) : base(context)
        {

            this.UpdateContext();
        }




        public  void UpdateContext()
        {
            this.Files.Clear();
            this.Items.Clear();
            this.Files.Add(new IFVsFile() { ProjectName = "Contract", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.GetContractClass, Path = "Queries" });
            this.Files.Add(new IFVsFile() { ProjectName = "Api", FileExtension = "cs", FileName = this.Context.ControllerName + "Controller", FileType = VSFileType.ApiGetControllerMethod, Path = "Controllers" });
            this.Files.Add(new IFVsFile() { ProjectName = "Cqrs", FileExtension = "cs", FileName = this.Context.className + "Handler", FileType = VSFileType.GetHandler, Path = "Queries" });
            
        }

        public void SetItemActive(VSFileType type)
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
                
                default:
                    break;
            }

        }
    }
}
