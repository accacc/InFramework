using System;
using System.Collections.Generic;
using System.Text;

namespace Derin.SocialAndCommunication.Sms.Integration.InfoBip
{
    public interface IServiceClient : IDisposable
    {
        TResponse Send<TResponse>(object request);

        void SendOneWay(object request);
    }
}
