using IF.Core.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IF.Rest.Client
{
    public class IFRestClient : IIFRestClient
    {
        private HttpClient client;

        public string BaseUrl { get; set; }

        //public IFRestClient(string baseUrl, HttpClient httpClient)
        //{
        //    this.client = httpClient;
        //    this.BaseUrl = baseUrl;
        //}


        public IFRestClient(HttpClient httpClient)
        {
            this.client = httpClient;
        }

        public string Version { get; set; }

      
        public string MediaType { get; set; }
        

        public void AddAuthorization(KeyValuePair<string, string> header)
        {
            string[] split = header.Value.Split();
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(split[0], split[1]);
        }

      
        public async Task<Response> MakeRequest(HttpRequestMessage request, CancellationToken cancellationToken = default(CancellationToken))
        {
            HttpResponseMessage response = await this.client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            return new Response(response.StatusCode, response.Content, response.Headers);
        }


        public async Task<Response> RequestAsync(
            RestMethodType method,
            string requestBody,
            object queryParams = null,
            string urlPath = null,
            Dictionary<string, string> requestHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {

            this.AddHeader(requestHeaders);

            var @params = String.Empty;

            if (queryParams != null)
            {
                @params = JsonConvert.SerializeObject(queryParams);
            }

            var endpoint = this.BuildUrl(this.BaseUrl + urlPath, @params);

            StringContent content = null;

            if (requestBody != null)
            {
                content = new StringContent(requestBody, Encoding.UTF8, this.MediaType);
            }            

            var request = new HttpRequestMessage
            {
                Method = new HttpMethod(method.ToString()),
                RequestUri = new Uri(endpoint),
                Content = content
            };

            return await this.MakeRequest(request, cancellationToken).ConfigureAwait(false);
        }


        public async Task<T> RequestAsync<T>(
          RestMethodType method,
          string requestBody,
          object queryParams = null,
          string urlPath = null,
          Dictionary<string, string> requestHeaders = null,
          CancellationToken cancellationToken = default(CancellationToken))
        {

            Response response = await this.RequestAsync(method, requestBody, queryParams, urlPath, requestHeaders, cancellationToken);

            var stringResult = await response.Body.ReadAsStringAsync().ConfigureAwait(false);

            return await Task.FromResult(JsonConvert.DeserializeObject<T>(stringResult));
        }


        public void AddHeader(Dictionary<string, string> requestHeaders = null)
        {

            if (requestHeaders != null)
            {
                requestHeaders.Add("Content-Type", "application/json");
                requestHeaders.Add("Accept", "application/json");
            }
            else
            {
                requestHeaders = new Dictionary<string, string>
                    {
                        //{ "Authorization", "Bearer " + apiKey },
                        { "Content-Type", "application/json" },
                        { "Accept", "application/json" }
                    };
            }


            foreach (var header in requestHeaders)
            {
                if (header.Key == "Content-Type")
                {
                    this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header.Value));
                    this.MediaType = header.Value;
                }
                else
                {
                    this.client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
        }

        private Dictionary<string, List<object>> ParseJson(string json)
        {
            var dict = new Dictionary<string, List<object>>();

            using (var sr = new StringReader(json))
            using (var reader = new JsonTextReader(sr))
            {
                var propertyName = string.Empty;
                while (reader.Read())
                {
                    switch (reader.TokenType)
                    {
                        case JsonToken.PropertyName:
                            {
                                propertyName = reader.Value.ToString();
                                if (!dict.ContainsKey(propertyName))
                                {
                                    dict.Add(propertyName, new List<object>());
                                }

                                break;
                            }

                        case JsonToken.Boolean:
                        case JsonToken.Integer:
                        case JsonToken.Float:
                        case JsonToken.Bytes:
                        case JsonToken.String:
                        case JsonToken.Date:
                            {
                                dict[propertyName].Add(reader.Value);
                                break;
                            }
                    }
                }
            }

            return dict;
        }

        private string BuildUrl(string urlpath, string queryParams = null)
        {
            string url = null;

            if (this.Version != null)
            {
                url = this.Version + "/" + urlpath;
            }
            else
            {
                url = urlpath;
            }

            if (!String.IsNullOrWhiteSpace(queryParams))
            {
                var ds_query_params = this.ParseJson(queryParams);
                string query = "?";
                foreach (var pair in ds_query_params)
                {
                    foreach (var element in pair.Value)
                    {
                        if (query != "?")
                        {
                            query = query + "&";
                        }

                        query = query + pair.Key + "=" + element;
                    }
                }

                url = url + query;
            }

            return url;
        }
    }

    

  

    
}
