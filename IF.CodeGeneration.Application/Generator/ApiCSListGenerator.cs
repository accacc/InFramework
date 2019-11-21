using IF.CodeGeneration.Application.Generator.List.Items;
using System.Collections.Generic;
using System.Linq;

namespace IF.CodeGeneration.Application.Generator.List
{
    public class ApiCSListGenerator : CSGeneratorBase
    {
        

        public ApiCSListGenerator(GeneratorContext context) : base(context)
        {
            this.UpdateContext();
            
        }

        public void UpdateContext()
        {
            this.Files.Clear();

            this.Files.Add(new IFVsFile() { ProjectName = "Contract", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.ListContracts, Path = "Queries" });
            this.Files.Add(new IFVsFile() { ProjectName = "Cqrs", FileExtension = "cs", FileName = this.Context.className, FileType = VSFileType.ListHandler, Path = "Queries" });
            this.Files.Add(new IFVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = this.Context.ControllerName + "Controller", FileType = VSFileType.ApiListControllerMethods, Path = "Controllers" });
            
        }

        public void SetItemActive(VSFileType type)
        {
         

            switch (type)
            {
                
                case VSFileType.ListContracts:
                    this.Items.Add(new ListContractClassGenerator(this.Context));
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
