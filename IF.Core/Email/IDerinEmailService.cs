using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Email
{
    public interface IDerinEmailService
    {
        Task<DerinEmailResponse> SendEmail(DerinEmailRequest request);
    }
}
