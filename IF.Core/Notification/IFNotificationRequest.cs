using IF.Core.Handler;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Notification
{
    public class IFNotificationRequest:BaseRequest
    {       

        public string[] DeviceIds { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

    }


     


   
}
