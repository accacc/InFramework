﻿using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public class IFSmsStatusRequest:BaseRequest
    {
        public string IntegrationId { get; set; }
    }
}
