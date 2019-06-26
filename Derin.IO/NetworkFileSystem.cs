using IF.Core.File;
using System.Configuration;
using System.IO;
using System.Net;

namespace Derin.IO
{
    public partial class NetworkFileSystem : FileSystem, IFileSystem
    {

        public string Domain { get { return ConfigurationManager.AppSettings["DocumenatationDomain"]; } }
        public string Username { get { return ConfigurationManager.AppSettings["DocumenatationUsername"]; } }
        public string Password { get { return ConfigurationManager.AppSettings["DocumenatationPassword"]; } }
        public string DocumenatationServer { get { return ConfigurationManager.AppSettings["DocumenatationServer"]; } }

        public NetworkFileSystem(string DocumentBasePath) : base(DocumentBasePath)
        {

        }

        public new void CreateDirectory(string path, bool checkIfExist)
        {
            NetworkCredential NCredentials = new NetworkCredential(Username, Password, Domain);

            using (new NetworkConnection(this.DocumenatationServer, NCredentials))
            {
                base.CreateDirectory(path, checkIfExist);
            }
        }

        public new void CreateFile(string path, Stream stream)
        {
            NetworkCredential NCredentials = new NetworkCredential(Username, Password, Domain);

            using (new NetworkConnection(this.DocumenatationServer, NCredentials))
            {

                base.CreateFile(path, stream);

            }
        }

        public new void WriteAllBytes(string path, byte[] bytes)
        {
            NetworkCredential NCredentials = new NetworkCredential(Username, Password, Domain);

            using (new NetworkConnection(this.DocumenatationServer, NCredentials))
            {
                base.WriteAllBytes(path, bytes);

            }
        }

        public new void Delete(string path)
        {

            NetworkCredential NCredentials = new NetworkCredential(Username, Password, Domain);

            using (new NetworkConnection(this.DocumenatationServer, NCredentials))
            {
                base.Delete(path);

            }
        }

        public new byte[] GetFile(string path)
        {
            NetworkCredential NCredentials = new NetworkCredential(Username, Password, Domain);

            using (new NetworkConnection(this.DocumenatationServer, NCredentials))
            {
                return base.GetFile(path);
            }
        }
    }
}
