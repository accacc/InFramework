using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOFramework.CodeGeneration.Core
{
    public abstract class CodeFormatProvider
    {

        
        

        public abstract void FormatCode(CodeTemplate template, string path, string extension,string indent="");
        public abstract void FormatCode(string template, string path, string extension, string fileName);
    }
}
