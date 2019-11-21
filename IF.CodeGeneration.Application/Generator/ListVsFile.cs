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

        VSFileType FileType { get; set; }
    }

    //public class IFVsFile:IVsFile
    //{
    //    public IFVsFile()
    //    {
    //    }

    //    //public VSFileType FileType { get; set; }
    //    public string FileName { get; set; }
    //    public string FileExtension { get; set; }

    //    public string Path { get; set; }

    //    public string ProjectName { get; set; }
    //    public string FileType { get; set; }


    //}


    public class IFVsFile : IVsFile
    {
        public IFVsFile()
        {
        }

        public VSFileType FileType { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }

        public string Path { get; set; }

        public string ProjectName { get; set; }
        


    }

    //public class AddVsFile : IVsFile
    //{
    //    public AddVsFile()
    //    {
    //    }

    //    //public AddFileType FileType { get; set; }
    //    public string FileName { get; set; }
    //    public string FileExtension { get; set; }

    //    public string Path { get; set; }

    //    public string ProjectName { get; set; }

    //    public string FileType { get; set; }


    //}

    //public class IFVsFile : IVsFile
    //{
    //    public IFVsFile()
    //    {
    //    }

    //    //public UpdateFileType FileType { get; set; }
    //    public string FileName { get; set; }
    //    public string FileExtension { get; set; }

    //    public string Path { get; set; }

    //    public string ProjectName { get; set; }

    //    public string FileType { get; set; }

    //}

    //public class GetVsFile : IVsFile
    //{
    //    public GetVsFile()
    //    {
    //    }

    //    //public GetFileType FileType { get; set; }
    //    public string FileName { get; set; }
    //    public string FileExtension { get; set; }

    //    public string Path { get; set; }

    //    public string ProjectName { get; set; }

    //    public string FileType { get; set; }

    //}
}
