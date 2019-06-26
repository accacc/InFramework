using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IF.Core.Rest
{
    public interface IIFRestClient
    {

        string Version { get; set; }

        string MediaType { get; set; }

        string BaseUrl { get; set; }

        void AddAuthorization(KeyValuePair<string, string> authParams);

        void AddHeader(Dictionary<string, string> requestHeaders);

        Task<Response> MakeRequest(HttpRequestMessage request, CancellationToken cancellationToken = default(CancellationToken));

        Task<Response> RequestAsync(RestMethodType method, string requestBody = null, object queryParams = null, string urlPath = null,Dictionary<string, string> requestHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<T> RequestAsync<T>(RestMethodType method, string requestBody = null, object queryParams = null, string urlPath = null, Dictionary<string, string> requestHeaders = null, CancellationToken cancellationToken = default(CancellationToken));


    }
}
