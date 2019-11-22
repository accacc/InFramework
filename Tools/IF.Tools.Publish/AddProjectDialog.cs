using IF.Core.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IF.Tools.Publish
{
    public class ProjectDialog:Form
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ProjectDialog
            // 
            this.ClientSize = new System.Drawing.Size(762, 364);
            this.Name = "ProjectDialog";
            this.ResumeLayout(false);

        }
    }

    public partial class AddProjectDialog : Form
    {

        Project project;
        ProjectDialog addProjectDialog;
        CheckedListBox checkedListBoxFrameworkModule;
        public AddProjectDialog(ProjectDialog addProjectDialog, CheckedListBox checkedListBoxFrameworkModule, Project project)
        {
            InitializeComponent();

            this.project = project;
            this.addProjectDialog = addProjectDialog;
            this.checkedListBoxFrameworkModule = checkedListBoxFrameworkModule;

            comboBoxModuleType.DataSource = Enum.GetValues(typeof(ProjectType));
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            ProjectModule frameworkModule = new ProjectModule();
            frameworkModule.ModuleName = textBoxModuleName.Text;
            frameworkModule.Name = textBoxDesc.Text;
            this.project.FrameworkModules.Add(frameworkModule);
            this.checkedListBoxFrameworkModule.Items.Add(textBoxModuleName.Text);

            JsonDataContext jsonDataContext = new JsonDataContext(new NewtonsoftJsonSerializer());

            jsonDataContext.Update(project, Helper.GetJsonPath());


            this.Close();
            this.addProjectDialog.Show();
        }
    }
}
