using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.File
{
    public interface IFileSystem
    {
        void CreateFile(string path, Stream stream);

        void CreateDirectory(string path, bool checkIfExist);

        void WriteAllBytes(string path, byte[] bytes);

        void Delete(string path);

        byte[] GetFile(string path);
    }
}
