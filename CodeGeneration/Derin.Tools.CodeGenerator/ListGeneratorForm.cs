using IF.Tools.CodeGenerator.Generator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                this.checkedListBoxVsFiles.Items.Add(item.FileType.ToString());
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            foreach (var item in checkedListBoxVsFiles.CheckedItems)
            {
                var vsFile = generator.Files.SingleOrDefault(f => f.FileType.ToString() == item.ToString());
                if (vsFile != null)
                {
                    vsFile.IsActive = true;
                }
            }


            this.generator.GenerateContractClasses();
            this.generator.GenerateDataQueryHandlerClass();
            this.generator.GenerateHandlerClass();
            this.generator.GenerateControllerMethods();
            this.generator.GenerateMvcModels();
            this.generator.GenerateMvcIndexView();
            this.generator.GenerateMvcGridView();
        }
    }
}
