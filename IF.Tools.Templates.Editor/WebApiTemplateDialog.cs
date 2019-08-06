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
        public WebApiTemplateDialog()
        {
            InitializeComponent();
            this.templateCode = "WA";
            BindComboBox();

            var _solutionFile = SolutionFile.Parse(@"C:\Projects\InFramework\IF.Templates.sln");


          




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

            var source = new DirectoryInfo(@"C:\Projects\InFramework\IF.Templates");
            var target = new DirectoryInfo(@"C:\temp\templateproject\IF.Templates");

            CopyFilesRecursively(source, target);

            File.Copy(@"C:\Projects\InFramework\" + template.SolutionName + ".sln", @"C:\temp\templateproject\" + template.SolutionName + ".sln", true);


        }

        public  void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                //if (template.ProjectList.Any(p => dir.Name.Contains(p.Name)) || dir.Name.Contains("package"))
                {
                    CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
                }
            }

            foreach (FileInfo file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name));
            }
        
        }
    }
}
