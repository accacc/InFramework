using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IF.Tools.Templates.Editor
{
    public partial class WebApiTemplateDialog : Form
    {
        public WebApiTemplateDialog()
        {
            InitializeComponent();
            BindComboBox();
        }


        private void BindComboBox()
        {
            BindDatabases();
            BindOrms();
            BindServiceBuses();
            BindMessageBrokers();
            BindLogTypes();

        }


        private void BindServiceBuses()
        {
            List<NameValueDto> items = new List<NameValueDto>();

            items.Add(new NameValueDto { Name = "Native", Value = "Native" });

            bindingSourceServiceBus.DataSource = items;
            comboBoxServiceBus.DisplayMember = "Name";
            comboBoxServiceBus.ValueMember = "Value";
            comboBoxServiceBus.DataSource = bindingSourceServiceBus;
        }


        private void BindMessageBrokers()
        {
            List<NameValueDto> items = new List<NameValueDto>();

            items.Add(new NameValueDto { Name = "Rabbit MQ", Value = "RabbitMq" });

            bindingSourceMessageBroker.DataSource = items;
            comboBoxMessageBroker.DisplayMember = "Name";
            comboBoxMessageBroker.ValueMember = "Value";
            comboBoxMessageBroker.DataSource = bindingSourceMessageBroker;
        }


        private void BindLogTypes()
        {
            List<NameValueDto> items = new List<NameValueDto>();

            //items.Add(new NameValueDto { Name = "EF", Value = "EF" });
            items.Add(new NameValueDto { Name = "Mongo", Value = "Mongo" });

            bindingSourceLogType.DataSource = items;
            comboBoxLogType.DisplayMember = "Name";
            comboBoxLogType.ValueMember = "Value";
            comboBoxLogType.DataSource = bindingSourceLogType;
        }

        private void BindDatabases()
        {
            List<NameValueDto> items = new List<NameValueDto>();

            items.Add(new NameValueDto { Name = "Sql Server", Value = "SqlServer" });

            bindingSourceDatabase.DataSource = items;
            comboBoxDatabase.DisplayMember = "Name";
            comboBoxDatabase.ValueMember = "Value";
            comboBoxDatabase.DataSource = bindingSourceDatabase;
        }

        private void BindOrms()
        {
            List<NameValueDto> items = new List<NameValueDto>();

            items.Add(new NameValueDto { Name = "EF Core", Value = "EFCore" });

            bindingSourceOrm.DataSource = items;
            comboBoxOrm.DisplayMember = "Name";
            comboBoxOrm.ValueMember = "Value";
            comboBoxOrm.DataSource = bindingSourceOrm;
        }

        private void labelTemplateName_Click(object sender, EventArgs e)
        {

        }
    }
}
