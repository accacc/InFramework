﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Sms
{
    public class EmailModel
    {


        //public string Message { get; set; }
        public string Email { get; set; }
        public SmsState State { get; set; }

        //public string CallbackMessage { get; set; }

        public Dictionary<string,string> Parameters { get; set; }
        public DateTime SentDate { get; set; }



    }
}
