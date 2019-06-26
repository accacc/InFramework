﻿using IF.Core.Email;
using IF.Core.SendGrid;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IF.Email.SendGrid
{
    public class SendGridApiEmailService : IDerinEmailService
    {

        private readonly HttpClient httpClient;
        private readonly SendGridEmailSettings settings;
        public SendGridApiEmailService(HttpClient  httpClient, SendGridEmailSettings settings)
        {
            this.httpClient = httpClient;
            this.settings = settings;

        }
        public async Task<DerinEmailResponse> SendEmail(DerinEmailRequest request)
        {
            DerinEmailResponse derinEmailResponse = new DerinEmailResponse();

            try
            {
                

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.settings.ApiKey);

                string fromEmail = this.settings.FromMailAddress;

                if (!String.IsNullOrWhiteSpace(request.From))
                {
                    fromEmail = request.From;
                }

                var input = new SendGridInput
                {
                    from = new SendGridInput.Mail() { email = fromEmail },
                    personalizations = new[] {new SendGridInput.Personalization()
                {

                    to = new[] {new SendGridInput.Mail(){email = request.To } }
                }},
                    subject = request.Subject,
                    content = new[] {new SendGridInput.Content()
                {
                    type = "text/html",
                    value = request.Body
                }}
                };


                var response = await httpClient.PostAsync("https://api.sendgrid.com/v3/mail/send", input, new JsonMediaTypeFormatter());

                if (response.StatusCode != HttpStatusCode.Accepted)
                {
                    derinEmailResponse.IsSuccess = false;
                    derinEmailResponse.ErrorMessage = "Email service has error :" + response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }

            }
            catch (Exception ex)
            {
                derinEmailResponse.FromException(ex);
                derinEmailResponse.IsSent = false;
            }



            return derinEmailResponse;
        }





    }
}
