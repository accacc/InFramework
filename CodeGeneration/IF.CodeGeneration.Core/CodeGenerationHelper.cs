using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IF.CodeGeneration.Core
{
    public static class CodeGenerationHelper
    {
        public static void AddCodeToClassBottom(string path, string method)
        {
            //var lines = System.IO.File.ReadAllLines(path);

            //if (lines != null)
            //{
            //    lines = lines.Reverse().ToArray();

            //    bool find1 = false;
            //    bool find2 = false;

            //    for (int i = 0; i < lines.Length; i++)
            //    {

            //        if (find1 && find2)
            //        {
            //            lines[i] += method + Environment.NewLine;
            //            break;
            //        }

            //        if (lines[i].Trim() == "") continue;

            //        if (!find1 && lines[i].Trim() == "}")
            //        {
            //            find1 = true;
            //            continue;
            //        }

            //        if (find1 == true && lines[i].Trim() == "}") find2 = true;

            //    }


            //    System.IO.File.WriteAllLines(path, lines.Reverse());
            //}
        }

        //public static void AddCodeToInterfaceBottom(string path, string method)
        //{
        //    var lines = System.IO.File.ReadAllLines(path);

        //    if (lines != null)
        //    {
        //        lines = lines.Reverse().ToArray();

        //        bool find1 = false;

        //        for (int i = 0; i < lines.Length; i++)
        //        {

        //            if (find1)
        //            {
        //                lines[i] += method + Environment.NewLine;
        //                break;
        //            }

        //            if (lines[i].Trim() == "") continue;

        //            if (lines[i].Trim() == "}")
        //            {
        //                find1 = true;
        //                continue;
        //            }


        //        }


        //        System.IO.File.WriteAllLines(path, lines.Reverse());
        //    }
        //}

        public static void AddCodeToClassBottom(string path, string method,string methodName ,string[] parameters)
        {
            //bool methodExist = IsMethodExist(path, methodName,parameters);

            //if (methodExist) throw new Exception("Bu method daha önce eklenmiş");

            //AddCodeToClassBottom(path, method);

        }


     


        public static bool IsMethodExist(string path, string name, string[] parameters)
        {

            using (StreamReader reader = new StreamReader(path))
            {
                var code = reader.ReadToEnd();

                SyntaxTree Tree = CSharpSyntaxTree.ParseText(code);

                SyntaxNode Root = Tree.GetRoot();

                var Methods = Root.DescendantNodes().OfType<MethodDeclarationSyntax>().ToList();

                foreach (var method in Methods)
                {

                    if (method.Identifier.ValueText == name)
                    {
                        int existParameterCount = 0;

                        for (int i = 0; i < method.ParameterList.Parameters.Count; i++)
                        {
                            ParameterSyntax item = method.ParameterList.Parameters[i];

                            if (parameters.Contains(item.Identifier.ValueText)) existParameterCount++;
                        }

                        if (existParameterCount == method.ParameterList.Parameters.Count) return true;

                    }

                }

            }

            return false;
        }
    }
}
