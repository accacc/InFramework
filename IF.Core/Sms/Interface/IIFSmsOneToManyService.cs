using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms.Interface
{
    public interface IIFSmsOneToManyService
    {
        IFSmsResponse SendSms(IFSmsOneToManyRequest request);

    }
}
