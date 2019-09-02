using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{

    
    public class IFSmsNumberResult
    {
        public string Number { get; set; }
        public BatchItemState State { get; set; }

        public DateTime? SentDate { get; set; }
    }
}
