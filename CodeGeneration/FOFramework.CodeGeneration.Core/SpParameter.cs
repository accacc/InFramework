using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOFramework.CodeGeneration.Core
{
    public class SpParameter
    {

        public string Name { get; set; }

        public string Alias { get; set; }

        public Type Type { get; set; }
        public bool IsNullable { get; set; }

        public string TableName { get; set; }

        public string DbType { get; set; }

        public bool IsPrimaryKey { get; set; }

        public int? Length { get; set; }

        public ParameterType ParameterType { get; set; }

        public string Value { get; set; }


    }

    public enum ParameterType
    {
        IN,
        OUT,
        INOUT
    }
}
