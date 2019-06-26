using Derin.Core.Handler;
using IF.Core.Rest;
using IF.Rest.Client;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace IF.Test
{
    public class Test
    {
        public string Name { get; set; }
    }

    class Program
    {


        public static void Main(string[] args)
        {


            Working().Wait();

            //FluentRestApi fluentRestApi = new FluentRestApi();

            //fluentRestApi
            //    .BaseUrl("http://142.93.111.177/api/Test")
            //    .MethodName("Test")
            //    .PostAsync<string,object>("aaa");

            
        }

        public static async Task Working()

        {
            IFRestClient restClient = new IFRestClient();

            BaseCommand test = new BaseCommand();

            test.Id = 112;

            string msg = JsonConvert.SerializeObject(test);

            var url = "http://localhost:2921/api/test";

            var response = await restClient.RequestAsync<object>(RestMethodType.POST, msg,urlPath:url).ConfigureAwait(false);

            url = "http://localhost:2921/api/test/getall";

            JsonDataResult response2 = await restClient.RequestAsync<JsonDataResult>(RestMethodType.GET,null, urlPath: url).ConfigureAwait(false);

        }
    }
}
