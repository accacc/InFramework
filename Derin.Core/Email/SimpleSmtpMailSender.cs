using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Email
{
    public class SimpleSmtpMailSender : ISimpleMailSender
    {
        public void SendMail(string subject,string body ,string fromMail, List<string> toMails)
        {
            if (toMails != null)
            {

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(fromMail);
                mail.Sender = new MailAddress(fromMail);


                foreach (var errorMail in toMails)
                {
                    mail.To.Add(errorMail);
                }

                mail.Body = body;

                mail.IsBodyHtml = false;

                var smtp = new SmtpClient();

                smtp.Send(mail);
            }
        }
    }
}
