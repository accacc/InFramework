using IF.Core.Json;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace IF.Tools.Publish
{
    public partial class EditProjectModuleDialog : ProjectDialog
    {
        Project project;
        Publisher publisher;

        public EditProjectModuleDialog(Publisher publisher,Project project)
        {
            InitializeComponent();

            comboBoxProjectType.DataSource = Enum.GetValues(typeof(ProjectType));

            this.project = project;
            this.publisher = publisher;

            textBoxProjectName.Text = project.Name;
            comboBoxProjectType.SelectedText = project.ProjectType.ToString();
            textBoxPublishDirectory.Text = project.Path;
            textBoxProjectPath.Text = project.ProjectPath;
            checkBoxFrameworkProject.Checked = project.IsFrameworkProject;

            this.panelFrameworkModules.Visible = project.IsFrameworkProject;


            foreach (var item in ProjectModule.GetModules())
            {

                bool IsChecked = this.project.Modules.Any(m => m.ModuleName == item.ModuleName);

                checkedListBoxModules.Items.Add(item.ModuleName,IsChecked);
            }


            foreach (var item in project.FrameworkModules)
            {
               checkedListBoxFrameworkModule.Items.Add(item.ModuleName);
            }

            listBoxParents.DataSource = project.ParentProjects;

            listBoxParents.DisplayMember = "Name";

            
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

            project.Modules.Clear();

            foreach (var item in checkedListBoxModules.CheckedItems)
            {
                project.Modules.Add(allModules.Single(m => m.ModuleName == item.ToString()));
            }

            JsonDataContext jsonDataContext = new JsonDataContext(new NewtonsoftJsonSerializer());

            //NewtonsoftJsonSerializer jsonConverter = new NewtonsoftJsonSerializer();
            //var projectJson = jsonConverter.Serialize(project);

            //File.AppendAllText(,projectJson);

            jsonDataContext.Update(project, Helper.GetJsonPath());

            //AppDomain.CurrentDomain.BaseDirectory

            this.Close();
            this.publisher.Show();

        }

        private void buttonBaseDirectorySelect_Click(object sender, EventArgs e)
        {
            if (directoryBrowserDialogPublishPath.ShowDialog() == DialogResult.OK)
            {
                textBoxPublishDirectory.Text = directoryBrowserDialogPublishPath.SelectedPath;
            }
        }

        private void checkBoxFrameworkProject_CheckedChanged(object sender, EventArgs e)
        {
            this.panelFrameworkModules.Visible = checkBoxFrameworkProject.Checked;
        }

        private void buttonAddFrameworkModule_Click(object sender, EventArgs e)
        {
            AddProjectDialog addFrameworkModuleDialog = new AddProjectDialog(this, this.checkedListBoxFrameworkModule, project);
            addFrameworkModuleDialog.ShowDialog();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var moduleName = checkedListBoxFrameworkModule.SelectedItem.ToString();

            project.FrameworkModules.Remove(project.FrameworkModules.Where(m => m.ModuleName == moduleName).First());

        }

        private void buttonAddParentProject_Click(object sender, EventArgs e)
        {               

                AddFrameworkToProjectDialog dialog = new AddFrameworkToProjectDialog(this, project);

                dialog.ShowDialog();
        }

        private void buttonEditParentProject_Click(object sender, EventArgs e)
        {
            Project parentproject = listBoxParents.SelectedItem as Project;
            EditFrameworkToProjectDialog dialog = new EditFrameworkToProjectDialog(this, project,parentproject);
            
            dialog.ShowDialog();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            foreach (var item in listBoxParents.SelectedItems)
            {
                project.ParentProjects.Remove(item as Project);
            }
        }

        private void buttonProjectPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogProjectPath.ShowDialog() == DialogResult.OK)
            {
                textBoxProjectPath.Text = folderBrowserDialogProjectPath.SelectedPath;
            }
        }

        private void buttonFrameworkModuleEdit_Click(object sender, EventArgs e)
        {
            EditProjectDialog addFrameworkModuleDialog = new EditProjectDialog(this, this.checkedListBoxFrameworkModule, project);
            addFrameworkModuleDialog.ShowDialog();
        }
    }
}

