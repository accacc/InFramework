using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public interface IIFSmsService
    {
        IFSmsResponse SendSms(IFSmsOnetoManyRequest request);
    }
}
