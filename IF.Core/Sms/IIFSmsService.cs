using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Sms
{
    public interface IIFSmsOneToManyService
    {
        IFSmsResponse SendSms(IFSmsOnetoManyRequest request);

    }

    public interface IIFSmsManyToManyService
    {
        IFSmsResponse SendSms(IFSmsManyToManyRequest request);
    }


    public interface IIFSmsOneToManyServiceAsync
    {
        Task<IFSmsResponse> SendSmsAsync(IFSmsOnetoManyRequest request);

    }

    public interface IIFSmsManyToManyServiceAsync
    {
        Task<IFSmsResponse> SendSmsAsync(IFSmsManyToManyRequest request);
    }
}
