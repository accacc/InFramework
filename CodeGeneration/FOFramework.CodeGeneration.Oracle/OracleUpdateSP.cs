using FOFramework.CodeGeneration.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOFramework.CodeGeneration.Oracle
{
    public class OracleUpdateSP : StoredProcedure,IGenerateCode
    {


        public SpColumn PrimaryColumn { get; set; }       

        public string Type { get; set; }

        public string TableName { get; set; }


        public CodeTemplate GenerateCode()
        {

            StringBuilder builder = new StringBuilder("create or replace procedure " + Name);
            builder.AppendLine();
            builder.AppendLine("(");

            foreach (var param in this.Parameters)
            {
                builder.AppendLine(String.Format("P_{0} {1} {2}", param.Name, param.ParameterType.ToString(), param.DbType));

                if (this.Parameters.IndexOf(param) != this.Parameters.Count - 1)
                {

                    builder.AppendLine(",");
                }
            }


            builder.AppendLine(")");

            builder.AppendLine("is");

            builder.AppendLine("begin");



            builder.AppendFormat("update {0}", this.TableName);
            builder.AppendLine();
            builder.AppendLine("set");



            foreach (var column in this.Columns)
            {

                builder.AppendFormat("{0} = P_{1}", column.Alias, column.Name);

                if (this.Columns.Last().Name != column.Name)
                {

                    builder.AppendLine(",");
                }
            }

            builder.AppendLine();

            builder.AppendLine("where");

            foreach (var parameter in this.Parameters)
            {
                builder.AppendLine(String.Format("{0}.{1} =  P_{2} ", parameter.TableName, parameter.Name, parameter.Name));

                if (this.Parameters.Last().Name != parameter.Name)
                {
                    builder.Append("AND ");
                }
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
