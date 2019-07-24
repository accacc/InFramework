using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Sms.Interface
{
    public interface IIFSmsStatusServiceAsync
    {
        Task<IFSmsStatusResponse> GetSmsStatusAsync(IFSmsStatusRequest request);

    }
}
