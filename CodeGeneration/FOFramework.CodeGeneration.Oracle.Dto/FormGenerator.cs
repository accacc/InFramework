using DatabaseSchemaReader.DataSchema;
using FOFramework.CodeGeneration.Core;
using FOFramework.CodeGeneration.CSharp;
using FOFramework.CodeGeneration.Oracle;
using FOFramework.CodeGeneration.Oracle.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DTOGenerator
{
    public partial class FormGenerator : Form
    {
        public FormGenerator()
        {
            InitializeComponent();
        }

        public DatabaseSchema schema { get; set; }
        public Picker tablePicker { get; set; }

        public DatabaseTable table { get; set; }

        FileSystemCodeFormatProvider fileSystem = new FileSystemCodeFormatProvider(@"C:\Temp");


        public FormGenerator(DatabaseTable table, Picker form1, DatabaseSchema schema)
        {
            this.schema = schema;
            this.tablePicker = form1;
            InitializeComponent();

            LoadColumns(table.Name);
            LoadParameters(table.Name);
        }

        private void LoadColumns(string selectedTable)
        {

            this.table = schema.Tables.Where(c => c.Name == selectedTable).First();



            //var tableNode = treeViewTableName.Nodes.Add(table.Name);

            foreach (var column in table.Columns)
            {
                treeViewColumnPicker.Nodes.Add(column.Name);
            }


        }


        private void LoadParameters(string selectedTable
         )
        {

            this.table = schema.Tables.Where(c => c.Name == selectedTable).First();



            //var tableNode = treeViewTableName.Nodes.Add(table.Name);

            foreach (var column in table.Columns)
            {
                treeViewParamPicker.Nodes.Add(column.Name);
            }


        }

        private void GenerateSpButton_Click(object sender, EventArgs e)
        {

            try
            {



                string tablePascalCaseName = this.ToTitleCase(this.table.Name);

                List<string> columns = new List<string>();

                foreach (TreeNode node in treeViewColumnPicker.Nodes)
                {
                    if (node.Checked)
                    {

                        columns.Add(node.Text);
                    }
                }


                List<string> parameters = new List<string>();

                foreach (TreeNode node in treeViewParamPicker.Nodes)
                {
                    if (node.Checked)
                    {

                        parameters.Add(node.Text);
                    }
                }

                GenerateInsert(tablePascalCaseName, columns,parameters);
                GenerateUpdate(tablePascalCaseName, columns,parameters);
                GenerateGet(tablePascalCaseName, columns,parameters);



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void GenerateUpdate(string tablePascalCaseName, List<string> columns,List<string> parameters)
        {
            OracleUpdateSP sp = GenerateUpdateSP(columns,parameters);
            //GenerateContractClasses(tablePascalCaseName, sp);
            //GenerateHandleMethod(sp, tablePascalCaseName);
        }

        private void GenerateGet(string tablePascalCaseName, List<string> columns, List<string> parameters)
        {
            OracleGetSP sp = GenerateGetSP(columns, parameters);
            GenerateGetContractClasses(tablePascalCaseName, sp);
            GenerateGetHandleMethod(sp, tablePascalCaseName);
        }

        private void GenerateInsert(string tablePascalCaseName, List<string> columns, List<string> parameters)
        {
            OracleInsertSP sp = GenerateInsertSP(columns,parameters);
            GenerateInsertContractClasses(tablePascalCaseName, sp);
            GenerateInsertHandleMethod(sp, tablePascalCaseName);
        }

        private void GenerateInsertContractClasses(string tablePascalCaseName, OracleInsertSP sp)
        {



            string className = this.ToTitleCase(table.Name);

            CSClass @class = new CSClass();

            @class.Name = className + "Dto";
            @class.Properties = new List<CSProperty>();

            foreach (var column in sp.Columns)
            {


                @class.Properties.Add
                (
                new CSProperty(column.Type, "public", column.Alias, column.IsNullable)

                );
            }



            CSClass requestClass = new CSClass();
            requestClass.BaseClass = "BaseCommandRequest";
            requestClass.Name = className;


            CSProperty dtoProperty = new CSProperty(null, "public", @class.Name, false);
            dtoProperty.PropertyTypeString = String.Format("{0}", className);

            requestClass.Properties.Add(dtoProperty);



            var classes = @class.GenerateCode().Template + Environment.NewLine + requestClass.GenerateCode().Template;

            fileSystem.FormatCode(classes, "cs", tablePascalCaseName + "InsertDto");



        }


        private OracleInsertSP GenerateInsertSP(List<string> columns, List<string> parameters)
        {
            OracleInsertSP sp = new OracleInsertSP();

            sp.Name = String.Format("P_{0}_ADD", table.Name);
            sp.Parameters = new List<SpParameter>();
            sp.TableName = table.Name;

            foreach (var parameter in parameters)
            {

                var dbcolumn = table.Columns.Where(c => c.Name == parameter).Single();

                SpParameter param = new SpParameter();
                param.DbType = dbcolumn.DbDataType;
                param.ParameterType = ParameterType.IN;
                param.TableName = table.Name;
                param.Type = dbcolumn.DataType.GetNetType();
                param.IsNullable = dbcolumn.Nullable;
                param.Alias = this.ToTitleCase(dbcolumn.Name);
                param.Name = dbcolumn.Name;
                param.IsPrimaryKey = dbcolumn.IsPrimaryKey;
                sp.Parameters.Add(param);
            }




            sp.Columns = new List<SpColumn>();

            foreach (var column in columns)
            {

                var dbcolumn = table.Columns.Where(c => c.Name == column).Single();

                SpColumn spColumn = new SpColumn();
                spColumn.Name = dbcolumn.Name;
                
                spColumn.Alias = this.ToTitleCase(dbcolumn.Name);
                spColumn.Type = dbcolumn.DataType.GetNetType();
                spColumn.IsNullable = dbcolumn.Nullable;
                spColumn.DbType = dbcolumn.DbDataType;
                spColumn.IsPrimaryKey = dbcolumn.IsPrimaryKey;
                sp.Columns.Add(spColumn);

            }



            var primaryKeyColumn = table.Columns.Where(p => p.IsPrimaryKey).Single();

            SpColumn primaryColumn = new SpColumn();
            primaryColumn.DbType = primaryKeyColumn.DbDataType;
            primaryColumn.TableName = table.Name;
            primaryColumn.Type = primaryKeyColumn.DataType.GetNetType();
            primaryColumn.IsNullable = primaryKeyColumn.Nullable;
            primaryColumn.Alias = this.ToTitleCase(primaryKeyColumn.Name);
            primaryColumn.Name = primaryKeyColumn.Name;
            sp.PrimaryColumn = primaryColumn;



            fileSystem.FormatCode(sp.GenerateCode(),"sql");
            return sp;
        }

        private OracleUpdateSP GenerateUpdateSP(List<string> columns, List<string> parameters)
        {
            OracleUpdateSP sp = new OracleUpdateSP();

            sp.Name = String.Format("P_{0}_EDIT", table.Name);
            sp.Parameters = new List<SpParameter>();
            sp.TableName = table.Name;

            foreach (var parameter in parameters)
            {

                var dbcolumn = table.Columns.Where(c => c.Name == parameter).Single();

                SpParameter param = new SpParameter();
                param.DbType = dbcolumn.DbDataType;
                param.ParameterType = ParameterType.IN;
                param.TableName = table.Name;
                param.Type = dbcolumn.DataType.GetNetType();
                param.IsNullable = dbcolumn.Nullable;
                param.Alias = this.ToTitleCase(dbcolumn.Name);
                param.Name = dbcolumn.Name;
                param.IsPrimaryKey = dbcolumn.IsPrimaryKey;
                sp.Parameters.Add(param);
            }




            sp.Columns = new List<SpColumn>();

            foreach (var column in columns)
            {

                var dbcolumn = table.Columns.Where(c => c.Name == column).Single();

                SpColumn spColumn = new SpColumn();
                spColumn.Name = dbcolumn.Name;

                spColumn.Alias = this.ToTitleCase(dbcolumn.Name);
                spColumn.Type = dbcolumn.DataType.GetNetType();
                spColumn.IsNullable = dbcolumn.Nullable;
                spColumn.DbType = dbcolumn.DbDataType;
                spColumn.IsPrimaryKey = dbcolumn.IsPrimaryKey;
                sp.Columns.Add(spColumn);

                SpParameter param = new SpParameter();
                param.DbType = dbcolumn.DbDataType;
                param.ParameterType = ParameterType.IN;
                param.TableName = table.Name;
                param.Type = dbcolumn.DataType.GetNetType();
                param.IsNullable = dbcolumn.Nullable;
                param.Alias = this.ToTitleCase(dbcolumn.Name);
                param.Name = dbcolumn.Name;
                param.IsPrimaryKey = dbcolumn.IsPrimaryKey;
                sp.Parameters.Add(param);

            }



            var primaryKeyColumn = table.Columns.Where(p => p.IsPrimaryKey).Single();

            SpColumn primaryColumn = new SpColumn();
            primaryColumn.DbType = primaryKeyColumn.DbDataType;
            primaryColumn.TableName = table.Name;
            primaryColumn.Type = primaryKeyColumn.DataType.GetNetType();
            primaryColumn.IsNullable = primaryKeyColumn.Nullable;
            primaryColumn.Alias = this.ToTitleCase(primaryKeyColumn.Name);
            primaryColumn.Name = primaryColumn.Name;
            sp.PrimaryColumn = primaryColumn;



            fileSystem.FormatCode(sp.GenerateCode(), "sql");
            return sp;
        }

        private OracleGetSP GenerateGetSP(List<string> columns, List<string> parameters)
        {
            OracleGetSP sp = new OracleGetSP();

            sp.Name = String.Format("P_{0}_GET", table.Name);
            sp.Parameters = new List<SpParameter>();
            sp.TableName = table.Name;

            foreach (var parameter in parameters)
            {

                var dbcolumn = table.Columns.Where(c => c.Name == parameter).Single();

                SpParameter param = new SpParameter();
                param.DbType = dbcolumn.DbDataType;
                param.ParameterType = ParameterType.IN;
                param.TableName = table.Name;
                param.Type = dbcolumn.DataType.GetNetType();
                param.IsNullable = dbcolumn.Nullable;
                param.Alias = this.ToTitleCase(dbcolumn.Name);
                param.Name = dbcolumn.Name;
                param.IsPrimaryKey = dbcolumn.IsPrimaryKey;
                sp.Parameters.Add(param);
            }




            sp.Columns = new List<SpColumn>();

            foreach (var column in columns)
            {

                var dbcolumn = table.Columns.Where(c => c.Name == column).Single();

                SpColumn spColumn = new SpColumn();
                spColumn.Name = dbcolumn.Name;

                spColumn.Alias = this.ToTitleCase(dbcolumn.Name);
                spColumn.Type = dbcolumn.DataType.GetNetType();
                spColumn.IsNullable = dbcolumn.Nullable;
                spColumn.DbType = dbcolumn.DbDataType;
                spColumn.IsPrimaryKey = dbcolumn.IsPrimaryKey;
                sp.Columns.Add(spColumn);

            }



          



            fileSystem.FormatCode(sp.GenerateCode(), "sql");
            return sp;
        }



        private void GenerateInsertHandleMethod(OracleInsertSP sp,string tablePascalCaseName)
        {
            CSMethod method = new CSMethod(tablePascalCaseName+"Handle",tablePascalCaseName + "AddRequest","public");
            
            StringBuilder methodBody = new StringBuilder();


            string parameterBody = "var @object = new";

            parameterBody += "{" +Environment.NewLine;

            string parameters = String.Empty;

            foreach (var parameter in sp.Columns)
            {
                parameters += String.Format("{0} = {1},", parameter.Name, "command." + tablePascalCaseName + "Dto." + parameter.Alias);
                parameters += Environment.NewLine;                
            }

            

            parameterBody += parameters.Remove(parameters.Length - 1);

            parameterBody += "};" + Environment.NewLine;


            //var ds = oracleCommand.GetDatatable(null, "P_WORK_PACKAGE_LIST", request, null);
            methodBody.AppendFormat(@"var lastId = oracleCommand.ExecuteInsertQuery(null,""{0}"",@object);", sp.Name);
            methodBody.AppendLine();           

            method.Body = parameterBody + Environment.NewLine + methodBody.ToString();

            fileSystem.FormatCode(method.GenerateCode(),"cs","Insert") ;


        }

        private void GenerateGetHandleMethod(OracleGetSP sp, string tablePascalCaseName)
        {
            CSMethod method = new CSMethod(tablePascalCaseName + "Handle", tablePascalCaseName + "GetResponse", "public");

            StringBuilder methodBody = new StringBuilder();

            methodBody.AppendFormat("{0} response = new {0}();", tablePascalCaseName + "GetResponse");
            methodBody.AppendLine();

            //var ds = oracleCommand.GetDatatable(null, "P_WORK_PACKAGE_LIST", request, null);
            methodBody.AppendFormat(@"var ds = oracleCommand.GetDatatable(null,""{0}"",request,null);", sp.Name);
            methodBody.AppendLine();

            //response.WorkPackages = ds.MapTo<WorkPackageListDto>().ToList();
            methodBody.AppendFormat("response.Data = ds.MapTo<{0}>().Single();", sp.TableName + "Dto");
            methodBody.AppendLine();

            method.Body = methodBody.ToString();

            fileSystem.FormatCode(method.GenerateCode(), "cs","Get");


        }

        private void GenerateGetContractClasses(string tablePascalCaseName, OracleGetSP sp)
        {
            string className = this.ToTitleCase(table.Name);

            CSClass @class = new CSClass();

            @class.Name = className + "Dto";
            @class.Properties = new List<CSProperty>();

            foreach (var column in sp.Columns)
            {


                @class.Properties.Add
                (
                new CSProperty(column.Type, "public", column.Alias, column.IsNullable)

                );
            }



            CSClass requestClass = new CSClass();
            requestClass.BaseClass = "BaseRequest";
            requestClass.Name = className+"GetRequest";


            requestClass.Properties = new List<CSProperty>();

            foreach (var parameter in sp.Parameters)
            {


                requestClass.Properties.Add
                (
                new CSProperty(parameter.Type, "public", parameter.Name, parameter.IsNullable)

                );
            }





            CSClass responseClass = new CSClass();
            responseClass.BaseClass = "BaseResponse";
            responseClass.Name = className+"GetResponse";



            CSProperty dtoProperty = new CSProperty(null, "public", className, false);
            dtoProperty.PropertyTypeString = String.Format("{0}Dto", className);

            responseClass.Properties.Add(dtoProperty);


            var classes = @class.GenerateCode().Template + Environment.NewLine + requestClass.GenerateCode().Template + Environment.NewLine + responseClass.GenerateCode().Template;

            fileSystem.FormatCode(classes, "cs", tablePascalCaseName + "GetDto");



        }



        private string ToTitleCase(string text)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");

            return cultureInfo.TextInfo.ToTitleCase(text.ToLower(cultureInfo)).Replace("_", "");
        }

        private void SpGenerator_FormClosed(object sender, FormClosedEventArgs e)
        {
            tablePicker.Show();
        }
    }
}
