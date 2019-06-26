using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Derin.Core.Mvc.Security
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class BypassAntiForgeryTokenAttribute : ActionFilterAttribute { }
}
