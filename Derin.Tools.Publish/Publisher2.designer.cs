namespace IF.Tools.Publish
{
    public partial class Publisher2
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.baseDirectoryBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.comboBoxPublishDirectory = new System.Windows.Forms.ComboBox();
            this.comboBoxPublishMode = new System.Windows.Forms.ComboBox();
            this.labelPublishDirectory = new System.Windows.Forms.Label();
            this.PublishMode = new System.Windows.Forms.Label();
            this.publishDirectoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.publishModeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.textBoxBaseDirectory = new System.Windows.Forms.TextBox();
            this.buttonBaseDirectorySelect = new System.Windows.Forms.Button();
            this.labelFrameworkPath = new System.Windows.Forms.Label();
            this.checkBoxBuildAllProjects = new System.Windows.Forms.CheckBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.labelChildProjects = new System.Windows.Forms.Label();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.listBoxAllProjects = new System.Windows.Forms.ListBox();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonPublish2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.publishDirectoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.publishModeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(913, 232);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Publish";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxPublishDirectory
            // 
            this.comboBoxPublishDirectory.FormattingEnabled = true;
            this.comboBoxPublishDirectory.Location = new System.Drawing.Point(780, 174);
            this.comboBoxPublishDirectory.Name = "comboBoxPublishDirectory";
            this.comboBoxPublishDirectory.Size = new System.Drawing.Size(208, 21);
            this.comboBoxPublishDirectory.TabIndex = 1;
            // 
            // comboBoxPublishMode
            // 
            this.comboBoxPublishMode.FormattingEnabled = true;
            this.comboBoxPublishMode.Location = new System.Drawing.Point(780, 118);
            this.comboBoxPublishMode.Name = "comboBoxPublishMode";
            this.comboBoxPublishMode.Size = new System.Drawing.Size(208, 21);
            this.comboBoxPublishMode.TabIndex = 2;
            // 
            // labelPublishDirectory
            // 
            this.labelPublishDirectory.AutoSize = true;
            this.labelPublishDirectory.Location = new System.Drawing.Point(669, 182);
            this.labelPublishDirectory.Name = "labelPublishDirectory";
            this.labelPublishDirectory.Size = new System.Drawing.Size(86, 13);
            this.labelPublishDirectory.TabIndex = 3;
            this.labelPublishDirectory.Text = "Publish Directory";
            // 
            // PublishMode
            // 
            this.PublishMode.AutoSize = true;
            this.PublishMode.Location = new System.Drawing.Point(684, 126);
            this.PublishMode.Name = "PublishMode";
            this.PublishMode.Size = new System.Drawing.Size(71, 13);
            this.PublishMode.TabIndex = 4;
            this.PublishMode.Text = "Publish Mode";
            // 
            // textBoxBaseDirectory
            // 
            this.textBoxBaseDirectory.Location = new System.Drawing.Point(780, 75);
            this.textBoxBaseDirectory.Name = "textBoxBaseDirectory";
            this.textBoxBaseDirectory.ReadOnly = true;
            this.textBoxBaseDirectory.Size = new System.Drawing.Size(208, 20);
            this.textBoxBaseDirectory.TabIndex = 5;
            // 
            // buttonBaseDirectorySelect
            // 
            this.buttonBaseDirectorySelect.Location = new System.Drawing.Point(994, 72);
            this.buttonBaseDirectorySelect.Name = "buttonBaseDirectorySelect";
            this.buttonBaseDirectorySelect.Size = new System.Drawing.Size(75, 23);
            this.buttonBaseDirectorySelect.TabIndex = 6;
            this.buttonBaseDirectorySelect.Text = "Seç";
            this.buttonBaseDirectorySelect.UseVisualStyleBackColor = true;
            this.buttonBaseDirectorySelect.Click += new System.EventHandler(this.buttonBaseDirectorySelect_Click);
            // 
            // labelFrameworkPath
            // 
            this.labelFrameworkPath.AutoSize = true;
            this.labelFrameworkPath.Location = new System.Drawing.Point(684, 82);
            this.labelFrameworkPath.Name = "labelFrameworkPath";
            this.labelFrameworkPath.Size = new System.Drawing.Size(84, 13);
            this.labelFrameworkPath.TabIndex = 7;
            this.labelFrameworkPath.Text = "Framework Path";
            // 
            // checkBoxBuildAllProjects
            // 
            this.checkBoxBuildAllProjects.AutoSize = true;
            this.checkBoxBuildAllProjects.Location = new System.Drawing.Point(780, 237);
            this.checkBoxBuildAllProjects.Name = "checkBoxBuildAllProjects";
            this.checkBoxBuildAllProjects.Size = new System.Drawing.Size(104, 17);
            this.checkBoxBuildAllProjects.TabIndex = 8;
            this.checkBoxBuildAllProjects.Text = "Build All Projects";
            this.checkBoxBuildAllProjects.UseVisualStyleBackColor = true;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(70, 82);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 9;
            this.buttonAdd.Text = "Yeni Proje Ekle";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // labelChildProjects
            // 
            this.labelChildProjects.AutoSize = true;
            this.labelChildProjects.Location = new System.Drawing.Point(67, 41);
            this.labelChildProjects.Name = "labelChildProjects";
            this.labelChildProjects.Size = new System.Drawing.Size(59, 13);
            this.labelChildProjects.TabIndex = 24;
            this.labelChildProjects.Text = "All Projects";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(70, 182);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 25;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // listBoxAllProjects
            // 
            this.listBoxAllProjects.FormattingEnabled = true;
            this.listBoxAllProjects.Location = new System.Drawing.Point(166, 41);
            this.listBoxAllProjects.Name = "listBoxAllProjects";
            this.listBoxAllProjects.Size = new System.Drawing.Size(454, 511);
            this.listBoxAllProjects.TabIndex = 26;
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(70, 126);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 27;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonPublish2
            // 
            this.buttonPublish2.Location = new System.Drawing.Point(913, 297);
            this.buttonPublish2.Name = "buttonPublish2";
            this.buttonPublish2.Size = new System.Drawing.Size(75, 23);
            this.buttonPublish2.TabIndex = 28;
            this.buttonPublish2.Text = "Publish Yeni";
            this.buttonPublish2.UseVisualStyleBackColor = true;
            this.buttonPublish2.Click += new System.EventHandler(this.buttonPublish2_Click);
            // 
            // Publisher2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 656);
            this.Controls.Add(this.buttonPublish2);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.listBoxAllProjects);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.labelChildProjects);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.checkBoxBuildAllProjects);
            this.Controls.Add(this.labelFrameworkPath);
            this.Controls.Add(this.buttonBaseDirectorySelect);
            this.Controls.Add(this.textBoxBaseDirectory);
            this.Controls.Add(this.PublishMode);
            this.Controls.Add(this.labelPublishDirectory);
            this.Controls.Add(this.comboBoxPublishMode);
            this.Controls.Add(this.comboBoxPublishDirectory);
            this.Controls.Add(this.button1);
            this.Name = "Publisher2";
            this.Text = "Framework Publisher";
            ((System.ComponentModel.ISupportInitialize)(this.publishDirectoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.publishModeBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog baseDirectoryBrowserDialog;
        private System.Windows.Forms.ComboBox comboBoxPublishDirectory;
        private System.Windows.Forms.ComboBox comboBoxPublishMode;
        private System.Windows.Forms.Label labelPublishDirectory;
        private System.Windows.Forms.Label PublishMode;
        private System.Windows.Forms.BindingSource publishDirectoryBindingSource;
        private System.Windows.Forms.BindingSource publishModeBindingSource;
        private System.Windows.Forms.TextBox textBoxBaseDirectory;
        private System.Windows.Forms.Button buttonBaseDirectorySelect;
        private System.Windows.Forms.Label labelFrameworkPath;
        private System.Windows.Forms.CheckBox checkBoxBuildAllProjects;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label labelChildProjects;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.ListBox listBoxAllProjects;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonPublish2;
    }
}

