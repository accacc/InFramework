using DatabaseSchemaReader.DataSchema;
using FOFramework.CodeGeneration.Core;
using System;
using System.Windows.Forms;

namespace FOFramework.CodeGeneration.Oracle.Dto
{
    public partial class SpParameterForm : Form
    {

        public Picker tablePicker { get; set; }

        public StoredProcedure sp { get; set; }

        DatabaseStoredProcedure sp2 { get; set; }
        public SpParameterForm()
        {
            InitializeComponent();

        }

        public SpParameterForm(StoredProcedure sp, DatabaseStoredProcedure sp2, Picker form1)
        {
            this.tablePicker = form1;
            this.sp = sp;
            this.sp2 = sp2;
            InitializeComponent();


            //JsonSerializer js = new JsonSerializer();

            //string parameters = js.Serialize(sp2,true);

            //this.textBoxParameters.Text = parameters;
           
        }

        private void SpParameterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.tablePicker.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in this.sp.Parameters)
            {

            }
        }
    }
}
