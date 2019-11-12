using FOFramework.CodeGeneration.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOFramework.CodeGeneration.Oracle
{
    public class OracleInsertSP : StoredProcedure,IGenerateCode
    {


        public SpColumn PrimaryColumn { get; set; }
    

        public string Type { get; set; }

        public string TableName { get; set; }


        public CodeTemplate GenerateCode()
        {

            StringBuilder builder = new StringBuilder("create or replace procedure " + Name);
            builder.AppendLine();
            builder.AppendLine("(");

            foreach (var param in this.Columns)
            {

                if (param.IsPrimaryKey) continue;

                builder.Append(String.Format("P_{0} {1} {2}", param.Name,"IN", param.DbType));

                builder.AppendLine(",");

                
            }

            if (this.PrimaryColumn != null)
            {
                builder.AppendLine();
                builder.AppendLine(String.Format("{0} {1} {2}", "P_ROWID", "OUT", this.PrimaryColumn.DbType));
            }


            builder.AppendLine(")");

            builder.AppendLine("is");

            builder.AppendLine("begin");



            builder.AppendFormat("insert into {0}",this.TableName);
            builder.AppendLine();
            builder.AppendLine("(");



            foreach (var column in this.Columns)
            {
                if (column.IsPrimaryKey) continue;
                builder.Append(column.Name);

                if (this.Columns.Last().Name != column.Name)
                {

                    builder.AppendLine(",");
                }
            }

            builder.AppendLine(")");

            builder.AppendLine("values");
            builder.AppendLine("(");
            

            foreach (var column in this.Columns)
            {
                if (column.IsPrimaryKey) continue;

                builder.AppendFormat("P_{0}",column.Name);

                if (this.Columns.Last().Name != column.Name)
                {

                    builder.AppendLine(",");
                }

            }

            builder.AppendLine(")");

            

            if (this.PrimaryColumn != null)
            {
                builder.AppendFormat("returning {0} into P_ROWID", this.PrimaryColumn.Name);
            }
            builder.AppendLine(";");


            builder.AppendLine("end;");



            CodeTemplate template = new CodeTemplate();
            template.Template = builder.ToString();
            template.CodeTemplateName = this.Name;
            return template;
        }


    }
}
