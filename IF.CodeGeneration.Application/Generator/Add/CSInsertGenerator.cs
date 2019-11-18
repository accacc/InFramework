using IF.CodeGeneration.Application.Generator.Add.Items;
using IF.CodeGeneration.Core;
using IF.CodeGeneration.CSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.CodeGeneration.Application.Generator
{
    public class CSInsertGenerator: CSGeneratorBase
    {

        public List<AddVsFile> Files { get; set; }
        public List<IGenerateItem> Items { get; set; }

        protected AddFileType FileType;

        public CSInsertGenerator(GeneratorContext context) : base(context)
        {
            this.Files = new List<AddVsFile>();
            this.Items = new List<IGenerateItem>();
            this.UpdateContext();
        }




        public void UpdateContext()
        {
            this.Files.Clear();
            this.Files.Add(new AddVsFile() { ProjectName = "Contract", FileExtension = "cs", FileName = this.Context.className, FileType = AddFileType.ContractClass, Path = "Commands" });
        }

        public void SetItemActive(AddFileType type)
        {


            switch (type)
            {
                case AddFileType.ContractClass:
                    this.Items.Add(new ContractClassGenerator(this.Context));
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

        public AddVsFile GetVsFile()
        {
            return this.Files.SingleOrDefault(f => f.FileType == this.FileType);
        }











    }
}
