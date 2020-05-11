using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.CodeGeneration.Core
{
    public class FileSystemCodeFormatProvider : CodeFormatProvider
    {
        string rootPath;
        public FileSystemCodeFormatProvider(string rootPath)
        {
            this.rootPath = rootPath;

            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
        }

        public override void FormatCode(CodeTemplate template,string extension,string indent="", string path="")
        {
            path = String.Format("{0}{1}/{2}{3}.{4}",rootPath, path,template.CodeTemplateName,indent,extension);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (StreamWriter sw = File.CreateText(path))
            {
                System.CodeDom.Compiler.IndentedTextWriter indentWriter = new IndentedTextWriter(sw, "    ");

                indentWriter.Indent = 0;
                indentWriter.WriteLine(template.Template);
            }            

        }

        public bool ExploreFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                return false;
            }

            filePath = System.IO.Path.GetFullPath(filePath);
            System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
            return true;
        }

        public bool ExploreDirectory(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                return false;
            }

            
            System.Diagnostics.Process.Start("explorer.exe",path);
            return true;
        }

        public override void FormatCode(string template,string extension,string fileName, string path="")
        {
            path = String.Format(path + "{0}{1}/{2}.{3}",rootPath,path,fileName,extension);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (StreamWriter sw = File.CreateText(path))
            {
                System.CodeDom.Compiler.IndentedTextWriter indentWriter = new IndentedTextWriter(sw, "    ");

                indentWriter.Indent = 0;
                indentWriter.WriteLine(template);
            }
        }
    }
}
