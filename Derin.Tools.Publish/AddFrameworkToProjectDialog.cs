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
    public partial class AddFrameworkToProjectDialog : Form
    {

        EditProjectModuleDialog projectDialog;
        Project project;

        public AddFrameworkToProjectDialog(EditProjectModuleDialog projectDialog,Project project)
        {
            InitializeComponent();
            this.projectDialog = projectDialog;
            this.project = project;

            JsonDataContext jsonDataContext = new JsonDataContext(new NewtonsoftJsonSerializer());

            var projects = jsonDataContext.GetList<Project>(Helper.GetJsonPath());

            if (projects != null)
            {
                List<Project> datasource = projects.Where(p => p.IsFrameworkProject).ToList();

                if (project.ParentProjects!=null)
                {
                    foreach (var parent in project.ParentProjects)
                    {
                        if (datasource.Any(p => p.UniqueId == parent.UniqueId))
                            datasource = datasource.Where(d => d.UniqueId != parent.UniqueId).ToList();


                    }
                }



                comboBox1.DataSource = datasource;
                comboBox1.DisplayMember = "Name";
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            Project parentProject = comboBox1.SelectedItem as Project;


            this.checkedListBox1.Items.Clear();

            foreach (var item in parentProject.FrameworkModules)
            {
                this.checkedListBox1.Items.Add(item.ModuleName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Project parentProject = comboBox1.SelectedItem as Project;

            foreach (var item in parentProject.FrameworkModules)
            {
                if (checkedListBox1.CheckedItems.Contains(item.ModuleName))
                {
                    item.UseIn = true;
                }
            }

            

            if (this.project.ParentProjects == null)
            {
                this.project.ParentProjects = new List<Project>();
            }

            this.project.ParentProjects.Add(parentProject);

            this.projectDialog.listBoxParents.DataSource = this.project.ParentProjects;

            JsonDataContext jsonDataContext = new JsonDataContext(new NewtonsoftJsonSerializer());

            jsonDataContext.Update(project, Helper.GetJsonPath());

            

        }
    }
}
