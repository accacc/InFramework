namespace IF.Tools.Templates.Editor
{
    partial class WebApiTemplateDialog
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
            this.labelDatabase = new System.Windows.Forms.Label();
            this.comboBoxDatabase = new System.Windows.Forms.ComboBox();
            this.comboBoxOrm = new System.Windows.Forms.ComboBox();
            this.labelOrm = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxLogType = new System.Windows.Forms.ComboBox();
            this.labelLogType = new System.Windows.Forms.Label();
            this.comboBoxMessageBroker = new System.Windows.Forms.ComboBox();
            this.labelMessageBroker = new System.Windows.Forms.Label();
            this.comboBoxServiceBus = new System.Windows.Forms.ComboBox();
            this.labelServiceBus = new System.Windows.Forms.Label();
            this.labelEventBus = new System.Windows.Forms.Label();
            this.bindingSourceDatabase = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceOrm = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceMessageBroker = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceServiceBus = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceLogType = new System.Windows.Forms.BindingSource(this.components);
            this.checkBoxSwagger = new System.Windows.Forms.CheckBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.clbFolders = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSolutionName = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDatabase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceOrm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMessageBroker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceServiceBus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceLogType)).BeginInit();
            this.SuspendLayout();
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Location = new System.Drawing.Point(17, 28);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(53, 13);
            this.labelDatabase.TabIndex = 2;
            this.labelDatabase.Text = "Database";
            this.labelDatabase.Click += new System.EventHandler(this.labelTemplateName_Click);
            // 
            // comboBoxDatabase
            // 
            this.comboBoxDatabase.FormattingEnabled = true;
            this.comboBoxDatabase.Location = new System.Drawing.Point(105, 25);
            this.comboBoxDatabase.Name = "comboBoxDatabase";
            this.comboBoxDatabase.Size = new System.Drawing.Size(205, 21);
            this.comboBoxDatabase.TabIndex = 3;
            // 
            // comboBoxOrm
            // 
            this.comboBoxOrm.FormattingEnabled = true;
            this.comboBoxOrm.Location = new System.Drawing.Point(105, 63);
            this.comboBoxOrm.Name = "comboBoxOrm";
            this.comboBoxOrm.Size = new System.Drawing.Size(205, 21);
            this.comboBoxOrm.TabIndex = 5;
            // 
            // labelOrm
            // 
            this.labelOrm.AutoSize = true;
            this.labelOrm.Location = new System.Drawing.Point(17, 66);
            this.labelOrm.Name = "labelOrm";
            this.labelOrm.Size = new System.Drawing.Size(26, 13);
            this.labelOrm.TabIndex = 4;
            this.labelOrm.Text = "Orm";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBoxLogType);
            this.panel1.Controls.Add(this.labelLogType);
            this.panel1.Controls.Add(this.comboBoxMessageBroker);
            this.panel1.Controls.Add(this.labelMessageBroker);
            this.panel1.Controls.Add(this.comboBoxServiceBus);
            this.panel1.Controls.Add(this.labelServiceBus);
            this.panel1.Location = new System.Drawing.Point(36, 147);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(331, 216);
            this.panel1.TabIndex = 6;
            // 
            // comboBoxLogType
            // 
            this.comboBoxLogType.FormattingEnabled = true;
            this.comboBoxLogType.Location = new System.Drawing.Point(107, 98);
            this.comboBoxLogType.Name = "comboBoxLogType";
            this.comboBoxLogType.Size = new System.Drawing.Size(205, 21);
            this.comboBoxLogType.TabIndex = 11;
            // 
            // labelLogType
            // 
            this.labelLogType.AutoSize = true;
            this.labelLogType.Location = new System.Drawing.Point(19, 101);
            this.labelLogType.Name = "labelLogType";
            this.labelLogType.Size = new System.Drawing.Size(52, 13);
            this.labelLogType.TabIndex = 10;
            this.labelLogType.Text = "Log Type";
            // 
            // comboBoxMessageBroker
            // 
            this.comboBoxMessageBroker.FormattingEnabled = true;
            this.comboBoxMessageBroker.Location = new System.Drawing.Point(107, 65);
            this.comboBoxMessageBroker.Name = "comboBoxMessageBroker";
            this.comboBoxMessageBroker.Size = new System.Drawing.Size(205, 21);
            this.comboBoxMessageBroker.TabIndex = 9;
            // 
            // labelMessageBroker
            // 
            this.labelMessageBroker.AutoSize = true;
            this.labelMessageBroker.Location = new System.Drawing.Point(19, 68);
            this.labelMessageBroker.Name = "labelMessageBroker";
            this.labelMessageBroker.Size = new System.Drawing.Size(84, 13);
            this.labelMessageBroker.TabIndex = 8;
            this.labelMessageBroker.Text = "Message Broker";
            // 
            // comboBoxServiceBus
            // 
            this.comboBoxServiceBus.FormattingEnabled = true;
            this.comboBoxServiceBus.Location = new System.Drawing.Point(107, 27);
            this.comboBoxServiceBus.Name = "comboBoxServiceBus";
            this.comboBoxServiceBus.Size = new System.Drawing.Size(205, 21);
            this.comboBoxServiceBus.TabIndex = 7;
            // 
            // labelServiceBus
            // 
            this.labelServiceBus.AutoSize = true;
            this.labelServiceBus.Location = new System.Drawing.Point(19, 30);
            this.labelServiceBus.Name = "labelServiceBus";
            this.labelServiceBus.Size = new System.Drawing.Size(64, 13);
            this.labelServiceBus.TabIndex = 6;
            this.labelServiceBus.Text = "Service Bus";
            // 
            // labelEventBus
            // 
            this.labelEventBus.AutoSize = true;
            this.labelEventBus.Location = new System.Drawing.Point(17, 112);
            this.labelEventBus.Name = "labelEventBus";
            this.labelEventBus.Size = new System.Drawing.Size(56, 13);
            this.labelEventBus.TabIndex = 7;
            this.labelEventBus.Text = "Event Bus";
            // 
            // checkBoxSwagger
            // 
            this.checkBoxSwagger.AutoSize = true;
            this.checkBoxSwagger.Location = new System.Drawing.Point(488, 32);
            this.checkBoxSwagger.Name = "checkBoxSwagger";
            this.checkBoxSwagger.Size = new System.Drawing.Size(90, 17);
            this.checkBoxSwagger.TabIndex = 9;
            this.checkBoxSwagger.Text = "Use Swagger";
            this.checkBoxSwagger.UseVisualStyleBackColor = true;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(686, 339);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerate.TabIndex = 10;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // clbFolders
            // 
            this.clbFolders.CheckOnClick = true;
            this.clbFolders.FormattingEnabled = true;
            this.clbFolders.Location = new System.Drawing.Point(418, 63);
            this.clbFolders.Name = "clbFolders";
            this.clbFolders.Size = new System.Drawing.Size(343, 259);
            this.clbFolders.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(427, 393);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Solution Name";
            // 
            // textBoxSolutionName
            // 
            this.textBoxSolutionName.Location = new System.Drawing.Point(543, 386);
            this.textBoxSolutionName.Name = "textBoxSolutionName";
            this.textBoxSolutionName.Size = new System.Drawing.Size(218, 20);
            this.textBoxSolutionName.TabIndex = 13;
            this.textBoxSolutionName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // WebApiTemplateDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBoxSolutionName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clbFolders);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.checkBoxSwagger);
            this.Controls.Add(this.labelEventBus);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.comboBoxOrm);
            this.Controls.Add(this.labelOrm);
            this.Controls.Add(this.comboBoxDatabase);
            this.Controls.Add(this.labelDatabase);
            this.Name = "WebApiTemplateDialog";
            this.Text = "WebApiTemplateDialog";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDatabase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceOrm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMessageBroker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceServiceBus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceLogType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource bindingSourceDatabase;
        private System.Windows.Forms.Label labelDatabase;
        private System.Windows.Forms.ComboBox comboBoxDatabase;
        private System.Windows.Forms.ComboBox comboBoxOrm;
        private System.Windows.Forms.Label labelOrm;
        private System.Windows.Forms.BindingSource bindingSourceOrm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelEventBus;
        private System.Windows.Forms.ComboBox comboBoxMessageBroker;
        private System.Windows.Forms.Label labelMessageBroker;
        private System.Windows.Forms.ComboBox comboBoxServiceBus;
        private System.Windows.Forms.Label labelServiceBus;
        private System.Windows.Forms.ComboBox comboBoxLogType;
        private System.Windows.Forms.Label labelLogType;
        private System.Windows.Forms.BindingSource bindingSourceMessageBroker;
        private System.Windows.Forms.BindingSource bindingSourceServiceBus;
        private System.Windows.Forms.BindingSource bindingSourceLogType;
        private System.Windows.Forms.CheckBox checkBoxSwagger;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.CheckedListBox clbFolders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSolutionName;
    }
}