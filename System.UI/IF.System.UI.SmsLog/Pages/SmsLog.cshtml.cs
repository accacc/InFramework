using IF.Core.Sms;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TutumluAnne.Log.AdminUI.Pages
{
    public class SmsLogModel : PageModel
    {
        private readonly ISmsLogService smsLogService;

        public IEnumerable<ISmsLog> Logs { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Number { get; set; }
        //public string Type { get; set; }

        public SmsLogModel(ISmsLogService smsLogService)
        {
            this.smsLogService = smsLogService;
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
                var pages = await smsLogService.GetPaginatedAsync(BeginDate, EndDate, Number,skip, take);

                this.Logs = pages.Data;
            }
        }
    }
}