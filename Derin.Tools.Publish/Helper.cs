using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Tools.Publish
{
    public static class Helper
    {
        public static string GetJsonPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "../../Projects.json";
        }

        
    }
}
