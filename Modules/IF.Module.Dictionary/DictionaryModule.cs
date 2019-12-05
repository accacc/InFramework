using IF.Core.Module;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Module.Dictionary
{
    public class DictionaryModule : IModule
    {

        public static string GetModuleName()
        {
            return "IF.Module.Dictionary";
        }

        public string[] GetAssembilies()
        {
            return new string[] { "IF.Module.Dictionary" ,"IF.Module.Dictionary.Views" };
        }
        public string Title
        {
            get { return "Dictionary"; }
        }

        public string Name
        {
            get { return GetModuleName(); }
        }

        public Version Version
        {
            get { return new Version(1, 0, 0, 0); }
        }

        public string ViewLocationPath
        {
            get { return "~/Views/Dictionary"; }
        }
    }
}
