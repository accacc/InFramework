//using Generator.Core;
//using Generator.CSharp;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Generator.SqlServer
//{
//    public class GeneratePocoClassSqlServer : GeneratePocoClass
//    {

//        public string cnnString { get; set; }



//        public GeneratePocoClassSqlServer(string cnnString, CodeFormatProvider codeFormatProvider)
//            : base(codeFormatProvider)
//        {

//            this.cnnString = cnnString;
//        }


//        public override CodeTemplate GenerateCodeTemplate(string tableName)
//        {
//            CSClass csClass = new CSClass();

//            using (SqlConnection OleDBConnection = new SqlConnection(cnnString))
//            {               

//                OleDBConnection.Open();

//                string[] restrictions2 = new string[] { null, null, null, "BASE TABLE" };

//                restrictions2[2] = tableName;

//                System.Data.DataTable tables = OleDBConnection.GetSchema("Tables", restrictions2);

//                foreach (System.Data.DataRow row in tables.Rows)
//                {
//                    string[] restrictions1 = new string[] { null, null, (string)row["TABLE_NAME"], null };

//                    System.Data.DataTable DataTable1 = OleDBConnection.GetSchema("Columns", restrictions1);


//                    csClass.Name = tableName;
//                    csClass.Properties = new List<CSProperty>();

//                    foreach (DataRow row1 in DataTable1.Rows)
//                    {
//                        bool IsNullable = row1["IS_NULLABLE"].ToString() != "NO";

//                        CSProperty property = new CSProperty(TypeExtension.ToClrTypeFromString(row1["DATA_TYPE"].ToString()), "public", row1["COLUMN_NAME"].ToString(), IsNullable);

//                        csClass.Properties.Add(property);
//                    }
//                }

//                OleDBConnection.Close();

//            }
//            return csClass.GenerateCode();



//        }

//        public override List<string> GetAllTables()
//        {
//            List<string> tableNames = new List<string>();

//            using (SqlConnection OleDBConnection = new SqlConnection(cnnString))
//            {
//                OleDBConnection.Open();

//                string[] restrictions2 = new string[] { null, null, null, "BASE TABLE" };
//                System.Data.DataTable tables = OleDBConnection.GetSchema("Tables", restrictions2);
//                DataRow[] dataRows = tables.Select().OrderBy(u => u["TABLE_NAME"]).ToArray();

//                foreach (System.Data.DataRow row in dataRows)
//                {
//                    tableNames.Add(row["TABLE_NAME"].ToString());
//                }
//                OleDBConnection.Close();
//            }
//            return tableNames;
//        }
//    }
//}
