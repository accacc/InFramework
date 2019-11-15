using IF.CodeGeneration.Application.Generator.List.Items;
using IF.CodeGeneration.Core;

using System;
using System.Collections.Generic;

namespace IF.CodeGeneration.Application.Generator.List
{
    public class CSListGenerator : CSGeneratorBase
    {




        public List<VsFile> Files { get; set; }

        public VsFile File { get; set; }

        public bool IsActive { get; set; }
        public List<IGenerateItem> Items { get; set; }
        public string Title { get; set; }

        public CSListGenerator(FileSystemCodeFormatProvider fileSystem, string className, string nameSpaceName, ClassTree classTree, Type classType) : base(fileSystem, className, nameSpaceName, classTree, classType)
        {
            this.Files = new List<VsFile>();
            this.Items = new List<IGenerateItem>();
            this.Files.Add(new VsFile() { FileExtension = "cshtml", FileName = "_GridView", FileType = ListFileType.Gridview, Path = "" });
            this.Files.Add(new VsFile() { FileExtension = "cs", FileName = "_GridView", FileType = ListFileType.Contracts, Path = "" });
            this.Files.Add(new VsFile() { FileExtension = "cs", FileName = "_GridView", FileType = ListFileType.DataHandler, Path = "" });
            this.Files.Add(new VsFile() { FileExtension = "cs", FileName = "_GridView", FileType = ListFileType.MvcModel, Path = "" });
            this.Files.Add(new VsFile() { FileExtension = "cs", FileName = "_GridView", FileType = ListFileType.Handler, Path = "" });
            this.Files.Add(new VsFile() { FileExtension = "cs", FileName = "_GridView", FileType = ListFileType.MvcMethods, Path = "" });
            this.Files.Add(new VsFile() { FileExtension = "cshtml", FileName = "_GridView", FileType = ListFileType.IndexView, Path = "" });
        }
        public void SetItemActive(ListFileType type)
        {

            switch (type)
            {
                case ListFileType.Gridview:
                    this.Items.Add(new MvcGridViewGenerator(this.fileSystem, this.className, this.nameSpaceName, this.classTree, this.classType));
                    break;
                case ListFileType.Contracts:
                    this.Items.Add(new ContractClassGenerator(this.fileSystem, this.className, this.nameSpaceName, this.classTree, this.classType));
                    break;
                case ListFileType.DataHandler:
                    this.Items.Add(new DataHandlerClass(this.fileSystem, this.className, this.nameSpaceName, this.classTree, this.classType));
                    break;
                case ListFileType.MvcModel:
                    this.Items.Add(new MvcModelGenerator(this.fileSystem, this.className, this.nameSpaceName, this.classTree, this.classType));
                    break;
                case ListFileType.Handler:
                    this.Items.Add(new HandlerClassGenerator(this.fileSystem, this.className, this.nameSpaceName, this.classTree, this.classType));
                    break;
                case ListFileType.MvcMethods:
                    this.Items.Add(new ControllerMethodGenerator(this.fileSystem, this.className, this.nameSpaceName, this.classTree, this.classType));
                    break;
                case ListFileType.IndexView:
                    this.Items.Add(new MvcIndexViewGenerator(this.fileSystem, this.className, this.nameSpaceName, this.classTree, this.classType));
                    break;
                default:
                    break;
            }
            //var item = this.Items.SingleOrDefault(t => t.File.FileType.ToString() == type.ToString());

            //if(item!=null)
            //{
            //    item.IsActive = true;
            //}

        }

        public void Generate()
        {
            foreach (var item in Items)
            {

                item.Execute();

            }

        }



    }
}
