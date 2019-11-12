namespace FOFramework.CodeGeneration.Oracle.Dto
{
    partial class SpParameterForm
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
            this.textBoxParameters = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxParameters
            // 
            this.textBoxParameters.Location = new System.Drawing.Point(40, 13);
            this.textBoxParameters.Multiline = true;
            this.textBoxParameters.Name = "textBoxParameters";
            this.textBoxParameters.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxParameters.Size = new System.Drawing.Size(759, 362);
            this.textBoxParameters.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(704, 394);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SpParameterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 441);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxParameters);
            this.Name = "SpParameterForm";
            this.Text = "SpParameterForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SpParameterForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxParameters;
        private System.Windows.Forms.Button button1;
    }
}