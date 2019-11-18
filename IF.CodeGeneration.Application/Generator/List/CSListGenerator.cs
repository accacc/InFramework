using IF.CodeGeneration.Application.Generator.List.Items;
using System.Collections.Generic;
using System.Linq;

namespace IF.CodeGeneration.Application.Generator.List
{
    public class CSListGenerator : CSGeneratorBase
    {
        public List<ListVsFile> Files { get; set; }
        public List<IGenerateItem> Items { get; set; }

        protected ListFileType FileType;

        

        public CSListGenerator(GeneratorContext context) : base(context)
        {
            this.Files = new List<ListVsFile>();
            this.Items = new List<IGenerateItem>();
            this.UpdateContext();
            
        }

        public void UpdateContext()
        {
            this.Files.Clear();
            this.Files.Add(new ListVsFile() { ProjectName = "Admin.UI", FileExtension = "cshtml", FileName = "_GridView", FileType = ListFileType.Gridview, Path = $@"{this.Context.ViewBasePath}" });
            this.Files.Add(new ListVsFile() { ProjectName = "Contract", FileExtension = "cs", FileName = this.Context.className, FileType = ListFileType.Contracts, Path = "Commands" });
            this.Files.Add(new ListVsFile() { ProjectName = "Persistence.EF", FileExtension = "cs", FileName = this.Context.className, FileType = ListFileType.DataHandler, Path = "Queries" });
            this.Files.Add(new ListVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = this.Context.className + "GridModel", FileType = ListFileType.MvcModel, Path = "Models" });
            this.Files.Add(new ListVsFile() { ProjectName = "Cqrs", FileExtension = "cs", FileName = this.Context.className, FileType = ListFileType.Handler, Path = "Queries" });
            this.Files.Add(new ListVsFile() { ProjectName = "Admin.UI", FileExtension = "cs", FileName = "Security", FileType = ListFileType.MvcControllerMethods, Path = "Controllers" });
            this.Files.Add(new ListVsFile() { ProjectName = "Admin.UI", FileExtension = "cshtml", FileName = "Index", FileType = ListFileType.IndexView, Path = $@"{this.Context.ViewBasePath}" });
        }

        public void SetItemActive(ListFileType type)
        {
         

            switch (type)
            {
                case ListFileType.Gridview:
                    this.Items.Add(new MvcGridViewGenerator(this.Context));
                    break;
                case ListFileType.Contracts:
                    this.Items.Add(new ContractClassGenerator(this.Context));
                    break;
                case ListFileType.DataHandler:
                    this.Items.Add(new DataHandlerClass(this.Context));
                    break;
                case ListFileType.MvcModel:
                    this.Items.Add(new MvcModelGenerator(this.Context));
                    break;
                case ListFileType.Handler:
                    this.Items.Add(new HandlerClassGenerator(this.Context));
                    break;
                case ListFileType.MvcControllerMethods:
                    this.Items.Add(new ControllerMethodGenerator(this.Context));
                    break;
                case ListFileType.IndexView:
                    this.Items.Add(new MvcIndexViewGenerator(this.Context));
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

        public ListVsFile GetVsFile()
        {
            return this.Files.SingleOrDefault(f => f.FileType == this.FileType);
        }
    }
}
