using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator
{
    public interface IVsFile
    {
        string FileName { get; set; }
        string FileExtension { get; set; }

        string Path { get; set; }

        string ProjectName { get; set; }
    }

    public class ListVsFile:IVsFile
    {
        public ListVsFile()
        {
        }

        public ListFileType FileType { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }

        public string Path { get; set; }

        public string ProjectName { get; set; }


    }

    public class AddVsFile : IVsFile
    {
        public AddVsFile()
        {
        }

        public AddFileType FileType { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }

        public string Path { get; set; }

        public string ProjectName { get; set; }


    }

    public class UpdateVsFile : IVsFile
    {
        public UpdateVsFile()
        {
        }

        public UpdateFileType FileType { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }

        public string Path { get; set; }

        public string ProjectName { get; set; }


    }
}
