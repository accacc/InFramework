using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IF.Core.File
{
    public class FileWriter
    {
        private static ReaderWriterLockSlim lock_ = new ReaderWriterLockSlim();

        public static void WriteData(string path, string contents)
        {
            lock_.EnterWriteLock();
            try
            {
                using (StreamWriter sw = System.IO.File.AppendText(path))
                {
                    sw.WriteLineAsync(contents);
                }


                //System.IO.File.AppendAllText(path, contents);
            }
            finally
            {
                lock_.ExitWriteLock();
            }
        }
    }
}
