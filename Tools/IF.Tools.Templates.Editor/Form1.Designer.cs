namespace IF.Tools.Templates.Editor
{
    partial class Form1
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
            this.labelTemplateName = new System.Windows.Forms.Label();
            this.comboBoxTemplates = new System.Windows.Forms.ComboBox();
            this.templateTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonSelectTemplate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.templateTypeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTemplateName
            // 
            this.labelTemplateName.AutoSize = true;
            this.labelTemplateName.Location = new System.Drawing.Point(31, 38);
            this.labelTemplateName.Name = "labelTemplateName";
            this.labelTemplateName.Size = new System.Drawing.Size(82, 13);
            this.labelTemplateName.TabIndex = 0;
            this.labelTemplateName.Text = "Template Name";
            // 
            // comboBoxTemplates
            // 
            this.comboBoxTemplates.FormattingEnabled = true;
            this.comboBoxTemplates.Location = new System.Drawing.Point(119, 35);
            this.comboBoxTemplates.Name = "comboBoxTemplates";
            this.comboBoxTemplates.Size = new System.Drawing.Size(205, 21);
            this.comboBoxTemplates.TabIndex = 1;
            // 
            // buttonSelectTemplate
            // 
            this.buttonSelectTemplate.Location = new System.Drawing.Point(369, 35);
            this.buttonSelectTemplate.Name = "buttonSelectTemplate";
            this.buttonSelectTemplate.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectTemplate.TabIndex = 2;
            this.buttonSelectTemplate.Text = "Select";
            this.buttonSelectTemplate.UseVisualStyleBackColor = true;
            this.buttonSelectTemplate.Click += new System.EventHandler(this.buttonSelectTemplate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 135);
            this.Controls.Add(this.buttonSelectTemplate);
            this.Controls.Add(this.comboBoxTemplates);
            this.Controls.Add(this.labelTemplateName);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.templateTypeBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTemplateName;
        private System.Windows.Forms.ComboBox comboBoxTemplates;
        private System.Windows.Forms.BindingSource templateTypeBindingSource;
        private System.Windows.Forms.Button buttonSelectTemplate;
    }
}

