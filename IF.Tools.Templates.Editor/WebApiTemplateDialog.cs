using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Build.Construction;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace IF.Tools.Templates.Editor
{
    public partial class WebApiTemplateDialog : Form
    {
        string templateCode;
        IFProjectTemplate template;
        string templateSolutionName = "IF.Template";
        public WebApiTemplateDialog()
        {
            InitializeComponent();
            this.templateCode = "WA";
            BindComboBox();
            //var _solutionFile = SolutionFile.Parse(@"C:\Projects\InFramework\IF.Templates.sln");          
        }


        private void BindComboBox()
        {
            BindDatabases();
            BindOrms();
            BindServiceBuses();
            BindMessageBrokers();
            BindLogTypes();
            BindProjects();

        }

        private void BindProjects()
        {

            List<NameValueDto> projectTemplates = new List<NameValueDto>();



            using (var ctx = new MyDbContext())
            {
                template = ctx.ProjectTemplates.Include(p=>p.ProjectList).SingleOrDefault(p => p.Code == templateCode);
         


                clbFolders.Items.Clear();

                foreach (var project in template.ProjectList)
                {
                    clbFolders.Items.Add(project.Name,true);
                }
            }


          
        }

        private void BindServiceBuses()
        {
            List<NameValueDto> items = new List<NameValueDto>();

            items.Add(new NameValueDto { Name = "Native", Value = "Native" });

            bindingSourceServiceBus.DataSource = items;
            comboBoxServiceBus.DisplayMember = "Name";
            comboBoxServiceBus.ValueMember = "Value";
            comboBoxServiceBus.DataSource = bindingSourceServiceBus;
        }


        private void BindMessageBrokers()
        {
            List<NameValueDto> items = new List<NameValueDto>();

            items.Add(new NameValueDto { Name = "Rabbit MQ", Value = "RabbitMq" });

            bindingSourceMessageBroker.DataSource = items;
            comboBoxMessageBroker.DisplayMember = "Name";
            comboBoxMessageBroker.ValueMember = "Value";
            comboBoxMessageBroker.DataSource = bindingSourceMessageBroker;
        }


        private void BindLogTypes()
        {
            List<NameValueDto> items = new List<NameValueDto>();

            //items.Add(new NameValueDto { Name = "EF", Value = "EF" });
            items.Add(new NameValueDto { Name = "Mongo", Value = "Mongo" });

            bindingSourceLogType.DataSource = items;
            comboBoxLogType.DisplayMember = "Name";
            comboBoxLogType.ValueMember = "Value";
            comboBoxLogType.DataSource = bindingSourceLogType;
        }

        private void BindDatabases()
        {
            List<NameValueDto> items = new List<NameValueDto>();

            items.Add(new NameValueDto { Name = "Sql Server", Value = "SqlServer" });

            bindingSourceDatabase.DataSource = items;
            comboBoxDatabase.DisplayMember = "Name";
            comboBoxDatabase.ValueMember = "Value";
            comboBoxDatabase.DataSource = bindingSourceDatabase;
        }

        private void BindOrms()
        {
            List<NameValueDto> items = new List<NameValueDto>();

            items.Add(new NameValueDto { Name = "EF Core", Value = "EFCore" });

            bindingSourceOrm.DataSource = items;
            comboBoxOrm.DisplayMember = "Name";
            comboBoxOrm.ValueMember = "Value";
            comboBoxOrm.DataSource = bindingSourceOrm;
        }

        private void labelTemplateName_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(textBoxSolutionName.Text))
            {
                MessageBox.Show(@"Lütfen yeni solution adını giriniz.", @"Zorunlu Alan", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            string newSolutionName = textBoxSolutionName.Text.Trim();

            if (Directory.Exists(@"C:\temp\templateproject"))
            {
                Directory.Delete(@"C:\temp\templateproject", true);
            }

            //foreach (string dirPath in Directory.GetDirectories(@"C:\Projects\InFramework\IF.Templates", "*", SearchOption.AllDirectories))
            //{
            //    if (template.ProjectList.Any(p => dirPath.Contains(p.Name)) || dirPath.Contains("package"))
            //    {
            //        Directory.CreateDirectory(dirPath.Replace(@"C:\Projects\InFramework\IF.Templates", @"C:\temp\templateproject\IF.Templates"));
            //    }
            //}

            var source = new DirectoryInfo(@"C:\Projects\InFramework\"+ templateSolutionName + "s");
            var target = new DirectoryInfo(@"C:\temp\templateproject\" + newSolutionName);

            CopyFilesRecursively(source, target,newSolutionName);

            File.Copy(@"C:\Projects\InFramework\" + template.SolutionName + ".sln", @"C:\temp\templateproject\" + template.SolutionName.Replace(templateSolutionName + "s", newSolutionName) + ".sln", true);



            string text = File.ReadAllText(@"C:\temp\templateproject\" + template.SolutionName.Replace(templateSolutionName+ "s", newSolutionName) + ".sln");
            text = text.Replace(templateSolutionName + "s", templateSolutionName);
            text = text.Replace(templateSolutionName, newSolutionName);
            File.WriteAllText(@"C:\temp\templateproject\" + template.SolutionName.Replace(templateSolutionName + "s", newSolutionName) + ".sln", text);


            ExploreFile(@"C:\temp\templateproject\" + template.SolutionName.Replace(templateSolutionName + "s", newSolutionName) + ".sln");

        }


        public bool ExploreFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                return false;
            }
            //Clean up file path so it can be navigated OK
            filePath = System.IO.Path.GetFullPath(filePath);
            System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
            return true;
        }

        public  void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target,string newSolutionName)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                //if (template.ProjectList.Any(p => dir.Name.Contains(p.Name)) || dir.Name.Contains("package"))
                {
                    CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name.Replace(templateSolutionName , newSolutionName)), newSolutionName);
                }
            }

            foreach (FileInfo file in source.GetFiles())
            {              
              

                if (file.Extension == ".dll")
                {
                    var newFileName = file.Name.Replace(templateSolutionName , newSolutionName);
                    file.CopyTo(Path.Combine(target.FullName, newFileName));                    
                }
                else if (file.Name == "IFTemplateSettings.cs" && file.Directory.Name == "IF.Template.Domain")
                {
                    HandleSettings(target,file, newSolutionName);
                }
                else if (file.Name == "Startup.cs" && file.Directory.Name == "IF.Template.Api")
                {
                    HandleStartup(target, file, newSolutionName);
                }
                else
                {
                    var newFileName = file.Name.Replace(templateSolutionName , newSolutionName);
                    file.CopyTo(Path.Combine(target.FullName, newFileName));
                    string text = File.ReadAllText(Path.Combine(target.FullName, newFileName));
                    text = text.Replace(templateSolutionName, newSolutionName);
                    File.WriteAllText(Path.Combine(target.FullName, newFileName), text);
                }
            }
        
        }

        private void HandleSettings(DirectoryInfo target,FileInfo file, string newSolutionName)
        {
            var newFileName = file.Name.Replace("IFTemplate", newSolutionName.Replace(".",""));
            file.CopyTo(Path.Combine(target.FullName, newFileName));
            string text = File.ReadAllText(Path.Combine(target.FullName, newFileName));
            text = text.Replace("IFTemplate", newSolutionName.Replace(".", ""));
            text = text.Replace(templateSolutionName, newSolutionName);
            File.WriteAllText(Path.Combine(target.FullName, newFileName), text);
        }

        private void HandleStartup(DirectoryInfo target, FileInfo file, string newSolutionName)
        {
            var newFileName = file.Name.Replace("IFTemplate", newSolutionName.Replace(".", ""));
            file.CopyTo(Path.Combine(target.FullName, newFileName));
            string text = File.ReadAllText(Path.Combine(target.FullName, newFileName));
            text = text.Replace("IFTemplate", newSolutionName.Replace(".", ""));
            text = text.Replace(templateSolutionName, newSolutionName);
            File.WriteAllText(Path.Combine(target.FullName, newFileName), text);
        }
    }
}
