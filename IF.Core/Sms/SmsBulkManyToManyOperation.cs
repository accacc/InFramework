﻿using IF.Core.Sms.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public class SmsBulkManyToManyOperation: ISmsBulkManyToManyOperation
    {
     
        public string BulkName { get; set; }

        public int SplitBy { get; set; }

        public int Total { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public SmsOperationStatus Status { get; set; }

        public int BatchCount { get; set; }


        public string SenderPrefixName { get; set; }

        public string CallBackPrefixName { get; set; }

        public string CallBackMessageTemplate { get; set; }

        public string CallBackNumberId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }



    }
}
