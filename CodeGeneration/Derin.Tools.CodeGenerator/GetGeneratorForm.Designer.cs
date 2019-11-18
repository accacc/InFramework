namespace IF.Tools.CodeGenerator
{
    partial class GetGeneratorForm
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
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.checkedListBoxVsFiles = new System.Windows.Forms.CheckedListBox();
            this.labelViewBasePath = new System.Windows.Forms.Label();
            this.textBoxViewBasePath = new System.Windows.Forms.TextBox();
            this.labelControllerName = new System.Windows.Forms.Label();
            this.textBoxControllerName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(682, 369);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerate.TabIndex = 0;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // checkedListBoxVsFiles
            // 
            this.checkedListBoxVsFiles.FormattingEnabled = true;
            this.checkedListBoxVsFiles.Location = new System.Drawing.Point(26, 33);
            this.checkedListBoxVsFiles.Name = "checkedListBoxVsFiles";
            this.checkedListBoxVsFiles.Size = new System.Drawing.Size(225, 199);
            this.checkedListBoxVsFiles.TabIndex = 1;
            // 
            // labelViewBasePath
            // 
            this.labelViewBasePath.AutoSize = true;
            this.labelViewBasePath.Location = new System.Drawing.Point(287, 36);
            this.labelViewBasePath.Name = "labelViewBasePath";
            this.labelViewBasePath.Size = new System.Drawing.Size(82, 13);
            this.labelViewBasePath.TabIndex = 16;
            this.labelViewBasePath.Text = "View Base Path";
            // 
            // textBoxViewBasePath
            // 
            this.textBoxViewBasePath.Location = new System.Drawing.Point(378, 33);
            this.textBoxViewBasePath.Name = "textBoxViewBasePath";
            this.textBoxViewBasePath.Size = new System.Drawing.Size(195, 20);
            this.textBoxViewBasePath.TabIndex = 15;
            // 
            // labelControllerName
            // 
            this.labelControllerName.AutoSize = true;
            this.labelControllerName.Location = new System.Drawing.Point(287, 78);
            this.labelControllerName.Name = "labelControllerName";
            this.labelControllerName.Size = new System.Drawing.Size(82, 13);
            this.labelControllerName.TabIndex = 18;
            this.labelControllerName.Text = "Controller Name";
            // 
            // textBoxControllerName
            // 
            this.textBoxControllerName.Location = new System.Drawing.Point(378, 75);
            this.textBoxControllerName.Name = "textBoxControllerName";
            this.textBoxControllerName.Size = new System.Drawing.Size(195, 20);
            this.textBoxControllerName.TabIndex = 17;
            // 
            // ListGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelControllerName);
            this.Controls.Add(this.textBoxControllerName);
            this.Controls.Add(this.labelViewBasePath);
            this.Controls.Add(this.textBoxViewBasePath);
            this.Controls.Add(this.checkedListBoxVsFiles);
            this.Controls.Add(this.buttonGenerate);
            this.Name = "GetGeneratorForm";
            this.Text = "Get Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.CheckedListBox checkedListBoxVsFiles;
        private System.Windows.Forms.Label labelViewBasePath;
        private System.Windows.Forms.TextBox textBoxViewBasePath;
        private System.Windows.Forms.Label labelControllerName;
        private System.Windows.Forms.TextBox textBoxControllerName;
    }
}