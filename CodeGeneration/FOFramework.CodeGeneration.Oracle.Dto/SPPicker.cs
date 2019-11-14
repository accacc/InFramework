using DatabaseSchemaReader;
using DatabaseSchemaReader.DataSchema;

using FOFramework.CodeGeneration.Core;
using FOFramework.CodeGeneration.Oracle.Dto;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DTOGenerator
{
    public partial class SPPicker : Picker
    {




        string clickedNode;
        MenuItem myMenuItem = new MenuItem("Show Me");
        ContextMenu mnu = new ContextMenu();
        DatabaseSchema schema;

        //GeneratePocoClass pocoClass;

        string cnnString = ConfigurationManager.ConnectionStrings["mydb"].ToString();

        public SPPicker()
        {
            InitializeComponent();
            mnu.MenuItems.Add(myMenuItem);
            myMenuItem.Click += new EventHandler(myMenuItem_Click);
            //pocoClass = new GeneratePocoClassSqlServer(cnnString,new FileSystemCodeFormatProvider(@"C:\Temp\"));


        }

        private void LoadTables()
        {

            var reader = new DatabaseReader(cnnString, "Oracle.ManagedDataAccess.Client", "WUSRADMIN");


            schema = reader.ReadAll();

            foreach (var table in schema.StoredProcedures)
            {
                var tableNode = treeViewSPName.Nodes.Add(table.Name);

                //foreach (var column in table.Columns)
                //{
                //    tableNode.Nodes.Add(column.Name);
                //}

            }

            foreach (var package in schema.Packages)
            {
                foreach (var sp in package.StoredProcedures)
                {
                    treeViewSPName.Nodes.Add(sp.Name);
                }
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
                List<string> sps = new List<string>();

                foreach (TreeNode node in treeViewSPName.Nodes)
                {
                    if (node.Checked)
                    {
                        sps.Add(node.Text);
                    }
                }


                var dbSp = schema.StoredProcedures.Where(t => t.Name == sps.First()).SingleOrDefault();

                if(dbSp == null)
                {
                    dbSp = schema.Packages.SelectMany(p => p.StoredProcedures.Where(s => s.Name == sps.First())).SingleOrDefault();

                }

                var sp = GenerateStoreProcedure(dbSp);

                SpParameterForm spParameterForm = new SpParameterForm(sp,dbSp,this);

                spParameterForm.Show();


                //if (gridRadioButton.Checked)
                //{
                //    var sp = GenerateStoreProcedure(dbSp);

                //    GridGenerator cp = new GridGenerator(sp, this);


                //    cp.Show();


                //}
                //else if (radioGenerateFormButton.Checked)
                //{

                //    //FormGenerator cp = new FormGenerator(dbTable, this, schema);
                //    //cp.Show();

                //}




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

        private StoredProcedure GenerateStoreProcedure(DatabaseStoredProcedure dbSp)
        {
            StoredProcedure sp = new StoredProcedure();

            sp.Name = dbSp.Name;

            foreach (var arg in dbSp.Arguments)
            {

                if (arg.DatabaseDataType == "REF CURSOR") continue;

                SpParameter parameter = new SpParameter();
                parameter.Name = arg.Name;
                parameter.Type = arg.DataType.GetNetType();
                parameter.Length = arg.Length;

                if (arg.In && !arg.Out)
                {
                    parameter.ParameterType = ParameterType.IN;
                }

                if (!arg.In && arg.Out)
                {
                    parameter.ParameterType = ParameterType.OUT;
                }

                if (arg.In && arg.Out)
                {
                    parameter.ParameterType = ParameterType.INOUT;
                }

                sp.Parameters.Add(parameter);
            }

            return sp;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                clickedNode = e.Node.Name;
                mnu.Show(treeViewSPName, e.Location);
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
