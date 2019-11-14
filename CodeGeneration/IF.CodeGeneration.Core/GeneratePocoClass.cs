//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Generator.Core
//{
//    public abstract class GeneratePocoClass
//    {

//        protected CodeFormatProvider codeFormatProvider;

//        public GeneratePocoClass(CodeFormatProvider codeFormatProvider)
//        {
//            this.codeFormatProvider = codeFormatProvider;
//        }

//        public virtual void GenerateCode(string tableName)
//        {
//            this.codeFormatProvider.FormatCode(this.GenerateCodeTemplate(tableName));

//        }

//        public abstract CodeTemplate GenerateCodeTemplate(string tableName);
//        public abstract List<string> GetAllTables();
//        //public abstract List<TableRelation> GetTableRelation(string schemaName, string tableName);


//    }
//}
