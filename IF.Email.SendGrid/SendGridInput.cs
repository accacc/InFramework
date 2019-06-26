using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Email.SendGrid
{
    public class SendGridInput
    {
        public Mail from { get; set; }
        public string subject { get; set; }

        public ICollection<Personalization> personalizations { get; set; }
        public ICollection<Content> content { get; set; }

        public class Personalization
        {
            public ICollection<Mail> to { get; set; }
        }
        public class Mail
        {
            public string email { get; set; }
        }

        public class Content
        {
            public string type { get; set; }
            public string value { get; set; }
        }

    }
}
