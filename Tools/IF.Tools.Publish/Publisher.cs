﻿using IF.Core.Data;
using IF.Core.Json;
using Microsoft.Build.BuildEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace IF.Tools.Publish
{
    public partial class Publisher : Form
    {

        public string frameworkIndent = "IF";

        public string baseFrameworkPath = @"C:/Projects/InFramework";

        public string fwDllPath = String.Empty;

        public Publisher()
        {
            InitializeComponent();

            BindComboBox();            

            textBoxBaseDirectory.Text = baseFrameworkPath;

            this.fwDllPath =   "/" + frameworkIndent + ".";            

            JsonDataContext jsonDataContext = new JsonDataContext(new NewtonsoftJsonSerializer());

            var projects = jsonDataContext.GetList<Project>(Helper.GetJsonPath());

            if (projects != null)
            {

                foreach (var item in projects)
                {
                    listBoxAllProjects.DataSource = projects;
                    listBoxAllProjects.DisplayMember = "Name";
                    listBoxAllProjects.ValueMember = "UniqueId";
                }
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    string basePublishPath = comboBoxPublishDirectory.SelectedValue.ToString();
        //    string binFolder = comboBoxPublishMode.SelectedValue.ToString();

        //    List<string> dllUniqueNames = new List<string>();

        //    //dllUniqueNames.Add("AutoMapper");
        //    dllUniqueNames.Add("Persistence.EF");
        //    //dllUniqueNames.Add("Persistence");
        //    dllUniqueNames.Add("Web.Mvc");
        //    dllUniqueNames.Add("Web.Mvc.Kendo");
        //    dllUniqueNames.Add("Web.Mvc.FluentHtml");
        //    //dllUniqueNames.Add("Core");
        //    //dllUniqueNames.Add("Log.NLog");
        //    dllUniqueNames.Add("Configuration");
        //    //dllUniqueNames.Add("Json");
        //    //dllUniqueNames.Add("Rest");
        //    dllUniqueNames.Add("JWT");
        //    //dllUniqueNames.Add("Integration");
        //    //dllUniqueNames.Add("DynamicData");
        //    //dllUniqueNames.Add("WebApi");
        //    //dllUniqueNames.Add("EventBus.RabbitMQ");

        //    //.Net Core
        //    dllUniqueNames.Add("IF.Persistence.EF.Core");
        //    dllUniqueNames.Add("IF.Configuration");
        //    dllUniqueNames.Add("IF.Validation.FluentValidation");
        //    dllUniqueNames.Add("IF.EventBus.RabbitMQ.Integration");
        //    dllUniqueNames.Add("IF.EventBus.Azure");
        //    dllUniqueNames.Add("IF.Core");
        //    dllUniqueNames.Add("IF.Jwt");
        //    dllUniqueNames.Add("IF.Json");
        //    dllUniqueNames.Add("IF.DynamicData");
        //    dllUniqueNames.Add("IF.Persistence");
        //    dllUniqueNames.Add("IF.EventBus.RabbitMQ");
        //    dllUniqueNames.Add("IF.AutoMapper");
        //    dllUniqueNames.Add("IF.RazorviewEngine");
        //    dllUniqueNames.Add("IF.Rest.Client");
        //    dllUniqueNames.Add("IF.Elasticsearch");
        //    dllUniqueNames.Add("IF.HealthChecks");
        //    dllUniqueNames.Add("IF.HealthChecks.SqlServer");
        //    dllUniqueNames.Add("IF.HealthChecks.RabbitMQ");
        //    dllUniqueNames.Add("IF.HealthChecks.Elasticsearch");
        //    dllUniqueNames.Add("IF.NetCore.Scheduler");
        //    dllUniqueNames.Add("IF.Redis");
        //    dllUniqueNames.Add("IF.MongoDB");
        //    dllUniqueNames.Add("IF.Dependency.AutoFac");



        //    //basePublishPath.ClearDirectory();

        //    CopyFiles(textBoxBaseDirectory.Text, basePublishPath, binFolder, dllUniqueNames);

        //    MessageBox.Show("Publish done!");
        //}

        //private void CopyFiles(string baseFrameworkPath, string basePublishPath, string binFolder, List<string> dlls)
        //{
            

        //    foreach (var dllUniqueName in dlls)
        //    {
        //        var dllName = fwDllPath + dllUniqueName + ".dll";

        //        var dllPath = baseFrameworkPath + fwDllPath + dllUniqueName + "/bin/" + binFolder + dllName;

        //        if (dllUniqueName.StartsWith("Plugins."))
        //        {
        //            dllPath = baseFrameworkPath + fwDllPath + dllUniqueName + "/bin/" + dllName;
        //        }

        //        if(dllUniqueName.StartsWith("IF.Persistence.EF.Core")
        //          ||  dllUniqueName.StartsWith("IF.NetCore.Scheduler")
        //            )
        //        {
        //            //C:\Projects\DerinWebCore\IF.Persistence.EF.Core\bin\Debug\netcoreapp2.1
        //            dllPath = baseFrameworkPath + "/" + dllUniqueName + "/bin/" + binFolder + "/netcoreapp2.1/" + dllUniqueName + ".dll";

        //            if (!File.Exists(dllPath))
        //            {
        //                throw new Exception(dllName + " dosyasi bulunamadi");
        //            }

        //            if (checkBoxBuildAllProjects.Checked)
        //            {
        //                BuildProject(baseFrameworkPath, dllUniqueName);
        //            }

        //            File.Copy(dllPath,basePublishPath+"/"+ dllUniqueName + ".dll",true);
        //            continue;
        //        }


        //        if (
        //            dllUniqueName.StartsWith("IF.Configuration")
        //            || dllUniqueName.StartsWith("IF.Validation.FluentValidation")
        //            || dllUniqueName.StartsWith("IF.EventBus.RabbitMQ.Integration")
        //            || dllUniqueName.StartsWith("IF.EventBus.Azure.Integration")
        //            || dllUniqueName.StartsWith("IF.EventBus.Azure")
        //            || dllUniqueName.StartsWith("IF.Core")
        //            || dllUniqueName.StartsWith("IF.Jwt")
        //            || dllUniqueName.StartsWith("IF.Redis")
        //            || dllUniqueName.StartsWith("IF.Json")
        //            || dllUniqueName.StartsWith("IF.DynamicData")
        //            || dllUniqueName.StartsWith("IF.Persistence")
        //            || dllUniqueName.StartsWith("IF.EventBus.RabbitMQ")
        //            || dllUniqueName.StartsWith("IF.AutoMapper")
        //            || dllUniqueName.StartsWith("IF.RazorviewEngine")
        //            || dllUniqueName.StartsWith("IF.Rest.Client")
        //            || dllUniqueName.StartsWith("IF.Elasticsearch")
        //            || dllUniqueName.StartsWith("IF.HealthChecks")
        //            || dllUniqueName.StartsWith("IF.HealthChecks.SqlServer")
        //            || dllUniqueName.StartsWith("IF.HealthChecks.Elasticsearch")
        //            || dllUniqueName.StartsWith("IF.HealthChecks.RabbitMQ")
        //            || dllUniqueName.StartsWith("IF.Dependency.AutoFac")
        //            || dllUniqueName.StartsWith("IF.MongoDB")

        //            )
        //        {
        //            //C:\Projects\DerinWebCore\IF.Persistence.EF.Core\bin\Debug\netcoreapp2.1
        //            dllPath = baseFrameworkPath + "/" + dllUniqueName + "/bin/" + binFolder + "/netstandard2.0/" + dllUniqueName + ".dll";

        //            if (!File.Exists(dllPath))
        //            {
        //                throw new Exception(dllName + " dosyasi bulunamadi");
        //            }

        //            if (checkBoxBuildAllProjects.Checked)
        //            {
        //                BuildProject(baseFrameworkPath, dllUniqueName);
        //            }

        //            File.Copy(dllPath, basePublishPath + "/" + dllUniqueName + ".dll",true);
        //            continue;
        //        }


        //        if (!File.Exists(dllPath))
        //        {
        //            throw new Exception(dllPath + " dosyasi bulunamadi");
        //        }               

                

        //        if (checkBoxBuildAllProjects.Checked)
        //        {
        //            BuildProject(baseFrameworkPath, dllUniqueName);
        //        }

        //        File.Copy(dllPath, basePublishPath + dllName,true);
        //    }
        //}

        private void BuildProject(string baseFrameworkPath, string dllUniqueName)
        {
            var projectPath = baseFrameworkPath + fwDllPath + dllUniqueName + fwDllPath + dllUniqueName + ".csproj";

            var buildLogPath = @"C:\Temp\"+ frameworkIndent +"Build.log";

            if(!File.Exists(buildLogPath))
            {
                File.Create(buildLogPath);
            }

            UnloadAnyProject();

            Microsoft.Build.Evaluation.Project p = new Microsoft.Build.Evaluation.Project(projectPath);

            

            FileLogger buildLogger = new FileLogger();

            buildLogger.Parameters = String.Format(@"logfile={0}", buildLogPath);

            bool buildresult = p.Build(buildLogger);

            if (!buildresult)
            {
                MessageBox.Show(String.Format("{0 }Project not compiled, check {1}", projectPath, @"C:\temp\myapp.msbuild.log"));

            }

            p.Save();

            UnloadAnyProject();
        }

        private void BindComboBox()
        {
            List<NameValueDto> publishModes = new List<NameValueDto>
            {
                new NameValueDto( "Debug","Debug"),
                new NameValueDto( "Release","Release")
            };

            publishModeBindingSource.DataSource = publishModes;

            comboBoxPublishMode.DataSource = publishModeBindingSource;

            comboBoxPublishMode.DisplayMember = "Name";
            comboBoxPublishMode.ValueMember = "Value";
        }

        private void buttonBaseDirectorySelect_Click(object sender, EventArgs e)
        {
            if (baseDirectoryBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxBaseDirectory.Text = baseDirectoryBrowserDialog.SelectedPath;
            }
        }

        private static void UnloadAnyProject()
        {
            Microsoft.Build.Evaluation.ProjectCollection projcoll = Microsoft.Build.Evaluation.ProjectCollection.GlobalProjectCollection;

            foreach (Microsoft.Build.Evaluation.Project pr in projcoll.LoadedProjects)
            {
                Microsoft.Build.Evaluation.ProjectCollection mypcollection = pr.ProjectCollection;
                mypcollection.UnloadProject(pr);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddProjectModuleDialog projectDialog = new AddProjectModuleDialog(this);

            projectDialog.ShowDialog();
        }

       

        private void buttonDelete_Click(object sender, EventArgs e)
        {

            JsonDataContext jsonDataContext = new JsonDataContext(new NewtonsoftJsonSerializer());

            foreach (Project item in listBoxAllProjects.SelectedItems)
            {
                jsonDataContext.Delete<Project>(item.UniqueId, Helper.GetJsonPath());
            }

            listBoxAllProjects.Refresh();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Project project = listBoxAllProjects.SelectedItem as Project;

            EditProjectModuleDialog editProjectDialog = new EditProjectModuleDialog(this,project);

            editProjectDialog.ShowDialog();
        }

        private void buttonPublish2_Click(object sender, EventArgs e)
        {
            var project = this.listBoxAllProjects.SelectedItem as Project;

            string basePublishPath = project.Path;
            string binFolder = comboBoxPublishMode.SelectedValue.ToString();

            foreach (var projectModule in project.Modules)
            {
                CopyModule(baseFrameworkPath,basePublishPath, binFolder, projectModule);

            }

            if (project.ParentProjects != null)
            {

                foreach (var parentProject in project.ParentProjects)
                {
                    foreach (var projectModule in parentProject.FrameworkModules)
                    {
                        if (projectModule.UseIn)
                        {
                            CopyModule(parentProject.ProjectPath,basePublishPath, binFolder, projectModule);
                        }
                    }
                }
            }

            MessageBox.Show("Publish done!");
        }

        private void CopyModule(string baseFrameworkPath,string basePublishPath, string binFolder, ProjectModule projectModule)
        {
            if (projectModule.Type == ProjectType.Net)
            {

                var dllName = projectModule.ModuleName + ".dll";

                var dllPath = baseFrameworkPath + "/" + projectModule.ModuleName + "/bin/" + binFolder + "/" + dllName;

                if (!File.Exists(dllPath))
                {
                    throw new Exception(dllPath + " dosyasi bulunamadi");
                }

                if (checkBoxBuildAllProjects.Checked)
                {
                    BuildProject(baseFrameworkPath, projectModule.ModuleName);
                }

                File.Copy(dllPath, basePublishPath + "/" + dllName, true);

            }
            else if (projectModule.Type == ProjectType.Core)
            {
                //C:\Projects\DerinWebCore\IF.Persistence.EF.Core\bin\Debug\netcoreapp2.1
                var dllPath = baseFrameworkPath + "/" + projectModule.ModuleName + "/bin/" + binFolder + "/netcoreapp2.1/" + projectModule.ModuleName + ".dll";

                var dllName = projectModule.ModuleName + ".dll";

                if (!File.Exists(dllPath))
                {
                    throw new Exception(dllName + " dosyasi bulunamadi");
                }

                if (checkBoxBuildAllProjects.Checked)
                {
                    BuildProject(baseFrameworkPath, projectModule.ModuleName);
                }

                File.Copy(dllPath, basePublishPath + "/" + projectModule.ModuleName + ".dll", true);
            }
            else if (projectModule.Type == ProjectType.Standart)
            {
                //C:\Projects\DerinWebCore\IF.Persistence.EF.Core\bin\Debug\netcoreapp2.1
                var dllPath = $"{baseFrameworkPath}/{(projectModule.Path == null ? "" : projectModule.Path + "/" )}{projectModule.ModuleName}/bin/{binFolder}/netstandard2.0/{projectModule.ModuleName}.dll";

                var dllName = projectModule.ModuleName + ".dll";

                if (!File.Exists(dllPath))
                {
                    throw new Exception(dllName + " dosyasi bulunamadi");
                }

                if (checkBoxBuildAllProjects.Checked)
                {
                    BuildProject(baseFrameworkPath, projectModule.ModuleName);
                }

                File.Copy(dllPath, basePublishPath + "/" + projectModule.ModuleName + ".dll", true);
            }
        }

    }
}
