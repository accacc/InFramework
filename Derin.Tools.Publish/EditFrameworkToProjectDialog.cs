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

namespace Derin.Tools.Publish
{
    public partial class EditFrameworkToProjectDialog : Form
    {

        EditProjectModuleDialog projectDialog;
        Project project;
        Project projectParent;

        public EditFrameworkToProjectDialog(EditProjectModuleDialog projectDialog,Project project,Project projectParent)
        {
            InitializeComponent();
            this.projectDialog = projectDialog;
            this.project = project;
            this.projectParent = projectParent;

            JsonDataContext jsonDataContext = new JsonDataContext(new NewtonsoftJsonSerializer());

            var projects = jsonDataContext.GetList<Project>(Helper.GetJsonPath());

            if (projects != null)
            {
                List<Project> datasource = projects.Where(p => p.UniqueId == projectParent.UniqueId).ToList();
                comboBox1.SelectedText = projectParent.Name;
                comboBox1.DataSource = datasource;
                comboBox1.DisplayMember = "Name";
            }


            this.checkedListBox1.Items.Clear();

            foreach (var item in this.projectParent.FrameworkModules)
            {
                
                this.checkedListBox1.Items.Add(item.ModuleName,item.UseIn);

            }
            
        }

       

        private void button1_Click(object sender, EventArgs e)
        {

            foreach (var item in projectParent.FrameworkModules)
            {
                if (checkedListBox1.CheckedItems.Contains(item.ModuleName))
                {
                    item.UseIn = true;
                }
                else
                {
                    item.UseIn = false;
                }
            }          

            

            JsonDataContext jsonDataContext = new JsonDataContext(new NewtonsoftJsonSerializer());

            jsonDataContext.Update(project, Helper.GetJsonPath());

            

        }
    }
}
