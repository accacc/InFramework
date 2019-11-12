namespace DTOGenerator
{
    partial class GridGenerator
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
            this.treeViewColumnPicker = new System.Windows.Forms.TreeView();
            this.GenerateSpButton = new System.Windows.Forms.Button();
            this.treeViewParamPicker = new System.Windows.Forms.TreeView();
            this.textBoxControllerName = new System.Windows.Forms.TextBox();
            this.labelControllerName = new System.Windows.Forms.Label();
            this.labelActionName = new System.Windows.Forms.Label();
            this.textBoxActionName = new System.Windows.Forms.TextBox();
            this.labelColumns = new System.Windows.Forms.Label();
            this.labelParameters = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // treeViewColumnPicker
            // 
            this.treeViewColumnPicker.CheckBoxes = true;
            this.treeViewColumnPicker.Location = new System.Drawing.Point(27, 42);
            this.treeViewColumnPicker.Name = "treeViewColumnPicker";
            this.treeViewColumnPicker.Size = new System.Drawing.Size(352, 404);
            this.treeViewColumnPicker.TabIndex = 2;
            // 
            // GenerateSpButton
            // 
            this.GenerateSpButton.Location = new System.Drawing.Point(871, 481);
            this.GenerateSpButton.Name = "GenerateSpButton";
            this.GenerateSpButton.Size = new System.Drawing.Size(75, 23);
            this.GenerateSpButton.TabIndex = 3;
            this.GenerateSpButton.Text = "Generate SP";
            this.GenerateSpButton.UseVisualStyleBackColor = true;
            this.GenerateSpButton.Click += new System.EventHandler(this.GenerateSpButton_Click);
            // 
            // treeViewParamPicker
            // 
            this.treeViewParamPicker.CheckBoxes = true;
            this.treeViewParamPicker.Location = new System.Drawing.Point(423, 42);
            this.treeViewParamPicker.Name = "treeViewParamPicker";
            this.treeViewParamPicker.Size = new System.Drawing.Size(352, 404);
            this.treeViewParamPicker.TabIndex = 4;
            // 
            // textBoxControllerName
            // 
            this.textBoxControllerName.Location = new System.Drawing.Point(982, 35);
            this.textBoxControllerName.Name = "textBoxControllerName";
            this.textBoxControllerName.Size = new System.Drawing.Size(178, 20);
            this.textBoxControllerName.TabIndex = 5;
            // 
            // labelControllerName
            // 
            this.labelControllerName.AutoSize = true;
            this.labelControllerName.Location = new System.Drawing.Point(808, 42);
            this.labelControllerName.Name = "labelControllerName";
            this.labelControllerName.Size = new System.Drawing.Size(82, 13);
            this.labelControllerName.TabIndex = 6;
            this.labelControllerName.Text = "Controller Name";
            // 
            // labelActionName
            // 
            this.labelActionName.AutoSize = true;
            this.labelActionName.Location = new System.Drawing.Point(808, 77);
            this.labelActionName.Name = "labelActionName";
            this.labelActionName.Size = new System.Drawing.Size(68, 13);
            this.labelActionName.TabIndex = 8;
            this.labelActionName.Text = "Action Name";
            // 
            // textBoxActionName
            // 
            this.textBoxActionName.Location = new System.Drawing.Point(982, 70);
            this.textBoxActionName.Name = "textBoxActionName";
            this.textBoxActionName.Size = new System.Drawing.Size(178, 20);
            this.textBoxActionName.TabIndex = 7;
            // 
            // labelColumns
            // 
            this.labelColumns.AutoSize = true;
            this.labelColumns.Location = new System.Drawing.Point(150, 9);
            this.labelColumns.Name = "labelColumns";
            this.labelColumns.Size = new System.Drawing.Size(47, 13);
            this.labelColumns.TabIndex = 9;
            this.labelColumns.Text = "Columns";
            // 
            // labelParameters
            // 
            this.labelParameters.AutoSize = true;
            this.labelParameters.Location = new System.Drawing.Point(530, 9);
            this.labelParameters.Name = "labelParameters";
            this.labelParameters.Size = new System.Drawing.Size(60, 13);
            this.labelParameters.TabIndex = 10;
            this.labelParameters.Text = "Parameters";
            // 
            // SpGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1199, 560);
            this.Controls.Add(this.labelParameters);
            this.Controls.Add(this.labelColumns);
            this.Controls.Add(this.labelActionName);
            this.Controls.Add(this.textBoxActionName);
            this.Controls.Add(this.labelControllerName);
            this.Controls.Add(this.textBoxControllerName);
            this.Controls.Add(this.treeViewParamPicker);
            this.Controls.Add(this.GenerateSpButton);
            this.Controls.Add(this.treeViewColumnPicker);
            this.Name = "SpGenerator";
            this.Text = "SpGenerator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SpGenerator_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewColumnPicker;
        private System.Windows.Forms.Button GenerateSpButton;
        private System.Windows.Forms.TreeView treeViewParamPicker;
        private System.Windows.Forms.TextBox textBoxControllerName;
        private System.Windows.Forms.Label labelControllerName;
        private System.Windows.Forms.Label labelActionName;
        private System.Windows.Forms.TextBox textBoxActionName;
        private System.Windows.Forms.Label labelColumns;
        private System.Windows.Forms.Label labelParameters;
    }
}