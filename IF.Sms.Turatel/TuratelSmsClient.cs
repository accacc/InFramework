using IF.Core.Sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IF.Sms.Turatel
{
    public class TuratelSmsClient
    {
        private readonly HttpClient httpClient;

        public TuratelSmsClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("https://processor.smsorigin.com");
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            this.httpClient.MaxResponseContentBufferSize = Int32.MaxValue;
        }

        public async Task<SmsResult> Gonder(XDocument document)
        {
            document.Declaration = new XDeclaration("1.0", "utf-8", null);
            

            var httpContent = new StringContent(document.ToString(), Encoding.UTF8, "text/html");

            var response = await this.httpClient.PostAsync("xml/process.aspx", httpContent);

            var returnValue = response.Content.ReadAsStringAsync().Result;

            var model = new SmsResult { IsSuccess = true };

            if (!response.IsSuccessStatusCode)
            {
                model.IsSuccess = false;
            }
            
            model.Response = returnValue;

            return model;
        }

        private string GetSHA1(string temp)
        {
            using (var sha1 = new System.Security.Cryptography.SHA1Managed())
            {
                var hash = sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(temp));
                var sb = new System.Text.StringBuilder(hash.Length * 2);
                foreach (byte bb in hash)
                {
                    sb.Append(bb.ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
