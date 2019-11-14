using IF.CodeGeneration.Core;
using System;
using System.Linq;
using System.Text;

namespace FOFramework.CodeGeneration.Oracle
{
    public class OracleGetSP : StoredProcedure,IGenerateCode
    {



        public string Type { get; set; }

        public string TableName { get; set; }


        public CodeTemplate GenerateCode()
        {

            StringBuilder builder = new StringBuilder("create or replace procedure " + Name);
            builder.AppendLine("(");

            foreach (var parameter in this.Parameters)
            {
                builder.AppendLine(String.Format("P_{0} {1} {2},", parameter.Name, parameter.ParameterType.ToString(), parameter.DbType));
            }

            builder.AppendLine("CURSOR_  OUT SYS_REFCURSOR");

            builder.AppendLine(")");

            builder.AppendLine("is");

            builder.AppendLine("begin");

            builder.AppendLine("OPEN CURSOR_ FOR");


            builder.AppendLine("select");



            foreach (var column in this.Columns)
            {

                builder.AppendLine(String.Format(@"{0}.{1} as ""{2}""", TableName, column.Name, column.Alias));

                if (this.Columns.Last().Name != column.Name)
                {

                    builder.Append(",");
                }
            }


            builder.AppendLine("from");
            builder.AppendLine(TableName);
            builder.AppendLine("where");

            foreach (var parameter in this.Parameters)
            {
                if (parameter.Type == typeof(String))
                {
                    //    (PACKAGE_NAME IS NULL OR PACKAGE_NAME like  PACKAGE_NAME||'%') AND
                    builder.AppendLine(String.Format("(P_{0} IS NULL OR {1}.{2} like P_{3}||'%') ", parameter.Name, parameter.TableName, parameter.Name, parameter.Name));
                }
                else
                {
                    //    WP.PM_DEPARTMENT_ID = DEPARTMENT AND
                    builder.AppendLine(String.Format("{0}.{1} =  P_{2} ", parameter.TableName, parameter.Name, parameter.Name));
                }

                if (this.Parameters.Last().Name != parameter.Name)
                {
                    builder.Append("AND ");
                }
            }


            builder.Append(";");

            builder.AppendLine("end;");



            CodeTemplate template = new CodeTemplate();
            template.Template = builder.ToString();
            template.CodeTemplateName = this.Name;
            return template;
        }


    }
}
