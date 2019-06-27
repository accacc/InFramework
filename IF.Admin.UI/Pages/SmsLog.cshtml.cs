using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IF.MongoDB.Model;
using IF.MongoDB.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TutumluAnne.Log.AdminUI.Pages
{
    public class SmsLogModel : PageModel
    {
        private readonly IMongoSmsLogRepository _logger;

        public IEnumerable<SmsLog> Logs { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Number { get; set; }
        //public string Type { get; set; }

        public SmsLogModel(IMongoSmsLogRepository _logger)
        {
            this._logger = _logger;
            this.Take = 50;
            this.Skip = 1;
            this.BeginDate = DateTime.Now.Date.AddDays(-15);
            this.EndDate = DateTime.Now.Date.AddDays(1);
        }


        public async Task OnPost(DateTime BeginDate, DateTime EndDate, string Number, int skip, int take)
        {
            if (skip >= 1 && take >= 50)
            {
                this.Take = take;
                this.Skip = skip;
                this.BeginDate = BeginDate;
                this.EndDate = EndDate;
                this.Number = Number;
                //this.Type = type;
                var pages = await _logger.GetPaginatedAsync(BeginDate, EndDate, Number,skip, take);

                this.Logs = pages.Data;
            }
        }
    }
}