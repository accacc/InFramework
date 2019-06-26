using Derin.Core.Exception;
using Derin.Core.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Rest
{
    public class SimpleRestApi 
    {
        public static K PostAsync<T, K>(T requestType, string apiBaseAdress, string apiMethod, string token)
        {
            K result;

            try
            {

                var client = new HttpClient();

                if (!String.IsNullOrWhiteSpace(token))
                {

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                client.BaseAddress = new Uri(apiBaseAdress);

                var response = client.PostAsync(apiMethod, requestType, new JsonMediaTypeFormatter());
                var jresult = response.Result.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<K>(jresult.Result);
            }
            catch (System.Exception ex)
            {

                throw new WebApiException(new ErrorCode("Api ile iletişim kurulamıyor"));
            }

            return result;
        }

        public static string GetJWTToken(string baseUrl, string method, string userName, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("tr-TR"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(String.Format("{0}?username={1}&password={2}&grant_type={3}", method, userName, password, "password"));
                var result = response.Result.Content.ReadAsStringAsync();
                var token = JsonConvert.SerializeObject(result.Result).TrimStart('"').TrimEnd('"');

                return token;

            }



        }
    }
}
