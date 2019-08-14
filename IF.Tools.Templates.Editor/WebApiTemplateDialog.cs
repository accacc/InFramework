using CWDev.SLNTools.Core;
using IF.Core.Data;
//using Microsoft.Build.Construction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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


            using (var dbContext = new MyDbContext())
            {
                template = dbContext.ProjectTemplates.Include(p => p.ProjectList).ThenInclude(p => p.IFProjectNugetPackages).SingleOrDefault(p => p.Code == templateCode);

                checkBoxListProjects.Items.Clear();

                foreach (var project in template.ProjectList)
                {
                    checkBoxListProjects.Items.Add(project.Name, true);
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



        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxSolutionName.Text))
            {
                MessageBox.Show(@"Please enter the new solution name.", @"Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (String.IsNullOrWhiteSpace(textBoxApiName.Text))
            {
                MessageBox.Show(@"Please enter the new api name.", @"Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }


            if (String.IsNullOrWhiteSpace(textBoxEventBusName.Text))
            {
                MessageBox.Show(@"Please enter the event bus name.", @"Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (String.IsNullOrWhiteSpace(textBoxControllerName.Text))
            {
                MessageBox.Show(@"Please enter the controller name.", @"Required", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string newSolutionName = textBoxSolutionName.Text.Trim();

            if (Directory.Exists(@"C:\temp\templateproject"))
            {
                Directory.Delete(@"C:\temp\templateproject", true);
            }

            var source = new DirectoryInfo(@"C:\Projects\InFramework\" + templateSolutionName);
            var target = new DirectoryInfo(@"C:\temp\templateproject\" + newSolutionName);

            CopyFilesRecursively(source, target, newSolutionName);

            RemoveUnnecessaryDependencies();

            var newSolutionNamePath = @"C:\temp\templateproject\" + template.SolutionName.Replace(templateSolutionName, newSolutionName) + ".sln";

            CreateSolutionFile(newSolutionName, newSolutionNamePath);

            ExploreFile(newSolutionNamePath);

        }

        private void RemoveUnnecessaryDependencies()
        {
            string newSolutionName = textBoxSolutionName.Text.Trim();
            //var project = template.ProjectList.SingleOrDefault(p => p.Name == file.Name.Replace(file.Extension, ""));
            //text = text.Replace(@"<Project Sdk=""" + project.Sdk + @""">", "");
            foreach (var unnecessaryProject in template.ProjectList)
            {
                if (!checkBoxListProjects.CheckedItems.Contains(unnecessaryProject.Name))
                {

                    foreach (string necessaryProject in checkBoxListProjects.CheckedItems)
                    {
                        if (checkBoxListProjects.CheckedItems.Contains(unnecessaryProject.Name)) continue;

                        string projectName = necessaryProject.Replace(templateSolutionName, newSolutionName);

                        Microsoft.Build.Evaluation.Project msProject = new Microsoft.Build.Evaluation.Project(@"C:\temp\templateproject\" + newSolutionName + @"\" + projectName + @"\" + projectName + ".csproj");

                        var removeItem = msProject.GetItems("ProjectReference").SingleOrDefault(r => r.EvaluatedInclude.EndsWith(unnecessaryProject.Name.Replace(templateSolutionName, newSolutionName) + ".csproj"));

                        if (removeItem != null)
                        {
                            msProject.RemoveItem(removeItem);
                            msProject.Save();
                        }
                    }

                    //    Microsoft.Build.Evaluation.Project msProject = new Microsoft.Build.Evaluation.Project(@"C:\temp\templateproject\" + template.SolutionName);
                    //  p.RemoveItem(p.Items.First());
                    //msProject.Save();
                }
            }
        }

        private void CreateSolutionFile(string newSolutionName, string newSolutionNamePath)
        {

            var oldSolutionPath = @"C:\Projects\InFramework\" + template.SolutionName + ".sln";

            File.Copy(oldSolutionPath, newSolutionNamePath, true);




            var newSln = SolutionFile.FromFile(newSolutionNamePath);

            var projectCount = newSln.Projects.Count;

            List<string> deleteProjects = new List<string>();

            for (int i = 0; i < projectCount; i++)
            {
                var project = newSln.Projects[i];

                if (!checkBoxListProjects.CheckedItems.Contains(project.ProjectName))
                {
                    //var IsSuccess = newSln.Projects.Remove(project);
                    deleteProjects.Add(project.ProjectName);
                }
            }


            foreach (var deleteProject in deleteProjects)
            {
                var proj = newSln.Projects.FindByFullName(deleteProject);
                newSln.Projects.Remove(proj);
            }

            newSln.Save();


            string text = File.ReadAllText(@"C:\temp\templateproject\" + template.SolutionName.Replace(templateSolutionName, newSolutionName) + ".sln");
            text = text.Replace(templateSolutionName, templateSolutionName);
            text = text.Replace(templateSolutionName, newSolutionName);
            File.WriteAllText(newSolutionNamePath, text);

            newSln = SolutionFile.FromFile(newSolutionNamePath);

        }

        public bool ExploreFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                return false;
            }

            filePath = System.IO.Path.GetFullPath(filePath);
            System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
            return true;
        }

        public void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target, string newSolutionName)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                if (source.Name == "IF.Template" && dir.Name != "packages" && !checkBoxListProjects.CheckedItems.Contains(dir.Name)) continue;


                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name.Replace(templateSolutionName, newSolutionName)), newSolutionName);
            }

            foreach (FileInfo file in source.GetFiles())
            {
                if (file.Extension == ".dll")
                {
                    var newFileName = file.Name.Replace(templateSolutionName, newSolutionName);
                    file.CopyTo(Path.Combine(target.FullName, newFileName));
                }
                else if (file.Name == "IFTemplateSettings.cs" && file.Directory.Name == "IF.Template.Domain")
                {
                    HandleSettings(target, file, newSolutionName);
                }
                else if (file.Name == "Startup.cs" && file.Directory.Name == "IF.Template.Api")
                {
                    HandleStartup(target, file, newSolutionName);
                }
                else if (file.Name == "TestController.cs" && file.Directory.Name == "IF.Template.Api")
                {
                    HandleController(target, file, newSolutionName);
                }
                else if (file.Extension == ".csproj")
                {
                    HandleProjects(target, file, newSolutionName);
                }
                else
                {
                    var newFileName = file.Name.Replace(templateSolutionName, newSolutionName);
                    file.CopyTo(Path.Combine(target.FullName, newFileName));
                    string text = File.ReadAllText(Path.Combine(target.FullName, newFileName));
                    text = text.Replace(templateSolutionName, newSolutionName);
                    File.WriteAllText(Path.Combine(target.FullName, newFileName), text);
                }
            }

        }

        private void HandleProjects(DirectoryInfo target, FileInfo file, string newSolutionName)
        {

            var newFileName = file.Name.Replace(templateSolutionName, newSolutionName);
            file.CopyTo(Path.Combine(target.FullName, newFileName));
            string text = File.ReadAllText(Path.Combine(target.FullName, newFileName));
            text = text.Replace(templateSolutionName, newSolutionName);
            File.WriteAllText(Path.Combine(target.FullName, newFileName), text);


        }

        private void HandleSettings(DirectoryInfo target, FileInfo file, string newSolutionName)
        {
            var newFileName = file.Name.Replace("IFTemplate", newSolutionName.Replace(".", ""));
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
            text = text.Replace("InFramework Template Api", textBoxApiName.Text.Trim());
            text = text.Replace("if_template", textBoxEventBusName.Text.Trim());
            File.WriteAllText(Path.Combine(target.FullName, newFileName), text);
        }

        private void HandleController(DirectoryInfo target, FileInfo file, string newSolutionName)
        {
            var newFileName = file.Name.Replace("TestController", textBoxControllerName.Text.Trim());
            file.CopyTo(Path.Combine(target.FullName, newFileName));
            string text = File.ReadAllText(Path.Combine(target.FullName, newFileName));
            text = text.Replace("IFTemplate", newSolutionName.Replace(".", ""));
            text = text.Replace(templateSolutionName, newSolutionName);
            text = text.Replace("TestController", textBoxControllerName.Text.Trim() + "Controller");
            File.WriteAllText(Path.Combine(target.FullName, newFileName), text);
        }
    }
}
