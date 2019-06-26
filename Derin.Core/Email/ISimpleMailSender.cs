using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Email
{
    public interface ISimpleMailSender
    {
        void SendMail(string subject,string body ,string fromMail, List<string> toMails);
    }
}
