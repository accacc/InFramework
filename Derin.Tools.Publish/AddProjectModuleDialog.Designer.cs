namespace Derin.Tools.Publish
{
    partial class AddProjectModuleDialog
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
            this.labelPublishPath = new System.Windows.Forms.Label();
            this.buttonPublishDirectory = new System.Windows.Forms.Button();
            this.textBoxPublishDirectory = new System.Windows.Forms.TextBox();
            this.labelProjectName = new System.Windows.Forms.Label();
            this.textBoxProjectName = new System.Windows.Forms.TextBox();
            this.checkedListBoxModules = new System.Windows.Forms.CheckedListBox();
            this.labelModules = new System.Windows.Forms.Label();
            this.labelProjectType = new System.Windows.Forms.Label();
            this.comboBoxProjectType = new System.Windows.Forms.ComboBox();
            this.Save = new System.Windows.Forms.Button();
            this.browserDialogPublishDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.checkBoxFrameworkProject = new System.Windows.Forms.CheckBox();
            this.labelFrameworkProject = new System.Windows.Forms.Label();
            this.labelFrameworkModule = new System.Windows.Forms.Label();
            this.checkedListBoxFrameworkModule = new System.Windows.Forms.CheckedListBox();
            this.panelFrameworkModules = new System.Windows.Forms.Panel();
            this.buttonAddFrameworkModule = new System.Windows.Forms.Button();
            this.labelProjectPath = new System.Windows.Forms.Label();
            this.buttonProjectPathSelect = new System.Windows.Forms.Button();
            this.textBoxProjectPath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialogProjectPath = new System.Windows.Forms.FolderBrowserDialog();
            this.panelFrameworkModules.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelPublishPath
            // 
            this.labelPublishPath.AutoSize = true;
            this.labelPublishPath.Location = new System.Drawing.Point(12, 29);
            this.labelPublishPath.Name = "labelPublishPath";
            this.labelPublishPath.Size = new System.Drawing.Size(66, 13);
            this.labelPublishPath.TabIndex = 16;
            this.labelPublishPath.Text = "Publish Path";
            // 
            // buttonPublishDirectory
            // 
            this.buttonPublishDirectory.Location = new System.Drawing.Point(322, 19);
            this.buttonPublishDirectory.Name = "buttonPublishDirectory";
            this.buttonPublishDirectory.Size = new System.Drawing.Size(75, 23);
            this.buttonPublishDirectory.TabIndex = 15;
            this.buttonPublishDirectory.Text = "Seç";
            this.buttonPublishDirectory.UseVisualStyleBackColor = true;
            this.buttonPublishDirectory.Click += new System.EventHandler(this.buttonBaseDirectorySelect_Click);
            // 
            // textBoxPublishDirectory
            // 
            this.textBoxPublishDirectory.Location = new System.Drawing.Point(112, 22);
            this.textBoxPublishDirectory.Name = "textBoxPublishDirectory";
            this.textBoxPublishDirectory.ReadOnly = true;
            this.textBoxPublishDirectory.Size = new System.Drawing.Size(208, 20);
            this.textBoxPublishDirectory.TabIndex = 14;
            // 
            // labelProjectName
            // 
            this.labelProjectName.AutoSize = true;
            this.labelProjectName.Location = new System.Drawing.Point(12, 112);
            this.labelProjectName.Name = "labelProjectName";
            this.labelProjectName.Size = new System.Drawing.Size(71, 13);
            this.labelProjectName.TabIndex = 13;
            this.labelProjectName.Text = "Project Name";
            // 
            // textBoxProjectName
            // 
            this.textBoxProjectName.Location = new System.Drawing.Point(112, 112);
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
            // labelProjectType
            // 
            this.labelProjectType.AutoSize = true;
            this.labelProjectType.Location = new System.Drawing.Point(12, 166);
            this.labelProjectType.Name = "labelProjectType";
            this.labelProjectType.Size = new System.Drawing.Size(67, 13);
            this.labelProjectType.TabIndex = 23;
            this.labelProjectType.Text = "Project Type";
            // 
            // comboBoxProjectType
            // 
            this.comboBoxProjectType.FormattingEnabled = true;
            this.comboBoxProjectType.Location = new System.Drawing.Point(112, 157);
            this.comboBoxProjectType.Name = "comboBoxProjectType";
            this.comboBoxProjectType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxProjectType.TabIndex = 24;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(1010, 759);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 25;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.buttonSave_Click);
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
            this.panelFrameworkModules.Controls.Add(this.buttonAddFrameworkModule);
            this.panelFrameworkModules.Controls.Add(this.checkedListBoxFrameworkModule);
            this.panelFrameworkModules.Controls.Add(this.labelFrameworkModule);
            this.panelFrameworkModules.Location = new System.Drawing.Point(636, 22);
            this.panelFrameworkModules.Name = "panelFrameworkModules";
            this.panelFrameworkModules.Size = new System.Drawing.Size(724, 693);
            this.panelFrameworkModules.TabIndex = 30;
            // 
            // buttonAddFrameworkModule
            // 
            this.buttonAddFrameworkModule.Location = new System.Drawing.Point(232, 21);
            this.buttonAddFrameworkModule.Name = "buttonAddFrameworkModule";
            this.buttonAddFrameworkModule.Size = new System.Drawing.Size(166, 23);
            this.buttonAddFrameworkModule.TabIndex = 30;
            this.buttonAddFrameworkModule.Text = "Add Framework Module";
            this.buttonAddFrameworkModule.UseVisualStyleBackColor = true;
            this.buttonAddFrameworkModule.Click += new System.EventHandler(this.buttonAddFrameworkModule_Click);
            // 
            // labelProjectPath
            // 
            this.labelProjectPath.AutoSize = true;
            this.labelProjectPath.Location = new System.Drawing.Point(12, 72);
            this.labelProjectPath.Name = "labelProjectPath";
            this.labelProjectPath.Size = new System.Drawing.Size(65, 13);
            this.labelProjectPath.TabIndex = 33;
            this.labelProjectPath.Text = "Project Path";
            // 
            // buttonProjectPathSelect
            // 
            this.buttonProjectPathSelect.Location = new System.Drawing.Point(322, 62);
            this.buttonProjectPathSelect.Name = "buttonProjectPathSelect";
            this.buttonProjectPathSelect.Size = new System.Drawing.Size(75, 23);
            this.buttonProjectPathSelect.TabIndex = 32;
            this.buttonProjectPathSelect.Text = "Seç";
            this.buttonProjectPathSelect.UseVisualStyleBackColor = true;
            this.buttonProjectPathSelect.Click += new System.EventHandler(this.button_Click);
            // 
            // textBoxProjectPath
            // 
            this.textBoxProjectPath.Location = new System.Drawing.Point(108, 65);
            this.textBoxProjectPath.Name = "textBoxProjectPath";
            this.textBoxProjectPath.ReadOnly = true;
            this.textBoxProjectPath.Size = new System.Drawing.Size(208, 20);
            this.textBoxProjectPath.TabIndex = 31;
            // 
            // AddProjectModuleDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1364, 749);
            this.Controls.Add(this.labelProjectPath);
            this.Controls.Add(this.buttonProjectPathSelect);
            this.Controls.Add(this.textBoxProjectPath);
            this.Controls.Add(this.panelFrameworkModules);
            this.Controls.Add(this.labelFrameworkProject);
            this.Controls.Add(this.checkBoxFrameworkProject);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.comboBoxProjectType);
            this.Controls.Add(this.labelProjectType);
            this.Controls.Add(this.labelModules);
            this.Controls.Add(this.checkedListBoxModules);
            this.Controls.Add(this.textBoxProjectName);
            this.Controls.Add(this.labelPublishPath);
            this.Controls.Add(this.buttonPublishDirectory);
            this.Controls.Add(this.textBoxPublishDirectory);
            this.Controls.Add(this.labelProjectName);
            this.Name = "AddProjectModuleDialog";
            this.Text = "ProjectDialog";
            this.panelFrameworkModules.ResumeLayout(false);
            this.panelFrameworkModules.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelPublishPath;
        private System.Windows.Forms.Button buttonPublishDirectory;
        private System.Windows.Forms.TextBox textBoxPublishDirectory;
        private System.Windows.Forms.Label labelProjectName;
        private System.Windows.Forms.TextBox textBoxProjectName;
        private System.Windows.Forms.CheckedListBox checkedListBoxModules;
        private System.Windows.Forms.Label labelModules;
        private System.Windows.Forms.Label labelProjectType;
        private System.Windows.Forms.ComboBox comboBoxProjectType;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.FolderBrowserDialog browserDialogPublishDirectory;
        private System.Windows.Forms.CheckBox checkBoxFrameworkProject;
        private System.Windows.Forms.Label labelFrameworkProject;
        private System.Windows.Forms.Label labelFrameworkModule;
        private System.Windows.Forms.CheckedListBox checkedListBoxFrameworkModule;
        private System.Windows.Forms.Panel panelFrameworkModules;
        private System.Windows.Forms.Button buttonAddFrameworkModule;
        private System.Windows.Forms.Label labelProjectPath;
        private System.Windows.Forms.Button buttonProjectPathSelect;
        private System.Windows.Forms.TextBox textBoxProjectPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogProjectPath;
    }
}