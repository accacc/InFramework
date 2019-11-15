namespace IF.Tools.CodeGenerator
{
    partial class ListGeneratorForm
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
            // ListGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.checkedListBoxVsFiles);
            this.Controls.Add(this.buttonGenerate);
            this.Name = "ListGeneratorForm";
            this.Text = "ListGenerator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.CheckedListBox checkedListBoxVsFiles;
    }
}