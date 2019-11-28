namespace IF.Tools.CodeGenerator
{
    partial class ApiGeneratorBaseForm
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
            this.labelRepositoryName = new System.Windows.Forms.Label();
            this.textBoxRepositoryName = new System.Windows.Forms.TextBox();
            this.labelControllerName = new System.Windows.Forms.Label();
            this.textBoxControllerName = new System.Windows.Forms.TextBox();
            this.checkedListBoxVsFiles = new System.Windows.Forms.CheckedListBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelRepositoryName
            // 
            this.labelRepositoryName.AutoSize = true;
            this.labelRepositoryName.Location = new System.Drawing.Point(271, 64);
            this.labelRepositoryName.Name = "labelRepositoryName";
            this.labelRepositoryName.Size = new System.Drawing.Size(88, 13);
            this.labelRepositoryName.TabIndex = 26;
            this.labelRepositoryName.Text = "Repository Name";
            // 
            // textBoxRepositoryName
            // 
            this.textBoxRepositoryName.Location = new System.Drawing.Point(362, 61);
            this.textBoxRepositoryName.Name = "textBoxRepositoryName";
            this.textBoxRepositoryName.Size = new System.Drawing.Size(195, 20);
            this.textBoxRepositoryName.TabIndex = 25;
            // 
            // labelControllerName
            // 
            this.labelControllerName.AutoSize = true;
            this.labelControllerName.Location = new System.Drawing.Point(271, 15);
            this.labelControllerName.Name = "labelControllerName";
            this.labelControllerName.Size = new System.Drawing.Size(82, 13);
            this.labelControllerName.TabIndex = 24;
            this.labelControllerName.Text = "Controller Name";
            // 
            // textBoxControllerName
            // 
            this.textBoxControllerName.Location = new System.Drawing.Point(362, 12);
            this.textBoxControllerName.Name = "textBoxControllerName";
            this.textBoxControllerName.Size = new System.Drawing.Size(195, 20);
            this.textBoxControllerName.TabIndex = 23;
            // 
            // checkedListBoxVsFiles
            // 
            this.checkedListBoxVsFiles.FormattingEnabled = true;
            this.checkedListBoxVsFiles.Location = new System.Drawing.Point(12, 12);
            this.checkedListBoxVsFiles.Name = "checkedListBoxVsFiles";
            this.checkedListBoxVsFiles.Size = new System.Drawing.Size(225, 199);
            this.checkedListBoxVsFiles.TabIndex = 22;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(668, 348);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerate.TabIndex = 21;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // ApiGeneratorBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelRepositoryName);
            this.Controls.Add(this.textBoxRepositoryName);
            this.Controls.Add(this.labelControllerName);
            this.Controls.Add(this.textBoxControllerName);
            this.Controls.Add(this.checkedListBoxVsFiles);
            this.Controls.Add(this.buttonGenerate);
            this.Name = "ApiGeneratorBaseForm";
            this.Text = "ApiGeneratorBaseForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelRepositoryName;
        private System.Windows.Forms.TextBox textBoxRepositoryName;
        private System.Windows.Forms.Label labelControllerName;
        private System.Windows.Forms.TextBox textBoxControllerName;
        private System.Windows.Forms.CheckedListBox checkedListBoxVsFiles;
        private System.Windows.Forms.Button buttonGenerate;
    }
}