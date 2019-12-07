using IF.Core.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace IF.Tools.Templates.Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            BindComboBox();
           
        }


        private void BindComboBox()
        {
            List<NameValueDto> projectTemplates = new List<NameValueDto>();
           

            using (var ctx = new MyDbContext())
            {
               

                foreach (var template in ctx.ProjectTemplates.ToList())
                {
                    projectTemplates.Add(new NameValueDto {Name = template.Name,Value = template.Code });

                }
            }



            templateTypeBindingSource.DataSource = projectTemplates;


            comboBoxTemplates.DisplayMember = "Name";
            comboBoxTemplates.ValueMember = "Value";
            comboBoxTemplates.DataSource = templateTypeBindingSource;


           
        }

        private void buttonSelectTemplate_Click(object sender, System.EventArgs e)
        {
            if(comboBoxTemplates.SelectedValue.ToString() == "WA")
            {
                WebApiTemplateDialog webApiTemplateDialog = new WebApiTemplateDialog();
                webApiTemplateDialog.ShowDialog();

            }
        }
    }
}
