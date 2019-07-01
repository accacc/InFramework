using IF.Core.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TutumluAnne.Log.AdminUI.Pages
{


    //[Authorize]
    public class EmailLogModel : PageModel
    {

        private readonly IEmailLogService emailLogService;

        public IEnumerable<IEmailLog> Logs { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public string To { get; set; }
        public string Type { get; set; }

        public EmailLogModel(IEmailLogService emailLogService)
        {
            this.emailLogService = emailLogService;
            this.Take = 50;
            this.Skip = 1;
            this.BeginDate = DateTime.Now.Date.AddDays(-15);
            this.EndDate = DateTime.Now.Date.AddDays(1);
        }


        public async Task OnPost(DateTime BeginDate, DateTime EndDate, string To,string type, int skip, int take)
        {
            if (skip >= 1 && take >= 50)
            {
                this.Take = take;
                this.Skip = skip;
                this.BeginDate = BeginDate;
                this.EndDate = EndDate;
                this.To = To;
                this.Type = type;
                var pages = await emailLogService.GetPaginatedAsync(BeginDate, EndDate, To,type, skip, take);

                this.Logs = pages.Data;
            }
        }

        public async Task<PartialViewResult> OnGetBody(Guid uniqueId)
        {


            string body = await this.emailLogService.GetBodyAsync(uniqueId);


            return new PartialViewResult
            {
                ViewName = "_EmailBody",
                ViewData = new ViewDataDictionary<string>(ViewData, body)
            };
        }
    }

}