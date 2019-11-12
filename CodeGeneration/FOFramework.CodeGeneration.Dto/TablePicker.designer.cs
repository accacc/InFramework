namespace DTOGenerator
{
    partial class TablePicker
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
            this.panel1 = new System.Windows.Forms.Panel();
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRadioButton = new System.Windows.Forms.RadioButton();
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.treeViewTableName = new System.Windows.Forms.TreeView();
            this.radioGenerateFormButton = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioGenerateFormButton);
            this.panel1.Controls.Add(this.gridRadioButton);
            this.panel1.Controls.Add(this.buttonSelectAll);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.treeViewTableName);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1098, 527);
            this.panel1.TabIndex = 2;
            // 
            // gridRadioButton
            // 
            this.gridRadioButton.AutoSize = true;
            this.gridRadioButton.Location = new System.Drawing.Point(79, 463);
            this.gridRadioButton.Name = "gridRadioButton";
            this.gridRadioButton.Size = new System.Drawing.Size(91, 17);
            this.gridRadioButton.TabIndex = 3;
            this.gridRadioButton.TabStop = true;
            this.gridRadioButton.Text = "Generate Grid";
            this.gridRadioButton.UseVisualStyleBackColor = true;
            // 
            // buttonSelectAll
            // 
            this.buttonSelectAll.Location = new System.Drawing.Point(20, 11);
            this.buttonSelectAll.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(56, 19);
            this.buttonSelectAll.TabIndex = 2;
            this.buttonSelectAll.Text = "Select All";
            this.buttonSelectAll.UseVisualStyleBackColor = true;
            this.buttonSelectAll.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(936, 458);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Generate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonGenerate);
            // 
            // treeViewTableName
            // 
            this.treeViewTableName.CheckBoxes = true;
            this.treeViewTableName.Location = new System.Drawing.Point(20, 36);
            this.treeViewTableName.Name = "treeViewTableName";
            this.treeViewTableName.Size = new System.Drawing.Size(1022, 404);
            this.treeViewTableName.TabIndex = 0;
            this.treeViewTableName.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // radioGenerateFormButton
            // 
            this.radioGenerateFormButton.AutoSize = true;
            this.radioGenerateFormButton.Location = new System.Drawing.Point(195, 463);
            this.radioGenerateFormButton.Name = "radioGenerateFormButton";
            this.radioGenerateFormButton.Size = new System.Drawing.Size(95, 17);
            this.radioGenerateFormButton.TabIndex = 4;
            this.radioGenerateFormButton.TabStop = true;
            this.radioGenerateFormButton.Text = "Generate Form";
            this.radioGenerateFormButton.UseVisualStyleBackColor = true;
            // 
            // TablePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 551);
            this.Controls.Add(this.panel1);
            this.Name = "TablePicker";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TreeView treeViewTableName;
        private System.Windows.Forms.Button buttonSelectAll;
        private System.Windows.Forms.RadioButton gridRadioButton;
        private System.Windows.Forms.RadioButton radioGenerateFormButton;
    }
}

