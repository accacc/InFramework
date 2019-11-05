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
    //public class ProjectDialog:Form
    //{
    //    private void InitializeComponent()
    //    {
    //        this.SuspendLayout();
    //        // 
    //        // ProjectDialog
    //        // 
    //        this.ClientSize = new System.Drawing.Size(628, 395);
    //        this.Name = "ProjectDialog";
    //        this.ResumeLayout(false);

    //    }
    //}

    public partial class EditProjectDialog : Form
    {

        Project project;
        ProjectDialog addProjectDialog;
        CheckedListBox checkedListBoxFrameworkModule;

        ProjectModule projectModule;
        public EditProjectDialog(ProjectDialog addProjectDialog, CheckedListBox checkedListBoxFrameworkModule, Project project)
        {
            InitializeComponent();

            this.project = project;
            this.addProjectDialog = addProjectDialog;
            this.checkedListBoxFrameworkModule = checkedListBoxFrameworkModule;
            this.projectModule = project.FrameworkModules.Where(m => m.ModuleName == this.checkedListBoxFrameworkModule.Text.ToString()).Single();

            comboBoxModuleType.DataSource = Enum.GetValues(typeof(ProjectType));
            comboBoxModuleType.SelectedItem = projectModule.Type;


            textBoxModuleName.Text = projectModule.ModuleName;
            textBoxDesc.Text = projectModule.Name;

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {



            
            this.projectModule.ModuleName = textBoxModuleName.Text;
            this.projectModule.Name = textBoxDesc.Text;
            ProjectType type = (ProjectType)comboBoxModuleType.SelectedItem;
            this.projectModule.Type = type ;

            //this.project.FrameworkModules.Add(frameworkModule);
            

            JsonDataContext jsonDataContext = new JsonDataContext(new NewtonsoftJsonSerializer());

            jsonDataContext.Update(project, Helper.GetJsonPath());


            this.Close();
            this.addProjectDialog.Show();
        }
    }
}
