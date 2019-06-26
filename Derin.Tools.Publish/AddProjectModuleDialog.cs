using IF.Core.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Derin.Tools.Publish
{
    public partial class AddProjectModuleDialog : ProjectDialog
    {
        Project project;
        Publisher2 publisher;

        public AddProjectModuleDialog(Publisher2 publisher)
        {
            InitializeComponent();

            comboBoxProjectType.DataSource = Enum.GetValues(typeof(ProjectType));

            foreach (var item in ProjectModule.GetModules())
            {
                checkedListBoxModules.Items.Add(item.ModuleName);
            }

            this.panelFrameworkModules.Visible = false;

            project = new Project();
            this.publisher = publisher;

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
             


            Enum.TryParse(comboBoxProjectType.Text, out ProjectType projectTypeEnum);


            project.Name = textBoxProjectName.Text;
            project.ProjectType = projectTypeEnum;
            project.Path = textBoxPublishDirectory.Text;
            project.IsFrameworkProject = checkBoxFrameworkProject.Checked;
            project.ProjectPath = textBoxProjectPath.Text;


            var allModules = ProjectModule.GetModules();


            foreach (var item in checkedListBoxModules.CheckedItems)
            {
                project.Modules.Add(allModules.Single(m => m.ModuleName == item.ToString()));
            }

            JsonDataContext jsonDataContext = new JsonDataContext(new NewtonsoftJsonSerializer());

            //NewtonsoftJsonSerializer jsonConverter = new NewtonsoftJsonSerializer();
            //var projectJson = jsonConverter.Serialize(project);

            //File.AppendAllText(,projectJson);

            jsonDataContext.Add(project, Helper.GetJsonPath());

            //AppDomain.CurrentDomain.BaseDirectory

            this.Close();
            this.publisher.Show();

        }

        private void buttonBaseDirectorySelect_Click(object sender, EventArgs e)
        {
            if (browserDialogPublishDirectory.ShowDialog() == DialogResult.OK)
            {
                textBoxPublishDirectory.Text = browserDialogPublishDirectory.SelectedPath;
            }
        }

        private void checkBoxFrameworkProject_CheckedChanged(object sender, EventArgs e)
        {
            this.panelFrameworkModules.Visible = checkBoxFrameworkProject.Checked;
        }

        private void buttonAddFrameworkModule_Click(object sender, EventArgs e)
        {
            AddProjectDialog addFrameworkModuleDialog = new AddProjectDialog(this,this.checkedListBoxFrameworkModule, project);
            addFrameworkModuleDialog.ShowDialog();
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogProjectPath.ShowDialog() == DialogResult.OK)
            {
                textBoxProjectPath.Text = folderBrowserDialogProjectPath.SelectedPath;
            }
        }
    }
}

