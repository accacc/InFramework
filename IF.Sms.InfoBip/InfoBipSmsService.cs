using IF.Core.Sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace IF.Sms.InfoBip
{
    public class InfoBipSmsService : IIFSmsOneToManyService
    {

        private string userName;
        private string password;
        private Dictionary<string, string> serviceNumbers;
        private decimal? minCreditNumberCount;
        private HttpClient infoBipApiClient;

        public InfoBipSmsService(string UserName, string Password, Dictionary<string, string> ServiceNumbers, decimal? MinCreditNumberCount)
        {
            this.userName = UserName;
            this.password = Password;
            this.serviceNumbers = ServiceNumbers;
            this.minCreditNumberCount = MinCreditNumberCount;


            infoBipApiClient = GetHttpClient("https://api.infobip.com", UserName, Password);

        }

        public IFSmsResponse SendSms(IFSmsOnetoManyRequest request)
        {
            //var ruleEngine = new InfoBipSmsRuleEngine();

            //ruleEngine.Execute(request);

            return InfoBipSendSms(request.Subject, request.Message, request.Numbers.First());
        }

        private IFSmsResponse InfoBipSendSms(string başlık, string mesaj, string numara)
        {
            var input = new
            {
                from = başlık,
                to = numara,
                text = mesaj
            };

            try
            {
                var response = infoBipApiClient.PostAsync("sms/1/text/single", input, new JsonMediaTypeFormatter()).Result;
                if (!response.IsSuccessStatusCode)
                    return new IFSmsResponse
                    {

                        IsSuccess = false,
                        Code = "-1",
                        ErrorMessage = "",
                        IntegrationId = "-1"
                    };

                var result = new IFSmsResponse();

                dynamic output = response.Content.ReadAsAsync<dynamic>().Result;

                foreach (var itm in output.messages)
                {
                    result = new IFSmsResponse
                    {
                        IsSuccess = true,
                        Code = "200",
                        IntegrationId = itm.messageId.ToString(),
                        ErrorMessage = itm.status.description
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                return new IFSmsResponse
                {
                    IsSuccess = false,
                    Code = "-1",
                    ErrorMessage = ex.Message,
                    IntegrationId = "-1"
                };
            }
        }

        private HttpClient GetHttpClient(string baseUrl, string userName, string password)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.MaxResponseContentBufferSize = Int32.MaxValue;

            // Basic Authentication
            var byt = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", userName, password));
            var base64String = Convert.ToBase64String(byt);
            var authenticationString = "Basic " + base64String;
            client.DefaultRequestHeaders.Add("Authorization", authenticationString);

            return client;
        }


    }
}
