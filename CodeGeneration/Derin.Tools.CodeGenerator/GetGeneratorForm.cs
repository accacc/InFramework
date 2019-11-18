using IF.CodeGeneration.Application.Generator;
using IF.CodeGeneration.Application.Generator.Get;
using IF.CodeGeneration.Application.Generator.List;
using IF.CodeGeneration.Application.Generator.Update;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace IF.Tools.CodeGenerator
{
    public partial class GetGeneratorForm : Form
    {

        public CSGetGenerator generator { get; set; }

        public GetGeneratorForm(CSGetGenerator generator)
        {
            InitializeComponent();

            generator.UpdateContext();

            textBoxViewBasePath.Text = @"Views/Security/User";
            textBoxControllerName.Text = "SecurityController";          

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

            if (String.IsNullOrWhiteSpace(textBoxViewBasePath.Text))
            {
                MessageBox.Show(@"Please enter the View Base Path.", @"Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }


            if (String.IsNullOrWhiteSpace(textBoxControllerName.Text))
            {
                MessageBox.Show(@"Please enter the ControllerName.", @"Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            generator.Context.ViewBasePath = textBoxViewBasePath.Text;
            generator.Context.ControllerName = textBoxControllerName.Text;
            
            generator.UpdateContext();

            foreach (var item in checkedListBoxVsFiles.CheckedItems)
            {
                var vsFile = generator.Files.SingleOrDefault(f => f.FileType == (GetFileType)item);
                if (vsFile != null)
                {
                    this.generator.SetItemActive(vsFile.FileType);
                }
            }
              
            generator.Generate();
        }
    }
}
