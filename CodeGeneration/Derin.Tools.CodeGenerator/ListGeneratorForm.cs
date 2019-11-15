using IF.CodeGeneration.Application.Generator;
using IF.CodeGeneration.Application.Generator.List;
using System;
using System.Linq;
using System.Windows.Forms;

namespace IF.Tools.CodeGenerator
{
    public partial class ListGeneratorForm : Form
    {

        public CSListGenerator generator { get; set; }

        public ListGeneratorForm(CSListGenerator generator)
        {
            InitializeComponent();

            this.generator = generator;

            this.checkedListBoxVsFiles.Items.Clear();

            foreach (var item in generator.Files)
            {
                this.checkedListBoxVsFiles.Items.Add(item.FileType);
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            foreach (var item in checkedListBoxVsFiles.CheckedItems)
            {
                var vsFile = generator.Files.SingleOrDefault(f => f.FileType == (ListFileType)item);
                if (vsFile != null)
                {
                    this.generator.SetItemActive(vsFile.FileType);
                }
            }

            generator.Generate();

            //this.generator.GenerateContractClasses();
            //this.generator.GenerateDataQueryHandlerClass();
            //this.generator.GenerateHandlerClass();
            //this.generator.GenerateControllerMethods();
            //this.generator.GenerateMvcModels();
            //this.generator.GenerateMvcIndexView();
            //this.generator.GenerateMvcGridView();
        }
    }
}
