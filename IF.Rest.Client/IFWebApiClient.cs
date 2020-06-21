using IF.Core.Data;
using IF.Core.Exception;
using IF.Core.Rest;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IF.Rest.Client
{
    

    public partial class IFWebApiClient : IIFWebApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IHostingEnvironment _env;
        private Uri BaseEndpoint { get; set; }

        public string Version { get; set; }


        public string MediaType { get; set; }

        public IFWebApiClient(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContext, IHostingEnvironment env)
        {
            BaseEndpoint = new Uri(configuration["BaseWebApiUrl"]);
            if (BaseEndpoint == null)
            {
                throw new ArgumentNullException("baseEndpoint");
            }
            _httpClient = httpClient;
            _httpContext = httpContext;
            _env = env;
            this.MediaType = "application/json";
        }

        private async Task<T> DoAsMethodAsync<T>(string method, Uri requestUrl, string body, string token)
        {
            HttpResponseMessage response = null;

            addHeaders(token);

            var stringContent = new StringContent(body, UnicodeEncoding.UTF8, "application/json");

            switch (method.ToUpper())
            {
                case "GET":
                    response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
                    break;
                case "POST":
                    response = await _httpClient.PostAsync(requestUrl, stringContent);
                    break;
                case "DELETE":
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = requestUrl,
                        Content = stringContent
                    };
                    response = await _httpClient.SendAsync(request);
                    break;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new ApiUnauthorizedException("Unauthorized");
            }

            var data = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(data);
        }
        private async Task<T> PostDataAsync<T>(Uri requestUrl, MultipartFormDataContent file, string token)
        {
            addHeaders(token);
            var response = await _httpClient.PostAsync(requestUrl, file);
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }
        private async Task<T> GetResult<T>(string path, string queryString = "", string token = "")
        {
            return await DoAsMethodAsync<T>(HttpMethods.Get, CreateRequestUri(path, queryString), "", token);
        }
        private async Task<T> PostResult<T>(string method, string path, string body, string token)
        {
            return await DoAsMethodAsync<T>(method, CreateRequestUri(path), body, token);
        }
        private async Task<T> PostDataResult<T>(string path, MultipartFormDataContent file, string token)
        {
            return await PostDataAsync<T>(CreateRequestUri(path), file, token);
        }
        private Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            relativePath = string.Format(System.Globalization.CultureInfo.InvariantCulture, relativePath);
            var endpoint = new Uri(BaseEndpoint, relativePath);
            var uriBuilder = new UriBuilder(endpoint);

            uriBuilder.Query = queryString;
#if DEBUG
            Console.WriteLine("-------------------");
            Console.WriteLine(uriBuilder.Uri);
            Console.WriteLine("-------------------");
#endif
            return uriBuilder.Uri;
        }
        private void addHeaders(string newToken = "")
        {
            _httpClient.DefaultRequestHeaders.Remove("userIP");
            _httpClient.DefaultRequestHeaders.Add("userIP", _httpContext.HttpContext.GetClientIp());
            _httpClient.DefaultRequestHeaders.Remove("X-Device-OS");
            _httpClient.DefaultRequestHeaders.Add("X-Device-OS", "web");
            _httpClient.DefaultRequestHeaders.Remove("Authorization");

            var acceptEncodingStr = "Accept-Encoding";

            if (_httpClient.DefaultRequestHeaders.Contains(acceptEncodingStr))
            {
                _httpClient.DefaultRequestHeaders.Remove(acceptEncodingStr);
            }

            _httpClient.DefaultRequestHeaders.Add(acceptEncodingStr, "gzip");


            if (newToken != "")
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + newToken);
            }
            else
                if (_httpContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var token = _httpContext.HttpContext.User.GetFromClaim("token");
#if DEBUG
                Console.WriteLine("-----ApiClient----");
                Console.WriteLine("token: " + token);
                Console.WriteLine("---------");
#endif
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public async Task<T> PutAsync<T>(
         string urlPath,
         BaseCommand @object,
         Dictionary<string, string> requestHeaders = null,
         CancellationToken cancellationToken = default(CancellationToken)) where T : BaseCommand
        {
            var requestBody = JsonConvert.SerializeObject(@object);

            var endpoint = this.BuildUrl(this.BaseEndpoint + urlPath);

            return await MakeRequest<T>(RestMethodType.PUT, requestBody, endpoint, cancellationToken);
        }

        public async Task<T> PostAsync<T>(
        string urlPath,
        BaseCommand @object,
        Dictionary<string, string> requestHeaders = null,
        CancellationToken cancellationToken = default(CancellationToken)) where T : BaseCommand
        {

            var requestBody = JsonConvert.SerializeObject(@object);

            var endpoint = this.BuildUrl(this.BaseEndpoint + urlPath);

            return await MakeRequest<T>(RestMethodType.POST, requestBody, endpoint, cancellationToken);
        }

        public async Task<T> DeleteAsync<T>(
         string urlPath,
         BaseCommand @object,
         Dictionary<string, string> requestHeaders = null,
         CancellationToken cancellationToken = default(CancellationToken)) where T : BaseCommand
        {

            var requestBody = JsonConvert.SerializeObject(@object);

            var endpoint = this.BuildUrl(this.BaseEndpoint + urlPath);

            return await MakeRequest<T>(RestMethodType.DELETE, requestBody, endpoint, cancellationToken);
        }

        public async Task<T> GetAsync<T>(
        string urlPath,
        BaseRequest @params,
        Dictionary<string, string> requestHeaders = null,
        CancellationToken cancellationToken = default(CancellationToken)) where T : BaseResponse
        {
            var queryString = JsonConvert.SerializeObject(@params);

            var endpoint = this.BuildUrl(this.BaseEndpoint + urlPath, queryString);

            return await MakeRequest<T>(RestMethodType.GET, null, endpoint, cancellationToken);
        }

        private async Task<T> MakeRequest<T>(RestMethodType method, string requestBody, string endpoint, CancellationToken cancellationToken) where T : BaseResponseContract
        {
            addHeaders();

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

            HttpResponseMessage response = await this._httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {

                throw new ApiUnauthorizedException("Api token error");
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Api error:{response.StatusCode}");
            }

            var webapiresponse = new WebApiResponse(response.StatusCode, response.Content, response.Headers);

            var stringResult = await webapiresponse.Body.ReadAsStringAsync().ConfigureAwait(false);

            T result = JsonConvert.DeserializeObject<T>(stringResult);



            if (result.ExceptionType == ExceptionTypes.Business || result.ExceptionType == ExceptionTypes.Validation)
            {
                if (!result.IsSuccess)
                {
                    string msg = result.ErrorMessage;

                    if (!_env.IsDevelopment())
                        msg = $"Api hatası :{msg}";

                    if (result.Messages != null && result.Messages.Any())
                    {
                        foreach (var item in result.Messages)
                        {
                            msg += Environment.NewLine + item;
                        }
                    }

                    throw new Exception(msg);
                }
            }
            else
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("HttpRequestException - Reason :" + response.ReasonPhrase);
                }
            }

            return await Task.FromResult(result);


        }



        private string BuildUrl(string urlpath, string queryParams = null)
        {
            string url = null;

            //if (this.Version != null)
            //{
            //    url = this.Version + "/" + urlpath;
            //}
            //else
            //{
            url = urlpath;
            //}

            if (queryParams != null)
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
    }
}
