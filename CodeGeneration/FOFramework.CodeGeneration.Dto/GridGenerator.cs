using DatabaseSchemaReader.DataSchema;
using FOFramework.CodeGeneration.Core;
using FOFramework.CodeGeneration.CSharp;
using FOFramework.CodeGeneration.Oracle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DTOGenerator
{
    public partial class GridGenerator : Form
    {
        public GridGenerator()
        {
            InitializeComponent();
        }

        public DatabaseSchema schema { get; set; }
        public TablePicker tablePicker { get; set; }

        public DatabaseTable table { get; set; }

        FileSystemCodeFormatProvider fileSystem = new FileSystemCodeFormatProvider(@"C:\Temp");


        public GridGenerator(DatabaseTable table, TablePicker form1, DatabaseSchema schema)
        {
            this.schema = schema;
            this.tablePicker = form1;
            InitializeComponent();

            LoadColumns(table.Name);
            LoadParameters(table.Name);
        }

        private void LoadColumns(string selectedTable
            )
        {

            this.table = schema.Tables.Where(c => c.Name == selectedTable).First();



            //var tableNode = treeViewTableName.Nodes.Add(table.Name);

            foreach (var column in table.Columns)
            {
                treeViewColumnPicker.Nodes.Add(column.Name);
            }


        }


        private void LoadParameters(string selectedTable)
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

                OracleListSP sp = GenerateListSP(columns, parameters);

                GenerateHandleMethod(sp, tablePascalCaseName);
                GenerateContractClasses(tablePascalCaseName,sp);                
                GenerateControllerMethods(tablePascalCaseName, sp);
                GenerateMvcModels(tablePascalCaseName, sp);
                GenerateViewModels(columns,tablePascalCaseName,sp);


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void GenerateViewModels(List<string> columns, string tablePascalCaseName, OracleListSP sp)
        {

            GenerateGrid(columns);
            GenerateFilterGridForm(tablePascalCaseName, sp);
        }

        private OracleListSP GenerateListSP(List<string> columns, List<string> parameters)
        {
            OracleListSP sp = new OracleListSP();

            sp.Name = String.Format("P_{0}_LIST", table.Name);
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
                sp.Columns.Add(spColumn);

            }





            fileSystem.FormatCode(sp.GenerateCode(),"sql");
            return sp;
        }

        private void GenerateMvcModels(string tablePascalCaseName, OracleListSP sp)
        {

            CSClass gridFilterClass = new CSClass();

            gridFilterClass.Name = tablePascalCaseName + "GridFilterModel";

            foreach (var parameter in sp.Parameters)
            {
                gridFilterClass.Properties.Add
                    (
                    new CSProperty(parameter.Type, "public", parameter.Alias, parameter.IsNullable)
                    );
            }

            

            fileSystem.FormatCode(gridFilterClass.GenerateCode(),"cs");


            CSClass gridClass = new CSClass();

            gridClass.Name = tablePascalCaseName + "GridModel";

            foreach (var column in sp.Columns)
            {
                gridClass.Properties.Add
                    (
                    new CSProperty(column.Type, "public", column.Alias, column.IsNullable)
                    );
            }

            

            fileSystem.FormatCode(gridClass.GenerateCode(),"cs");



        }

        private void GenerateControllerMethods(string tablePascalCaseName, OracleListSP sp)
        {

            

            CsMethod index = new CsMethod(tablePascalCaseName + "Index", "ActionResult", "public");

            //DepartmentExtensionGridFilterModel model = new DepartmentExtensionGridFilterModel();

            StringBuilder methodBody = new StringBuilder();

            methodBody.AppendFormat("{0} model = new {0}();", tablePascalCaseName + "Grid");
            methodBody.AppendLine();

            methodBody.AppendFormat(@"return View(""~/Views/{0}/Index.cshtml"",model);", tablePascalCaseName);

            index.Body = methodBody.ToString();

            



            CsMethod grid = new CsMethod(tablePascalCaseName+"Grid", "ActionResult","public");

            methodBody = new StringBuilder();

            foreach (var parameter in sp.Parameters)
            {
                grid.Parameters.Add
                    (
                    
                    new CsMethodParameter
                    {

                        Name = parameter.Name,
                        Type = parameter.DbType
                    }
                    );
            }


            //var response = dispatcher.Query<DepartmentListRequest, DepartmentListResponse>(
            methodBody.AppendFormat("var response = dispatcher.Query<{0},{1}>", tablePascalCaseName + "ListRequest", tablePascalCaseName + "ListResponse");
            methodBody.AppendLine();

            methodBody.AppendFormat("(");
            methodBody.AppendLine();

            methodBody.Append("new " + tablePascalCaseName + "ListRequest()");
            methodBody.AppendLine("{");

            foreach (var parameter in sp.Parameters)
            {
                methodBody.AppendFormat("{0}={1},",parameter.Name,parameter.Name);
                methodBody.AppendLine();
            }

            methodBody.AppendLine("};");

            //var model = response.Departments.MapTo<DepartmentExtensionGridModel>();
            //return Json(model.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            methodBody.AppendFormat("var model = response.Data.MapTo<{0}GridModel>();",tablePascalCaseName+"GridModel");
            methodBody.AppendLine();

            methodBody.AppendFormat("return Json(model.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);");
            methodBody.AppendLine();            

            grid.Body = methodBody.ToString();

            var methods = index.GenerateCode().Template + Environment.NewLine +Environment.NewLine + grid.GenerateCode().Template;


            fileSystem.FormatCode(methods,"cs",tablePascalCaseName + "Controller");
        }

        private void GenerateHandleMethod(OracleListSP sp,string tablePascalCaseName)
        {
            CsMethod method = new CsMethod(tablePascalCaseName+"Handle",tablePascalCaseName + "ListResponse","public");
            
            StringBuilder methodBody = new StringBuilder();

            methodBody.AppendFormat("{0} response = new {0}();",tablePascalCaseName + "ListResponse");
            methodBody.AppendLine();

            //var ds = oracleCommand.GetDatatable(null, "P_WORK_PACKAGE_LIST", request, null);
            methodBody.AppendFormat(@"var ds = oracleCommand.GetDatatable(null,""{0}"",request,null);", sp.Name);
            methodBody.AppendLine();

            //response.WorkPackages = ds.MapTo<WorkPackageListDto>().ToList();
            methodBody.AppendFormat("response.Data = ds.MapTo<{0}>().ToList();", sp.TableName + "ListDto");
            methodBody.AppendLine();

            method.Body = methodBody.ToString();

            fileSystem.FormatCode(method.GenerateCode(),"cs") ;


        }


        private void GenerateContractClasses(string tablePascalCaseName, OracleListSP sp)
        {
            string className = this.ToTitleCase(table.Name);

            CSClass @class = new CSClass();

            @class.Name = className+"Dto";
            @class.Properties = new List<CSProperty>();

            foreach (var column in sp.Columns)
            {


                @class.Properties.Add
                (
                new CSProperty(column.Type, "public",column.Alias , column.IsNullable)

                );
            }



            CSClass requestClass = new CSClass();
            requestClass.BaseClass = "BaseRequest";
            requestClass.Name = className+"ListRequest";


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
            responseClass.Name = className+"ListResponse";

            

            CSProperty dtoProperty = new CSProperty(null, "public", "Data", false);
            dtoProperty.PropertyTypeString = String.Format("List<{0}ListDto>", className);

            responseClass.Properties.Add(dtoProperty);


            var classes = @class.GenerateCode().Template + Environment.NewLine + requestClass.GenerateCode().Template + Environment.NewLine + responseClass.GenerateCode().Template;

            fileSystem.FormatCode(classes, "cs",tablePascalCaseName+ "Dto");



        }

        private void GenerateGrid(List<string> columns)
        {
            string gridTemplate = @"@(Html.Ford().GridAjax<ScheduleListModel>(""{0}"", ""{1}"")

                                    .Columns(columns =>
                                    {{
                                         {2}
                                    }}";

            string columnS = String.Empty;

            foreach (var column in columns)
            {
                columnS += String.Format("columns.Bound(x => x.{0});" + Environment.NewLine, this.ToTitleCase(column));
            }

            gridTemplate = String.Format(gridTemplate, textBoxActionName.Text, textBoxControllerName.Text,columnS);


            fileSystem.FormatCode(gridTemplate,"cshtml", "_GridView");
        }

        private void GenerateFilterGridForm(string tablePascalCaseName, OracleListSP sp)
        {


            StringBuilder filterForm = new StringBuilder();

            filterForm.AppendFormat("@model FordBudgetPlaning.WebUI.Models.{0}",tablePascalCaseName+"GridFilterModel");
            filterForm.AppendLine();
            filterForm.AppendLine();

            filterForm.AppendLine("@(Html.Ford().BootstrapAjaxFilterForm().Content(FormContent().ToHtmlString()).Render())");
            filterForm.AppendLine();
            filterForm.AppendLine();

            filterForm.AppendLine("@helper FormContent()");

            filterForm.AppendLine("{");


            filterForm.AppendLine("<div class=\"row\">");

            foreach (var parameter in sp.Parameters)
            {

                filterForm.AppendLine("<div class=\"col-md-12\">");

                if(parameter.Type == typeof(String))
                {
                    //@Html.Ford().TextBoxFor(ds => ds.FVEHICLEGROUP_ID).RenderWithFormGroup()
                    filterForm.AppendFormat("@Html.Ford().TextBoxFor(ds => ds.{0}).RenderWithFormGroup()", parameter.Alias);
                }
                else
                {
                    filterForm.AppendFormat("@Html.Ford().?(ds => ds.{0}).RenderWithFormGroup()", parameter.Alias);
                }

                filterForm.AppendLine("</div>");
            }

            filterForm.AppendLine("</div>");

            filterForm.AppendLine("}");

            
            fileSystem.FormatCode(filterForm.ToString(),"cshtml", "_FilterForm");
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
