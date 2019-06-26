//using IF.Core.Email;
//using SendGrid;
//using SendGrid.Helpers.Mail;
//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace IF.Email.SendGrid
//{
//    public class SendGridClientEmailService : IDerinEmailService
//    {

//        private readonly string apiKey;
//        private readonly string from;


//        public SendGridClientEmailService(string apiKey, string from)
//        {
//            this.apiKey = apiKey;
//            this.from = from;

//        }
//        public async Task<DerinEmailResponse> SendEmail(DerinEmailRequest request)
//        {

//            DerinEmailResponse derinEmailResponse = new DerinEmailResponse();

//            try
//            {
                


//                var client = new SendGridClient(apiKey);


//                string fromEmail = this.from;

//                if (!String.IsNullOrWhiteSpace(request.From))
//                {
//                    fromEmail = request.From;
//                }

//                var from = new EmailAddress(fromEmail);
//                var subject = request.Subject;

//                var toList = request.To.Split(';');

//                List<EmailAddress> emailAddresses = new List<EmailAddress>();

//                foreach (var item in toList)
//                {
//                    if (!String.IsNullOrWhiteSpace(item))
//                    {
//                        emailAddresses.Add(new EmailAddress(item));
//                    }
//                }

//                var htmlContent = request.Body;

//                var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, emailAddresses, subject, null, htmlContent);

//                var response = await client.SendEmailAsync(msg);

//                if (response.StatusCode != HttpStatusCode.Accepted)
//                {
//                    derinEmailResponse.IsSuccess = false;
//                    derinEmailResponse.ErrorMessage = "Email service has error :" + response.Body.ReadAsStringAsync().Result;
//                }
//            }
//            catch (Exception ex)
//            {
//                derinEmailResponse.IsSuccess = false;
//                derinEmailResponse.ErrorMessage = "Email service has error :" + ex.GetBaseException().Message;
//            }
            

//            derinEmailResponse.IsSuccess = true;

//            return derinEmailResponse;
//        }
//    }
//}
