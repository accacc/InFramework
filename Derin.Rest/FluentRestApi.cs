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



    public class FluentRestApi : IRestApi
    {
        private string ApiBaseAddress;

        private string MethodName;


        public FluentRestApi(string apiBaseAdress, string apiMethod)
        {
            var builder = new FluentRestApi("aa", "aa");


            this.ApiBaseAddress = apiBaseAdress;
            this.MethodName = apiMethod;
        }

        private TokenRestApi TokenApi { get; set; }

        public FluentRestApi Token(Action<TokenRestApi> token)
        {

            this.TokenApi = new TokenRestApi();
            token(this.TokenApi);
            return this;
        }

        public Response PostAsync<Request, Response>(Request request)
        {
            Response result;

            try
            {

                var client = new HttpClient();

                if (!String.IsNullOrWhiteSpace(this.TokenApi.Token))
                {

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenApi.Token);
                }

                client.BaseAddress = new Uri(this.TokenApi.ApiBaseAddress);

                var response = client.PostAsync(this.TokenApi.MethodName, request, new JsonMediaTypeFormatter());
                var jresult = response.Result.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Response>(jresult.Result);
            }
            catch (System.Exception ex)
            {

                throw new ApiException("Api ile iletişim kurulamıyor" + ex.GetBaseException().Message);
            }

            return result;
        }
    }

    public class TokenRestApi
    {
        internal string apiBaseAddress;

        internal string methodName;

        internal string token;

        internal string userName;

        internal string password;

        //public TokenRestApi(string ApiBaseAddress, string methodName, string token, string userName, string password)
        //{
        //    this.apiBaseAddress = ApiBaseAddress;
        //    this.methodName = methodName;
        //    this.token = token;
        //    this.userName = userName;
        //    this.password = password;
        //}

        public TokenRestApi()
        {

        }
        public TokenRestApi ApiBaseAddress(string ApiBaseAddress)
        {
            this.apiBaseAddress = ApiBaseAddress;
            return this;
        }

        public TokenRestApi MethodName(string methodName)
        {
            this.methodName = methodName;
            return this;
        }

        public TokenRestApi Token(string token)
        {
            this.token = token;
            return this;
        }

        public TokenRestApi UserName(string UserName)
        {
            this.userName = userName;
            return this;
        }
    }
}
