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
            this.labelEventBus = new System.Windows.Forms.Label();
            this.labelServiceBus = new System.Windows.Forms.Label();
            this.bindingSourceDatabase = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceOrm = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceMessageBroker = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceServiceBus = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceLogType = new System.Windows.Forms.BindingSource(this.components);
            this.checkBoxSwagger = new System.Windows.Forms.CheckBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.checkBoxListProjects = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSolutionName = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxApiName = new System.Windows.Forms.TextBox();
            this.labelApiName = new System.Windows.Forms.Label();
            this.textBoxEventBusName = new System.Windows.Forms.TextBox();
            this.labelEventBusName = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDatabase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceOrm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMessageBroker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceServiceBus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceLogType)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Location = new System.Drawing.Point(26, 64);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(87, 13);
            this.labelDatabase.TabIndex = 2;
            this.labelDatabase.Text = "Database Server";
            // 
            // comboBoxDatabase
            // 
            this.comboBoxDatabase.FormattingEnabled = true;
            this.comboBoxDatabase.Location = new System.Drawing.Point(114, 61);
            this.comboBoxDatabase.Name = "comboBoxDatabase";
            this.comboBoxDatabase.Size = new System.Drawing.Size(205, 21);
            this.comboBoxDatabase.TabIndex = 3;
            // 
            // comboBoxOrm
            // 
            this.comboBoxOrm.FormattingEnabled = true;
            this.comboBoxOrm.Location = new System.Drawing.Point(114, 99);
            this.comboBoxOrm.Name = "comboBoxOrm";
            this.comboBoxOrm.Size = new System.Drawing.Size(205, 21);
            this.comboBoxOrm.TabIndex = 5;
            // 
            // labelOrm
            // 
            this.labelOrm.AutoSize = true;
            this.labelOrm.Location = new System.Drawing.Point(26, 102);
            this.labelOrm.Name = "labelOrm";
            this.labelOrm.Size = new System.Drawing.Size(26, 13);
            this.labelOrm.TabIndex = 4;
            this.labelOrm.Text = "Orm";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBoxEventBusName);
            this.panel1.Controls.Add(this.labelEventBusName);
            this.panel1.Controls.Add(this.comboBoxLogType);
            this.panel1.Controls.Add(this.labelLogType);
            this.panel1.Controls.Add(this.comboBoxMessageBroker);
            this.panel1.Controls.Add(this.labelMessageBroker);
            this.panel1.Controls.Add(this.comboBoxServiceBus);
            this.panel1.Controls.Add(this.labelEventBus);
            this.panel1.Controls.Add(this.labelServiceBus);
            this.panel1.Location = new System.Drawing.Point(30, 307);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(331, 227);
            this.panel1.TabIndex = 6;
            // 
            // comboBoxLogType
            // 
            this.comboBoxLogType.FormattingEnabled = true;
            this.comboBoxLogType.Location = new System.Drawing.Point(103, 140);
            this.comboBoxLogType.Name = "comboBoxLogType";
            this.comboBoxLogType.Size = new System.Drawing.Size(216, 21);
            this.comboBoxLogType.TabIndex = 11;
            // 
            // labelLogType
            // 
            this.labelLogType.AutoSize = true;
            this.labelLogType.Location = new System.Drawing.Point(17, 143);
            this.labelLogType.Name = "labelLogType";
            this.labelLogType.Size = new System.Drawing.Size(52, 13);
            this.labelLogType.TabIndex = 10;
            this.labelLogType.Text = "Log Type";
            // 
            // comboBoxMessageBroker
            // 
            this.comboBoxMessageBroker.FormattingEnabled = true;
            this.comboBoxMessageBroker.Location = new System.Drawing.Point(103, 107);
            this.comboBoxMessageBroker.Name = "comboBoxMessageBroker";
            this.comboBoxMessageBroker.Size = new System.Drawing.Size(216, 21);
            this.comboBoxMessageBroker.TabIndex = 9;
            // 
            // labelMessageBroker
            // 
            this.labelMessageBroker.AutoSize = true;
            this.labelMessageBroker.Location = new System.Drawing.Point(17, 110);
            this.labelMessageBroker.Name = "labelMessageBroker";
            this.labelMessageBroker.Size = new System.Drawing.Size(84, 13);
            this.labelMessageBroker.TabIndex = 8;
            this.labelMessageBroker.Text = "Message Broker";
            // 
            // comboBoxServiceBus
            // 
            this.comboBoxServiceBus.FormattingEnabled = true;
            this.comboBoxServiceBus.Location = new System.Drawing.Point(103, 69);
            this.comboBoxServiceBus.Name = "comboBoxServiceBus";
            this.comboBoxServiceBus.Size = new System.Drawing.Size(216, 21);
            this.comboBoxServiceBus.TabIndex = 7;
            // 
            // labelEventBus
            // 
            this.labelEventBus.AutoSize = true;
            this.labelEventBus.Location = new System.Drawing.Point(111, 16);
            this.labelEventBus.Name = "labelEventBus";
            this.labelEventBus.Size = new System.Drawing.Size(56, 13);
            this.labelEventBus.TabIndex = 7;
            this.labelEventBus.Text = "Event Bus";
            // 
            // labelServiceBus
            // 
            this.labelServiceBus.AutoSize = true;
            this.labelServiceBus.Location = new System.Drawing.Point(17, 72);
            this.labelServiceBus.Name = "labelServiceBus";
            this.labelServiceBus.Size = new System.Drawing.Size(64, 13);
            this.labelServiceBus.TabIndex = 6;
            this.labelServiceBus.Text = "Service Bus";
            // 
            // checkBoxSwagger
            // 
            this.checkBoxSwagger.AutoSize = true;
            this.checkBoxSwagger.Location = new System.Drawing.Point(1145, 418);
            this.checkBoxSwagger.Name = "checkBoxSwagger";
            this.checkBoxSwagger.Size = new System.Drawing.Size(90, 17);
            this.checkBoxSwagger.TabIndex = 9;
            this.checkBoxSwagger.Text = "Use Swagger";
            this.checkBoxSwagger.UseVisualStyleBackColor = true;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(1160, 498);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerate.TabIndex = 10;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // checkBoxListProjects
            // 
            this.checkBoxListProjects.CheckOnClick = true;
            this.checkBoxListProjects.FormattingEnabled = true;
            this.checkBoxListProjects.Location = new System.Drawing.Point(445, 46);
            this.checkBoxListProjects.Name = "checkBoxListProjects";
            this.checkBoxListProjects.Size = new System.Drawing.Size(343, 484);
            this.checkBoxListProjects.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(878, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Solution Name";
            // 
            // textBoxSolutionName
            // 
            this.textBoxSolutionName.Location = new System.Drawing.Point(1017, 46);
            this.textBoxSolutionName.Name = "textBoxSolutionName";
            this.textBoxSolutionName.Size = new System.Drawing.Size(218, 20);
            this.textBoxSolutionName.TabIndex = 13;
            this.textBoxSolutionName.Text = "Test.SolutionName";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.comboBoxDatabase);
            this.panel2.Controls.Add(this.labelDatabase);
            this.panel2.Controls.Add(this.labelOrm);
            this.panel2.Controls.Add(this.comboBoxOrm);
            this.panel2.Location = new System.Drawing.Point(30, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(340, 188);
            this.panel2.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Database";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(442, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Projects";
            // 
            // textBoxApiName
            // 
            this.textBoxApiName.Location = new System.Drawing.Point(1017, 105);
            this.textBoxApiName.Name = "textBoxApiName";
            this.textBoxApiName.Size = new System.Drawing.Size(218, 20);
            this.textBoxApiName.TabIndex = 17;
            this.textBoxApiName.Text = "Test Api ";
            // 
            // labelApiName
            // 
            this.labelApiName.AutoSize = true;
            this.labelApiName.Location = new System.Drawing.Point(878, 105);
            this.labelApiName.Name = "labelApiName";
            this.labelApiName.Size = new System.Drawing.Size(53, 13);
            this.labelApiName.TabIndex = 16;
            this.labelApiName.Text = "Api Name";
            // 
            // textBoxEventBusName
            // 
            this.textBoxEventBusName.Location = new System.Drawing.Point(103, 176);
            this.textBoxEventBusName.Name = "textBoxEventBusName";
            this.textBoxEventBusName.Size = new System.Drawing.Size(216, 20);
            this.textBoxEventBusName.TabIndex = 19;
            this.textBoxEventBusName.Text = "test_eventbus";
            // 
            // labelEventBusName
            // 
            this.labelEventBusName.AutoSize = true;
            this.labelEventBusName.Location = new System.Drawing.Point(17, 179);
            this.labelEventBusName.Name = "labelEventBusName";
            this.labelEventBusName.Size = new System.Drawing.Size(87, 13);
            this.labelEventBusName.TabIndex = 18;
            this.labelEventBusName.Text = "Event Bus Name";
            // 
            // WebApiTemplateDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1761, 715);
            this.Controls.Add(this.textBoxApiName);
            this.Controls.Add(this.labelApiName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.textBoxSolutionName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxListProjects);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.checkBoxSwagger);
            this.Controls.Add(this.panel1);
            this.Name = "WebApiTemplateDialog";
            this.Text = "WebApiTemplateDialog";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDatabase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceOrm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMessageBroker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceServiceBus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceLogType)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.CheckedListBox checkBoxListProjects;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSolutionName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxApiName;
        private System.Windows.Forms.Label labelApiName;
        private System.Windows.Forms.TextBox textBoxEventBusName;
        private System.Windows.Forms.Label labelEventBusName;
    }
}