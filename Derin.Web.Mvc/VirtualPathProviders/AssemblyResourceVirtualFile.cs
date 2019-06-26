using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

namespace Derin.Core.Mvc.VirtualPathProviders
{
    public class AssemblyResourceVirtualFile : VirtualFile
    {

        private string path;

        public AssemblyResourceVirtualFile(string virtualPath)
            : base(virtualPath)
        {
            path = VirtualPathUtility.ToAppRelative(virtualPath);
        }

        public override Stream Open()
        {
            string[] parts = path.Split('/');
            string assemblyName = parts[2];
            string resourceName = parts[3];

            //assemblyName = Path.Combine(PreApplicationInit.PluginFolder.FullName, assemblyName);
            assemblyName = Path.Combine(HttpRuntime.BinDirectory, assemblyName);
            
            byte[] assemblyBytes = System.IO.File.ReadAllBytes(assemblyName);
            Assembly assembly = Assembly.Load(assemblyBytes);
            if (assembly != null)
            {
                return assembly.GetManifestResourceStream(resourceName);
            }

            return null;
        }
    } 
}
