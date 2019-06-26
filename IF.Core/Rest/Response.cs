using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Rest
{
    public class Response
    {

        private HttpStatusCode statusCode;


        private HttpContent body;


        private HttpResponseHeaders headers;


        public Response(HttpStatusCode statusCode, HttpContent responseBody, HttpResponseHeaders responseHeaders)
        {
            this.StatusCode = statusCode;
            this.Body = responseBody;
            this.Headers = responseHeaders;
        }


        public HttpStatusCode StatusCode
        {
            get
            {
                return this.statusCode;
            }

            set
            {
                this.statusCode = value;
            }
        }


        public HttpContent Body
        {
            get
            {
                return this.body;
            }

            set
            {
                this.body = value;
            }
        }


        public HttpResponseHeaders Headers
        {
            get
            {
                return this.headers;
            }

            set
            {
                this.headers = value;
            }
        }


        //public virtual async Task<Dictionary<string, dynamic>> DeserializeResponseBodyAsync(HttpContent content)
        //{
        //    var stringContent = await content.ReadAsStringAsync().ConfigureAwait(false);
        //    var dsContent = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(stringContent);
        //    return dsContent;
        //}


        public virtual Dictionary<string, string> DeserializeResponseHeaders(HttpResponseHeaders content)
        {
            var dsContent = new Dictionary<string, string>();
            foreach (var pair in content)
            {
                dsContent.Add(pair.Key, pair.Value.First());
            }

            return dsContent;
        }
    }
}
