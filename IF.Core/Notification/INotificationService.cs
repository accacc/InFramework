using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Notification
{
    public interface INotificationService
    {
        Task<IFNotificationResponse> Notify(IFNotificationRequest request);
    }
}
