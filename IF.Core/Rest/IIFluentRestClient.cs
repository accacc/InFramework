using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Rest
{
    public interface  IIFFluentRestClient
    {
        //IIFFluentRestClient BaseUrl(string baseUrl);
        IIFFluentRestClient QueryParameters(object QueryParameters);
        IIFFluentRestClient MethodName(string MethodName);
        IIFFluentRestClient BaseUrl(string BaseUrl);

        IIFFluentRestClient MethodType(RestMethodType method);

        //IIFFluentRestApi ApiKey(string apiKey);

        IIFFluentRestClient Headers(Dictionary<string, string> headers);

        Task<T> RequestAsync<T>(object data=null);

        void AddAuthorization(KeyValuePair<string, string> header);
    }
}
