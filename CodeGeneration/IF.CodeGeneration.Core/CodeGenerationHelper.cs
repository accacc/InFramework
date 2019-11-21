using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.CodeGeneration.Core
{
    public static class CodeGenerationHelper
    {
        public static void AddCodeBottom(string path,string method)
        {

            return;
            var lines = System.IO.File.ReadAllLines(path);
            if (lines != null)
            {
                lines = lines.Reverse().ToArray();

                bool find1 = false;
                bool find2 = false;

                for (int i = 0; i < lines.Length; i++)
                {

                    if (find1 && find2)
                    {
                        lines[i] += method + Environment.NewLine;
                        break;
                    }

                    if (lines[i].Trim() == "") continue;

                    if (!find1 && lines[i].Trim() == "}")
                    {
                        find1 = true;
                        continue;
                    }

                    if (find1 == true && lines[i].Trim() == "}") find2 = true;

                }


                System.IO.File.WriteAllLines(path, lines.Reverse());
            }
        }
    }
}
