using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms.Interface
{
    public interface IIFEmailOneToManyService
    {
        IFSmsResponse SendSms(IFSmsOneToManyRequest request);

    }
}
