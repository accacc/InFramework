﻿using IF.CodeGeneration.Application.Generator;
using IF.CodeGeneration.Application.Generator.List;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace IF.Tools.CodeGenerator
{
    public partial class ListGeneratorForm : Form
    {

        public CSListGenerator generator { get; set; }

        public ListGeneratorForm(CSListGenerator generator)
        {
            InitializeComponent();

            generator.UpdateContext();

            textBoxViewBasePath.Text = @"Views\Security\User";
            textBoxControllerName.Text = "SecurityController";

            for (int i = 0; i < checkedListBoxVsFiles.Items.Count; i++)
            {
                checkedListBoxVsFiles.SetItemChecked(i, true);
            }

            foreach (ListItem item in checkedListBoxVsFiles.Items)
            {
                item.Selected = true;
            }

            this.generator = generator;

            this.checkedListBoxVsFiles.Items.Clear();

            foreach (var item in generator.Files)
            {
                this.checkedListBoxVsFiles.Items.Add(item.FileType);
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
