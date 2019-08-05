namespace IF.Tools.Publish
{
    partial class AddProjectDialog
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
            this.textBoxModuleName = new System.Windows.Forms.TextBox();
            this.labelModuleName = new System.Windows.Forms.Label();
            this.textBoxDesc = new System.Windows.Forms.TextBox();
            this.labelModuleDesc = new System.Windows.Forms.Label();
            this.comboBoxModuleType = new System.Windows.Forms.ComboBox();
            this.labelModuleType = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxModuleName
            // 
            this.textBoxModuleName.Location = new System.Drawing.Point(121, 28);
            this.textBoxModuleName.Name = "textBoxModuleName";
            this.textBoxModuleName.Size = new System.Drawing.Size(204, 20);
            this.textBoxModuleName.TabIndex = 20;
            // 
            // labelModuleName
            // 
            this.labelModuleName.AutoSize = true;
            this.labelModuleName.Location = new System.Drawing.Point(21, 28);
            this.labelModuleName.Name = "labelModuleName";
            this.labelModuleName.Size = new System.Drawing.Size(73, 13);
            this.labelModuleName.TabIndex = 19;
            this.labelModuleName.Text = "Module Name";
            // 
            // textBoxDesc
            // 
            this.textBoxDesc.Location = new System.Drawing.Point(121, 79);
            this.textBoxDesc.Name = "textBoxDesc";
            this.textBoxDesc.Size = new System.Drawing.Size(204, 20);
            this.textBoxDesc.TabIndex = 22;
            // 
            // labelModuleDesc
            // 
            this.labelModuleDesc.AutoSize = true;
            this.labelModuleDesc.Location = new System.Drawing.Point(21, 79);
            this.labelModuleDesc.Name = "labelModuleDesc";
            this.labelModuleDesc.Size = new System.Drawing.Size(60, 13);
            this.labelModuleDesc.TabIndex = 21;
            this.labelModuleDesc.Text = "Description";
            // 
            // comboBoxModuleType
            // 
            this.comboBoxModuleType.FormattingEnabled = true;
            this.comboBoxModuleType.Location = new System.Drawing.Point(121, 133);
            this.comboBoxModuleType.Name = "comboBoxModuleType";
            this.comboBoxModuleType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxModuleType.TabIndex = 26;
            // 
            // labelModuleType
            // 
            this.labelModuleType.AutoSize = true;
            this.labelModuleType.Location = new System.Drawing.Point(21, 142);
            this.labelModuleType.Name = "labelModuleType";
            this.labelModuleType.Size = new System.Drawing.Size(69, 13);
            this.labelModuleType.TabIndex = 25;
            this.labelModuleType.Text = "Module Type";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(121, 196);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 27;
            this.buttonSave.Text = "Kaydet";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // AddFrameworkModuleDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.comboBoxModuleType);
            this.Controls.Add(this.labelModuleType);
            this.Controls.Add(this.textBoxDesc);
            this.Controls.Add(this.labelModuleDesc);
            this.Controls.Add(this.textBoxModuleName);
            this.Controls.Add(this.labelModuleName);
            this.Name = "AddFrameworkModuleDialog";
            this.Text = "AddFrameworkModule";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxModuleName;
        private System.Windows.Forms.Label labelModuleName;
        private System.Windows.Forms.TextBox textBoxDesc;
        private System.Windows.Forms.Label labelModuleDesc;
        private System.Windows.Forms.ComboBox comboBoxModuleType;
        private System.Windows.Forms.Label labelModuleType;
        private System.Windows.Forms.Button buttonSave;
    }
}