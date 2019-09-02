using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public class SmsBatchItemModel: IFBatchItemModel
    {


        public string Message { get; set; }
        public string Number { get; set; }

        public string CallbackMessage { get; set; }

        public DateTime SentDate { get; set; }



    }
}
