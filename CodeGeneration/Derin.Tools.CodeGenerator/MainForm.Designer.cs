namespace Derin.Tools.CodeGenerator
{
    partial class MainForm
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
            this.modelTreeView = new System.Windows.Forms.TreeView();
            this.buttonLoadModel = new System.Windows.Forms.Button();
            this.buttonGenerateList = new System.Windows.Forms.Button();
            this.checkBoxCSharp = new System.Windows.Forms.CheckBox();
            this.checkBoxAndroid = new System.Windows.Forms.CheckBox();
            this.checkBoxSwift = new System.Windows.Forms.CheckBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelNameSpace = new System.Windows.Forms.Label();
            this.textBoxNameSpace = new System.Windows.Forms.TextBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.buttonGenerateCreate = new System.Windows.Forms.Button();
            this.buttonGenerateUpdate = new System.Windows.Forms.Button();
            this.buttonGenerateGet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // modelTreeView
            // 
            this.modelTreeView.Location = new System.Drawing.Point(12, 52);
            this.modelTreeView.Name = "modelTreeView";
            this.modelTreeView.Size = new System.Drawing.Size(776, 386);
            this.modelTreeView.TabIndex = 0;
            this.modelTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.modelTreeView_AfterCheck);
            this.modelTreeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.modelTreeView_AfterExpand);
            // 
            // buttonLoadModel
            // 
            this.buttonLoadModel.Location = new System.Drawing.Point(13, 13);
            this.buttonLoadModel.Name = "buttonLoadModel";
            this.buttonLoadModel.Size = new System.Drawing.Size(120, 23);
            this.buttonLoadModel.TabIndex = 1;
            this.buttonLoadModel.Text = "Load Model";
            this.buttonLoadModel.UseVisualStyleBackColor = true;
            this.buttonLoadModel.Click += new System.EventHandler(this.buttonLoadModel_Click);
            // 
            // buttonGenerateList
            // 
            this.buttonGenerateList.Location = new System.Drawing.Point(295, 460);
            this.buttonGenerateList.Name = "buttonGenerateList";
            this.buttonGenerateList.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonGenerateList.Size = new System.Drawing.Size(120, 23);
            this.buttonGenerateList.TabIndex = 2;
            this.buttonGenerateList.Text = "Generate List";
            this.buttonGenerateList.UseVisualStyleBackColor = true;
            this.buttonGenerateList.Click += new System.EventHandler(this.buttonGenerateList_Click);
            // 
            // checkBoxCSharp
            // 
            this.checkBoxCSharp.AutoSize = true;
            this.checkBoxCSharp.Location = new System.Drawing.Point(243, 19);
            this.checkBoxCSharp.Name = "checkBoxCSharp";
            this.checkBoxCSharp.Size = new System.Drawing.Size(40, 17);
            this.checkBoxCSharp.TabIndex = 6;
            this.checkBoxCSharp.Text = "C#";
            this.checkBoxCSharp.UseVisualStyleBackColor = true;
            // 
            // checkBoxAndroid
            // 
            this.checkBoxAndroid.AutoSize = true;
            this.checkBoxAndroid.Location = new System.Drawing.Point(324, 19);
            this.checkBoxAndroid.Name = "checkBoxAndroid";
            this.checkBoxAndroid.Size = new System.Drawing.Size(62, 17);
            this.checkBoxAndroid.TabIndex = 7;
            this.checkBoxAndroid.Text = "Android";
            this.checkBoxAndroid.UseVisualStyleBackColor = true;
            // 
            // checkBoxSwift
            // 
            this.checkBoxSwift.AutoSize = true;
            this.checkBoxSwift.Location = new System.Drawing.Point(430, 19);
            this.checkBoxSwift.Name = "checkBoxSwift";
            this.checkBoxSwift.Size = new System.Drawing.Size(49, 17);
            this.checkBoxSwift.TabIndex = 8;
            this.checkBoxSwift.Text = "Swift";
            this.checkBoxSwift.UseVisualStyleBackColor = true;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(919, 55);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(195, 20);
            this.textBoxName.TabIndex = 9;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(828, 58);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(35, 13);
            this.labelName.TabIndex = 10;
            this.labelName.Text = "Name";
            // 
            // labelNameSpace
            // 
            this.labelNameSpace.AutoSize = true;
            this.labelNameSpace.Location = new System.Drawing.Point(828, 122);
            this.labelNameSpace.Name = "labelNameSpace";
            this.labelNameSpace.Size = new System.Drawing.Size(66, 13);
            this.labelNameSpace.TabIndex = 12;
            this.labelNameSpace.Text = "NameSpace";
            // 
            // textBoxNameSpace
            // 
            this.textBoxNameSpace.Location = new System.Drawing.Point(919, 119);
            this.textBoxNameSpace.Name = "textBoxNameSpace";
            this.textBoxNameSpace.Size = new System.Drawing.Size(195, 20);
            this.textBoxNameSpace.TabIndex = 11;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(828, 178);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(27, 13);
            this.labelTitle.TabIndex = 14;
            this.labelTitle.Text = "Title";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(919, 175);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(195, 20);
            this.textBoxTitle.TabIndex = 13;
            // 
            // buttonGenerateCreate
            // 
            this.buttonGenerateCreate.Location = new System.Drawing.Point(13, 460);
            this.buttonGenerateCreate.Name = "buttonGenerateCreate";
            this.buttonGenerateCreate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonGenerateCreate.Size = new System.Drawing.Size(120, 23);
            this.buttonGenerateCreate.TabIndex = 15;
            this.buttonGenerateCreate.Text = "Generate Create";
            this.buttonGenerateCreate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonGenerateCreate.UseVisualStyleBackColor = true;
            this.buttonGenerateCreate.Click += new System.EventHandler(this.buttonGenerateCreate_Click);
            // 
            // buttonGenerateUpdate
            // 
            this.buttonGenerateUpdate.Location = new System.Drawing.Point(154, 460);
            this.buttonGenerateUpdate.Name = "buttonGenerateUpdate";
            this.buttonGenerateUpdate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonGenerateUpdate.Size = new System.Drawing.Size(120, 23);
            this.buttonGenerateUpdate.TabIndex = 16;
            this.buttonGenerateUpdate.Text = "Generate Update";
            this.buttonGenerateUpdate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonGenerateUpdate.UseVisualStyleBackColor = true;
            this.buttonGenerateUpdate.Click += new System.EventHandler(this.buttonGenerateUpdate_Click);
            // 
            // buttonGenerateGet
            // 
            this.buttonGenerateGet.Location = new System.Drawing.Point(430, 461);
            this.buttonGenerateGet.Name = "buttonGenerateGet";
            this.buttonGenerateGet.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonGenerateGet.Size = new System.Drawing.Size(120, 23);
            this.buttonGenerateGet.TabIndex = 17;
            this.buttonGenerateGet.Text = "Generate Get";
            this.buttonGenerateGet.UseVisualStyleBackColor = true;
            this.buttonGenerateGet.Click += new System.EventHandler(this.buttonGenerateGet_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 508);
            this.Controls.Add(this.buttonGenerateGet);
            this.Controls.Add(this.buttonGenerateUpdate);
            this.Controls.Add(this.buttonGenerateCreate);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.labelNameSpace);
            this.Controls.Add(this.textBoxNameSpace);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.checkBoxSwift);
            this.Controls.Add(this.checkBoxAndroid);
            this.Controls.Add(this.checkBoxCSharp);
            this.Controls.Add(this.buttonGenerateList);
            this.Controls.Add(this.buttonLoadModel);
            this.Controls.Add(this.modelTreeView);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView modelTreeView;
        private System.Windows.Forms.Button buttonLoadModel;
        private System.Windows.Forms.Button buttonGenerateList;
        private System.Windows.Forms.CheckBox checkBoxCSharp;
        private System.Windows.Forms.CheckBox checkBoxAndroid;
        private System.Windows.Forms.CheckBox checkBoxSwift;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelNameSpace;
        private System.Windows.Forms.TextBox textBoxNameSpace;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Button buttonGenerateCreate;
        private System.Windows.Forms.Button buttonGenerateUpdate;
        private System.Windows.Forms.Button buttonGenerateGet;
    }
}

