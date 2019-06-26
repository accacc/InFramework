using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Web;
using System.Web.Caching;
using System.Collections;
using System.IO;
using System.Reflection;


namespace Derin.Core.Mvc.VirtualPathProviders
{
    public class AssemblyResourceProvider : System.Web.Hosting.VirtualPathProvider
    {


        private bool IsAppResourcePath(string virtualPath)
        {
            String checkPath = VirtualPathUtility.ToAppRelative(virtualPath);
            return checkPath.StartsWith("~/Plugins/", StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool FileExists(string virtualPath)
        {
            if (IsAppResourcePath(virtualPath) && !virtualPath.Contains("ViewStart"))
            {
                if (IsAppResourceExisted(virtualPath))
                    return true;
                else
                {
                    return false;
                }
            }
            else
                return base.FileExists(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsAppResourcePath(virtualPath))
            {
                return new AssemblyResourceVirtualFile(virtualPath);
            }
            else
                return base.GetFile(virtualPath);
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (IsAppResourcePath(virtualPath))
            {
                return null;
            }
            return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        private bool IsAppResourceExisted(string virtualPath)
        {
            string path = VirtualPathUtility.ToAppRelative(virtualPath);
            string[] parts = path.Split('/');
            string assemblyName = parts[2];
            string resourceName = parts[3];

            assemblyName = Path.Combine(HttpRuntime.BinDirectory, assemblyName);
          
            if (!System.IO.File.Exists(assemblyName))  return false;
            
            byte[] assemblyBytes = System.IO.File.ReadAllBytes(assemblyName);
            
            Assembly assembly = Assembly.Load(assemblyBytes);

            if (assembly != null)
            {
                string[] resourceList = assembly.GetManifestResourceNames();
                bool found = Array.Exists(resourceList, r => r.Equals(resourceName));

                return found;
            }
            return false;
        }
    }
}
