﻿using IF.Core.Sms;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.MongoDB.Service
{
    public class MongoSmsBatchService : ISmsBatchService
    {
        public IFSmsResponse Send(IFSmsOnetoManyRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
