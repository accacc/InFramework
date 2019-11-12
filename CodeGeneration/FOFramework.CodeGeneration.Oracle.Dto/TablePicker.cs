using DatabaseSchemaReader;
using DatabaseSchemaReader.DataSchema;
using FOFramework.CodeGeneration.Oracle.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DTOGenerator
{
    public partial class TablePicker : Picker
    {




        string clickedNode;
        MenuItem myMenuItem = new MenuItem("Show Me");
        ContextMenu mnu = new ContextMenu();
        DatabaseSchema schema;

        //GeneratePocoClass pocoClass;

        string cnnString = ConfigurationManager.ConnectionStrings["mydb"].ToString();

        public TablePicker()
        {
            InitializeComponent();
            mnu.MenuItems.Add(myMenuItem);
            myMenuItem.Click += new EventHandler(myMenuItem_Click);
            //pocoClass = new GeneratePocoClassSqlServer(cnnString,new FileSystemCodeFormatProvider(@"C:\Temp\"));


        }

        private void LoadTables()
        {



            var reader = new DatabaseReader(cnnString, "Oracle.ManagedDataAccess.Client", "FORD_BUDGET_PLANNING");


            schema = reader.ReadAll();






            foreach (var table in schema.Tables)
            {
                var tableNode = treeViewTableName.Nodes.Add(table.Name);

                //foreach (var column in table.Columns)
                //{
                //    tableNode.Nodes.Add(column.Name);
                //}

            }
        }

        void myMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Form();
            frm.Text = clickedNode;
            frm.ShowDialog(this);
            clickedNode = "";
        }


        private void buttonGenerate(object sender, EventArgs e)
        {
            try
            {
                List<string> tables = new List<string>();

                foreach (TreeNode node in treeViewTableName.Nodes)
                {
                    if (node.Checked)
                    {
                        tables.Add(node.Text);
                    }
                }


                var dbTable = schema.Tables.Where(t => t.Name == tables.First()).Single();


                if (gridRadioButton.Checked)
                {

                    //GridGenerator cp = new GridGenerator(dbTable, this, schema);
                    //cp.Show();


                }
                else if (radioGenerateFormButton.Checked)
                {

                    FormGenerator cp = new FormGenerator(dbTable, this, schema);
                    cp.Show();

                }




                this.Hide();



                //foreach (var table in tables)
                //{
                //    pocoClass.GenerateCode(table);
                //}                          

                //Process.Start("explorer.exe", @"c:\temp");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                clickedNode = e.Node.Name;
                mnu.Show(treeViewTableName, e.Location);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //bool IsSelectAll = false;

            //if(buttonSelectAll.Text == "Select All")
            //{
            //    IsSelectAll = true;
            //    buttonSelectAll.Text = "Unselect All";
            //}
            //else
            //{
            //    IsSelectAll = false;
            //    buttonSelectAll.Text = "Select All";
            //}

            //SelectAllNodes(treeViewTableName.Nodes, IsSelectAll);

            LoadTables();

        }



        public void SelectAllNodes(TreeNodeCollection nodes, bool isChecked)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = isChecked;
                CheckChildren(node, isChecked);
            }
        }



        private void CheckChildren(TreeNode rootNode, bool isChecked)
        {
            foreach (TreeNode node in rootNode.Nodes)
            {
                CheckChildren(node, isChecked);
                node.Checked = isChecked;
            }
        }


    }
}
