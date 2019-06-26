using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Exception
{
    public interface IDiagnosticMail
    {
        void SendMail(System.Exception exception, object parameter, string userId, string extraInfo = "");
    }

    public class NullDiagnosticMail : IDiagnosticMail
    {
        public void SendMail(System.Exception exception, object parameter, string userId, string extraInfo = "")
        {

        }
    }
}