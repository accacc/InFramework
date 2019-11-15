using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator
{
    public class VsFile
    {
        public VsFile()
        {
        }

        public ListFileType FileType { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }

        public string Path { get; set; }

        public string ProjectName { get; set; }


    }
}
