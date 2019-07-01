using IF.Core.Rest;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IF.Rest.Client
{
    public class IFFluentRestClient : IIFFluentRestClient
    {
        //private string baseAddress;

        private object queryParameters;

        private string methodName;
        //private string baseUrl;
        //private string apiKey;

        private RestMethodType methodType;

        //private Dictionary<string, string> headers;

        //private KeyValuePair<string, string> authorization;

        private readonly IIFRestClient iFRestClient;

        public IFFluentRestClient(IIFRestClient iFRestClient)
        {
            this.iFRestClient = iFRestClient;
        }



        public IIFFluentRestClient MethodType(RestMethodType method)
        {
            this.methodType = method;
            return this;
        }

        public IIFFluentRestClient BaseUrl(string baseUrl)
        {
            this.iFRestClient.BaseUrl = baseUrl;
            return this;
        }

        public IIFFluentRestClient QueryParameters(object queryParameters)
        {
            this.queryParameters = queryParameters;
            return this;
        }

        //public IIFFluentRestClient BaseUrl(string baseUrl)
        //{
        //    this.baseAddress = baseUrl;
        //    return this;
        //}

        public IIFFluentRestClient MethodName(string MethodName)
        {
            this.methodName = MethodName;
            return this;
        }

        //public IIFFluentRestApi ApiKey(string apiKey)
        //{
        //    this.apiKey = apiKey;
        //    return this;
        //}

        public IIFFluentRestClient Headers(Dictionary<string, string> headers)
        {
            this.iFRestClient.AddHeader(headers);
            return this;
        }

        public async Task<T> RequestAsync<T>(object data=null)
        {
            string msg  = string.Empty;

            if (data != null)
            {
               msg  = JsonConvert.SerializeObject(data);
            }

            var response = await iFRestClient.RequestAsync<T>(this.methodType, msg, queryParams: this.queryParameters, urlPath: "/" + this.methodName).ConfigureAwait(false);

            return response;
        }

        public void AddAuthorization(KeyValuePair<string, string> authorization)
        {
            this.iFRestClient.AddAuthorization(authorization);
        }

    }

}
