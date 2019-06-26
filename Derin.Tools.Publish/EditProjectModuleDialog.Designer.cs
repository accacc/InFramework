namespace Derin.Tools.Publish
{
    partial class EditProjectModuleDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelProjectPath = new System.Windows.Forms.Label();
            this.buttonPublishDirectorySelect = new System.Windows.Forms.Button();
            this.textBoxPublishDirectory = new System.Windows.Forms.TextBox();
            this.labelProjectName = new System.Windows.Forms.Label();
            this.textBoxProjectName = new System.Windows.Forms.TextBox();
            this.checkedListBoxModules = new System.Windows.Forms.CheckedListBox();
            this.labelModules = new System.Windows.Forms.Label();
            this.labelChildProjects = new System.Windows.Forms.Label();
            this.checkedListBoxChildProjects = new System.Windows.Forms.CheckedListBox();
            this.labelProjectType = new System.Windows.Forms.Label();
            this.comboBoxProjectType = new System.Windows.Forms.ComboBox();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.directoryBrowserDialogPublishPath = new System.Windows.Forms.FolderBrowserDialog();
            this.checkBoxFrameworkProject = new System.Windows.Forms.CheckBox();
            this.labelFrameworkProject = new System.Windows.Forms.Label();
            this.labelFrameworkModule = new System.Windows.Forms.Label();
            this.checkedListBoxFrameworkModule = new System.Windows.Forms.CheckedListBox();
            this.panelFrameworkModules = new System.Windows.Forms.Panel();
            this.buttonFrameworkModuleEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAddFrameworkModule = new System.Windows.Forms.Button();
            this.labelParentProjects = new System.Windows.Forms.Label();
            this.buttonAddParentProject = new System.Windows.Forms.Button();
            this.buttonEditParentProject = new System.Windows.Forms.Button();
            this.listBoxParents = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonProjectPath = new System.Windows.Forms.Button();
            this.textBoxProjectPath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialogProjectPath = new System.Windows.Forms.FolderBrowserDialog();
            this.panelFrameworkModules.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelProjectPath
            // 
            this.labelProjectPath.AutoSize = true;
            this.labelProjectPath.Location = new System.Drawing.Point(12, 29);
            this.labelProjectPath.Name = "labelProjectPath";
            this.labelProjectPath.Size = new System.Drawing.Size(66, 13);
            this.labelProjectPath.TabIndex = 16;
            this.labelProjectPath.Text = "Publish Path";
            // 
            // buttonPublishDirectorySelect
            // 
            this.buttonPublishDirectorySelect.Location = new System.Drawing.Point(322, 19);
            this.buttonPublishDirectorySelect.Name = "buttonPublishDirectorySelect";
            this.buttonPublishDirectorySelect.Size = new System.Drawing.Size(75, 23);
            this.buttonPublishDirectorySelect.TabIndex = 15;
            this.buttonPublishDirectorySelect.Text = "Seç";
            this.buttonPublishDirectorySelect.UseVisualStyleBackColor = true;
            this.buttonPublishDirectorySelect.Click += new System.EventHandler(this.buttonBaseDirectorySelect_Click);
            // 
            // textBoxPublishDirectory
            // 
            this.textBoxPublishDirectory.Location = new System.Drawing.Point(108, 22);
            this.textBoxPublishDirectory.Name = "textBoxPublishDirectory";
            this.textBoxPublishDirectory.ReadOnly = true;
            this.textBoxPublishDirectory.Size = new System.Drawing.Size(208, 20);
            this.textBoxPublishDirectory.TabIndex = 14;
            // 
            // labelProjectName
            // 
            this.labelProjectName.AutoSize = true;
            this.labelProjectName.Location = new System.Drawing.Point(7, 119);
            this.labelProjectName.Name = "labelProjectName";
            this.labelProjectName.Size = new System.Drawing.Size(71, 13);
            this.labelProjectName.TabIndex = 13;
            this.labelProjectName.Text = "Project Name";
            // 
            // textBoxProjectName
            // 
            this.textBoxProjectName.Location = new System.Drawing.Point(107, 119);
            this.textBoxProjectName.Name = "textBoxProjectName";
            this.textBoxProjectName.Size = new System.Drawing.Size(204, 20);
            this.textBoxProjectName.TabIndex = 18;
            // 
            // checkedListBoxModules
            // 
            this.checkedListBoxModules.FormattingEnabled = true;
            this.checkedListBoxModules.Location = new System.Drawing.Point(17, 278);
            this.checkedListBoxModules.Name = "checkedListBoxModules";
            this.checkedListBoxModules.Size = new System.Drawing.Size(380, 514);
            this.checkedListBoxModules.TabIndex = 19;
            // 
            // labelModules
            // 
            this.labelModules.AutoSize = true;
            this.labelModules.Location = new System.Drawing.Point(12, 243);
            this.labelModules.Name = "labelModules";
            this.labelModules.Size = new System.Drawing.Size(47, 13);
            this.labelModules.TabIndex = 20;
            this.labelModules.Text = "Modules";
            // 
            // labelChildProjects
            // 
            this.labelChildProjects.AutoSize = true;
            this.labelChildProjects.Location = new System.Drawing.Point(489, 463);
            this.labelChildProjects.Name = "labelChildProjects";
            this.labelChildProjects.Size = new System.Drawing.Size(71, 13);
            this.labelChildProjects.TabIndex = 22;
            this.labelChildProjects.Text = "Child Projects";
            // 
            // checkedListBoxChildProjects
            // 
            this.checkedListBoxChildProjects.FormattingEnabled = true;
            this.checkedListBoxChildProjects.Location = new System.Drawing.Point(579, 463);
            this.checkedListBoxChildProjects.Name = "checkedListBoxChildProjects";
            this.checkedListBoxChildProjects.Size = new System.Drawing.Size(366, 319);
            this.checkedListBoxChildProjects.TabIndex = 21;
            // 
            // labelProjectType
            // 
            this.labelProjectType.AutoSize = true;
            this.labelProjectType.Location = new System.Drawing.Point(11, 170);
            this.labelProjectType.Name = "labelProjectType";
            this.labelProjectType.Size = new System.Drawing.Size(67, 13);
            this.labelProjectType.TabIndex = 23;
            this.labelProjectType.Text = "Project Type";
            // 
            // comboBoxProjectType
            // 
            this.comboBoxProjectType.FormattingEnabled = true;
            this.comboBoxProjectType.Location = new System.Drawing.Point(111, 161);
            this.comboBoxProjectType.Name = "comboBoxProjectType";
            this.comboBoxProjectType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxProjectType.TabIndex = 24;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(1010, 759);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdate.TabIndex = 25;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // checkBoxFrameworkProject
            // 
            this.checkBoxFrameworkProject.AutoSize = true;
            this.checkBoxFrameworkProject.Location = new System.Drawing.Point(112, 205);
            this.checkBoxFrameworkProject.Name = "checkBoxFrameworkProject";
            this.checkBoxFrameworkProject.Size = new System.Drawing.Size(15, 14);
            this.checkBoxFrameworkProject.TabIndex = 26;
            this.checkBoxFrameworkProject.UseVisualStyleBackColor = true;
            this.checkBoxFrameworkProject.CheckedChanged += new System.EventHandler(this.checkBoxFrameworkProject_CheckedChanged);
            // 
            // labelFrameworkProject
            // 
            this.labelFrameworkProject.AutoSize = true;
            this.labelFrameworkProject.Location = new System.Drawing.Point(11, 205);
            this.labelFrameworkProject.Name = "labelFrameworkProject";
            this.labelFrameworkProject.Size = new System.Drawing.Size(95, 13);
            this.labelFrameworkProject.TabIndex = 27;
            this.labelFrameworkProject.Text = "Framework Project";
            // 
            // labelFrameworkModule
            // 
            this.labelFrameworkModule.AutoSize = true;
            this.labelFrameworkModule.Location = new System.Drawing.Point(14, 73);
            this.labelFrameworkModule.Name = "labelFrameworkModule";
            this.labelFrameworkModule.Size = new System.Drawing.Size(102, 13);
            this.labelFrameworkModule.TabIndex = 29;
            this.labelFrameworkModule.Text = "Framework Modules";
            // 
            // checkedListBoxFrameworkModule
            // 
            this.checkedListBoxFrameworkModule.FormattingEnabled = true;
            this.checkedListBoxFrameworkModule.Location = new System.Drawing.Point(232, 73);
            this.checkedListBoxFrameworkModule.Name = "checkedListBoxFrameworkModule";
            this.checkedListBoxFrameworkModule.Size = new System.Drawing.Size(472, 604);
            this.checkedListBoxFrameworkModule.TabIndex = 28;
            // 
            // panelFrameworkModules
            // 
            this.panelFrameworkModules.Controls.Add(this.buttonFrameworkModuleEdit);
            this.panelFrameworkModules.Controls.Add(this.buttonDelete);
            this.panelFrameworkModules.Controls.Add(this.buttonAddFrameworkModule);
            this.panelFrameworkModules.Controls.Add(this.checkedListBoxFrameworkModule);
            this.panelFrameworkModules.Controls.Add(this.labelFrameworkModule);
            this.panelFrameworkModules.Location = new System.Drawing.Point(1010, 19);
            this.panelFrameworkModules.Name = "panelFrameworkModules";
            this.panelFrameworkModules.Size = new System.Drawing.Size(724, 693);
            this.panelFrameworkModules.TabIndex = 30;
            // 
            // buttonFrameworkModuleEdit
            // 
            this.buttonFrameworkModuleEdit.Location = new System.Drawing.Point(416, 21);
            this.buttonFrameworkModuleEdit.Name = "buttonFrameworkModuleEdit";
            this.buttonFrameworkModuleEdit.Size = new System.Drawing.Size(88, 23);
            this.buttonFrameworkModuleEdit.TabIndex = 32;
            this.buttonFrameworkModuleEdit.Text = "Edit";
            this.buttonFrameworkModuleEdit.UseVisualStyleBackColor = true;
            this.buttonFrameworkModuleEdit.Click += new System.EventHandler(this.buttonFrameworkModuleEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(322, 21);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(88, 23);
            this.buttonDelete.TabIndex = 31;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonAddFrameworkModule
            // 
            this.buttonAddFrameworkModule.Location = new System.Drawing.Point(232, 21);
            this.buttonAddFrameworkModule.Name = "buttonAddFrameworkModule";
            this.buttonAddFrameworkModule.Size = new System.Drawing.Size(84, 23);
            this.buttonAddFrameworkModule.TabIndex = 30;
            this.buttonAddFrameworkModule.Text = "Add";
            this.buttonAddFrameworkModule.UseVisualStyleBackColor = true;
            this.buttonAddFrameworkModule.Click += new System.EventHandler(this.buttonAddFrameworkModule_Click);
            // 
            // labelParentProjects
            // 
            this.labelParentProjects.AutoSize = true;
            this.labelParentProjects.Location = new System.Drawing.Point(489, 110);
            this.labelParentProjects.Name = "labelParentProjects";
            this.labelParentProjects.Size = new System.Drawing.Size(79, 13);
            this.labelParentProjects.TabIndex = 32;
            this.labelParentProjects.Text = "Parent Projects";
            // 
            // buttonAddParentProject
            // 
            this.buttonAddParentProject.Location = new System.Drawing.Point(579, 40);
            this.buttonAddParentProject.Name = "buttonAddParentProject";
            this.buttonAddParentProject.Size = new System.Drawing.Size(122, 23);
            this.buttonAddParentProject.TabIndex = 33;
            this.buttonAddParentProject.Text = "Add Parent Project";
            this.buttonAddParentProject.UseVisualStyleBackColor = true;
            this.buttonAddParentProject.Click += new System.EventHandler(this.buttonAddParentProject_Click);
            // 
            // buttonEditParentProject
            // 
            this.buttonEditParentProject.Location = new System.Drawing.Point(453, 195);
            this.buttonEditParentProject.Name = "buttonEditParentProject";
            this.buttonEditParentProject.Size = new System.Drawing.Size(122, 23);
            this.buttonEditParentProject.TabIndex = 34;
            this.buttonEditParentProject.Text = "Edit Parent Project";
            this.buttonEditParentProject.UseVisualStyleBackColor = true;
            this.buttonEditParentProject.Click += new System.EventHandler(this.buttonEditParentProject_Click);
            // 
            // listBoxParents
            // 
            this.listBoxParents.FormattingEnabled = true;
            this.listBoxParents.Location = new System.Drawing.Point(581, 110);
            this.listBoxParents.Name = "listBoxParents";
            this.listBoxParents.Size = new System.Drawing.Size(364, 277);
            this.listBoxParents.TabIndex = 35;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(453, 149);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 23);
            this.button1.TabIndex = 36;
            this.button1.Text = "Delete Parent Project";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Project Path";
            // 
            // buttonProjectPath
            // 
            this.buttonProjectPath.Location = new System.Drawing.Point(321, 61);
            this.buttonProjectPath.Name = "buttonProjectPath";
            this.buttonProjectPath.Size = new System.Drawing.Size(75, 23);
            this.buttonProjectPath.TabIndex = 41;
            this.buttonProjectPath.Text = "Seç";
            this.buttonProjectPath.UseVisualStyleBackColor = true;
            this.buttonProjectPath.Click += new System.EventHandler(this.buttonProjectPath_Click);
            // 
            // textBoxProjectPath
            // 
            this.textBoxProjectPath.Location = new System.Drawing.Point(107, 64);
            this.textBoxProjectPath.Name = "textBoxProjectPath";
            this.textBoxProjectPath.ReadOnly = true;
            this.textBoxProjectPath.Size = new System.Drawing.Size(208, 20);
            this.textBoxProjectPath.TabIndex = 40;
            // 
            // EditProjectModuleDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1364, 749);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonProjectPath);
            this.Controls.Add(this.textBoxProjectPath);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBoxParents);
            this.Controls.Add(this.buttonEditParentProject);
            this.Controls.Add(this.buttonAddParentProject);
            this.Controls.Add(this.labelParentProjects);
            this.Controls.Add(this.panelFrameworkModules);
            this.Controls.Add(this.labelFrameworkProject);
            this.Controls.Add(this.checkBoxFrameworkProject);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.comboBoxProjectType);
            this.Controls.Add(this.labelProjectType);
            this.Controls.Add(this.labelChildProjects);
            this.Controls.Add(this.checkedListBoxChildProjects);
            this.Controls.Add(this.labelModules);
            this.Controls.Add(this.checkedListBoxModules);
            this.Controls.Add(this.textBoxProjectName);
            this.Controls.Add(this.labelProjectPath);
            this.Controls.Add(this.buttonPublishDirectorySelect);
            this.Controls.Add(this.textBoxPublishDirectory);
            this.Controls.Add(this.labelProjectName);
            this.Name = "EditProjectModuleDialog";
            this.Text = "ProjectDialog";
            this.panelFrameworkModules.ResumeLayout(false);
            this.panelFrameworkModules.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelProjectPath;
        private System.Windows.Forms.Button buttonPublishDirectorySelect;
        private System.Windows.Forms.TextBox textBoxPublishDirectory;
        private System.Windows.Forms.Label labelProjectName;
        private System.Windows.Forms.TextBox textBoxProjectName;
        private System.Windows.Forms.CheckedListBox checkedListBoxModules;
        private System.Windows.Forms.Label labelModules;
        private System.Windows.Forms.Label labelChildProjects;
        private System.Windows.Forms.CheckedListBox checkedListBoxChildProjects;
        private System.Windows.Forms.Label labelProjectType;
        private System.Windows.Forms.ComboBox comboBoxProjectType;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.FolderBrowserDialog directoryBrowserDialogPublishPath;
        private System.Windows.Forms.CheckBox checkBoxFrameworkProject;
        private System.Windows.Forms.Label labelFrameworkProject;
        private System.Windows.Forms.Label labelFrameworkModule;
        private System.Windows.Forms.CheckedListBox checkedListBoxFrameworkModule;
        private System.Windows.Forms.Panel panelFrameworkModules;
        private System.Windows.Forms.Button buttonAddFrameworkModule;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Label labelParentProjects;
        private System.Windows.Forms.Button buttonAddParentProject;
        private System.Windows.Forms.Button buttonEditParentProject;
        internal System.Windows.Forms.ListBox listBoxParents;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonProjectPath;
        private System.Windows.Forms.TextBox textBoxProjectPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogProjectPath;
        private System.Windows.Forms.Button buttonFrameworkModuleEdit;
    }
}