//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using IF.Core.Database;
//using IF.Core.Rest;
//using IF.MongoDB;
//using IF.Rest.Client;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.Extensions.Configuration;
//using Microsoft.AspNetCore.Authorization;

//namespace IF.System.UI.Configs.Pages
//{
//    public class ConfigModel
//    {
//        public string Name { get; set; }

//        public string Body { get; set; }
//    }


//    [Authorize]
//    public class ConfigsModel : PageModel
//    {

//        private readonly IIFFluentRestClient restClient;
//        private readonly IConfiguration configuration;

//        public List<ConfigModel> Configs { get; set; }


//        public ConfigsModel(IIFFluentRestClient restClient, IConfiguration configuration)
//        {
//            this.restClient = restClient;
//            this.configuration = configuration;
//            this.Configs = new List<ConfigModel>();
//        }

//        public async Task OnGet()
//        {
//            var baseUrl = this.configuration["HealthCheck:CoreApiUrl"];
//            ConfigModel config = await GetConnectionSettings(baseUrl);
//            this.Configs.Add(config);


//             baseUrl = this.configuration["HealthCheck:EmailApiUrl"];
//            config = await GetConnectionSettings(baseUrl);
//            this.Configs.Add(config);

//            baseUrl = this.configuration["HealthCheck:MarketingUrl"];
//            config = await GetConnectionSettings(baseUrl);
//            this.Configs.Add(config);

//            baseUrl = this.configuration["HealthCheck:SmsApiUrl"];
//            config = await GetConnectionSettings(baseUrl);
//            this.Configs.Add(config);

//            baseUrl = this.configuration["HealthCheck:NotificationUrl"];
//            config = await GetConnectionSettings(baseUrl);
//            this.Configs.Add(config);

//            baseUrl = this.configuration["HealthCheck:SearchingUrl"];
//            config = await GetConnectionSettings(baseUrl);
//            this.Configs.Add(config);



//        }

//        private async Task<ConfigModel> GetConnectionSettings(string baseUrl)
//        {
//            ConfigModel model = new ConfigModel();

//            try
//            {
//                model.Body = await restClient.BaseUrl(baseUrl)
//                .MethodType(IF.Core.Rest.RestMethodType.GET)
//                .MethodName("HealthCheck/GetConfig")
//                .RequestAsync<string>();

//                model.Name = baseUrl;
//            }
//            catch (Exception ex)
//            {


//                model.Name = baseUrl + "Error Occured : " + ex.GetBaseException().Message;
//            }

//            return model;
//        }
//    }
//}