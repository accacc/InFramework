using IF.Core.File;
using System.IO;

namespace Derin.IO
{
    public partial class FileSystem : IFileSystem
    {

        public string DocumentBasePath { get; private set; }

        public FileSystem(string DocumentBasePath)
        {
            this.DocumentBasePath = DocumentBasePath;
            //Log("Init",DocumentBasePath);
        }



        public void CreateDirectory(string path, bool checkIfExist)
        {
            path = this.DocumentBasePath + path;

            Log(path, "CreateDirectory");

            if (checkIfExist)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }


            }
            else
            {
                Directory.CreateDirectory(path);
            }

        }

        public void CreateFile(string path, Stream stream)
        {
            path = this.DocumentBasePath + path;

            Log(path, "CreateFile");

            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));

            FileStream fileStream = File.Create(path, (int)stream.Length);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            fileStream.Write(bytes, 0, bytes.Length);
            fileStream.Close();
        }

        private static void Log(string path, string operation)
        {
            //string filename = "fileSystemLog.txt";

            //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\temp\" + filename, true))
            //{
            //    file.WriteLine(DateTime.Now.ToString() + ":" + operation + ":" + path);
            //    file.WriteLine();

            //}
        }

        public void WriteAllBytes(string path, byte[] bytes)
        {
            Log(path, "WriteAllBytes");
            path = this.DocumentBasePath + path;
            File.WriteAllBytes(path, bytes);
        }

        public void Delete(string path)
        {
            Log(path, "Delete");
            path = this.DocumentBasePath + path;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public byte[] GetFile(string path)
        {

            path = this.DocumentBasePath + path;

            Log(path, "GetFile");

            if (File.Exists(path))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                return fileBytes;
            }
            else
            {
                return null;
            }
        }
    }
}
