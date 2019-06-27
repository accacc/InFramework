using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IF.Core.Database;
using IF.Core.Rest;
using IF.MongoDB;
using IF.Rest.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace TutumluAnne.Log.AdminUI.Pages
{

    //[Authorize]
    public class SqlServerModel : PageModel
    {

        private readonly IIFFluentRestClient restClient;
        private readonly IConfiguration configuration;

        public List<ConnectionStringModel> ConnectionStrings { get; set; }

        public SqlServerModel(IIFFluentRestClient restClient, IConfiguration configuration)
        {
            this.restClient = restClient;
            this.configuration = configuration;
            this.ConnectionStrings = new List<ConnectionStringModel>();
        }


        public async Task OnGet(string searchString, int skip, int amount)
        {
            var baseUrl = this.configuration["HealthCheck:CoreApiUrl"];
            ConnectionStringModel connectionString = await GetConnectionSettings(baseUrl);
            this.ConnectionStrings.Add(connectionString);

            baseUrl = this.configuration["HealthCheck:EmailApiUrl"];
            connectionString = await GetConnectionSettings(baseUrl);
            this.ConnectionStrings.Add(connectionString);

            baseUrl = this.configuration["HealthCheck:SmsApiUrl"];
            connectionString = await GetConnectionSettings(baseUrl);
            this.ConnectionStrings.Add(connectionString);

            baseUrl = this.configuration["HealthCheck:MarketingUrl"];
            connectionString = await GetConnectionSettings(baseUrl);
            this.ConnectionStrings.Add(connectionString);


            baseUrl = this.configuration["HealthCheck:NotificationUrl"];
            connectionString = await GetConnectionSettings(baseUrl);
            this.ConnectionStrings.Add(connectionString);



        }

        private async Task<ConnectionStringModel> GetConnectionSettings(string baseUrl)
        {
            ConnectionStringModel model;

            try
            {
                model = await restClient.BaseUrl(baseUrl)
                .MethodType(IF.Core.Rest.RestMethodType.GET)
                .MethodName("HealthCheck/GetDBConfig")
                .RequestAsync<ConnectionStringModel>();
            }
            catch (Exception ex)
            {

                model = new ConnectionStringModel();
                model.Name = baseUrl + "Error Occured : " + ex.GetBaseException().Message;
            }

            return model;
        }
    }
}