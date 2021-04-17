using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.CodeGeneration.Core
{
    public abstract class CodeFormatProvider
    {
        public abstract void FormatCode(CodeTemplate template, string extension, string fileLastName, string extraPath);
        public abstract void FormatCode(string template, string extension, string fileFullName, string extraPath);
    }
}
