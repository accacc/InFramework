using IF.CodeGeneration.Application.Generator;
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
    public partial class ApiGeneratorBaseForm : Form
    {

        private CSGeneratorBase generator { get; set; }        

        public T GetGeneratorInstance<T>() where T : CSGeneratorBase
        {

            return this.generator as T;
        }

        public ApiGeneratorBaseForm(CSGeneratorBase generator)
        {
             InitializeComponent();

            generator.UpdateContext();            

            this.generator = generator;

            this.checkedListBoxVsFiles.Items.Clear();

            foreach (var item in generator.Files)
            {
                this.checkedListBoxVsFiles.Items.Add(item.FileType);
            }

            for (int i = 0; i < checkedListBoxVsFiles.Items.Count; i++)
            {
                checkedListBoxVsFiles.SetItemChecked(i, true);
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrWhiteSpace(textBoxRepositoryName.Text))
            {
                MessageBox.Show(@"Please enter the View Repository Name.", @"Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }


            if (String.IsNullOrWhiteSpace(textBoxControllerName.Text))
            {
                MessageBox.Show(@"Please enter the ControllerName.", @"Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            generator.Context.RepositoryName = textBoxRepositoryName.Text;
            generator.Context.ControllerName = textBoxControllerName.Text;

            generator.UpdateContext();

            foreach (var item in checkedListBoxVsFiles.CheckedItems)
            {
                var vsFile = generator.Files.SingleOrDefault(f => f.FileType == (VSFileType)item);
                if (vsFile != null)
                {
                    this.generator.SetItemActive(vsFile.FileType);
                }
            }

            generator.Generate();

        }

        
    }
}
