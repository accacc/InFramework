using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Email
{
    public class IFEmailRequest : BaseRequest
    {
        public string Subject { get; set; }

        public string Body { get; set; }


        public string From { get; set; }

        public string To { get; set; }

        public string TemplateName { get; set; }

        public object Model { get; set; }

        public bool DontSend { get; set; }

        public Guid SourceId { get; set; }


    }


}
