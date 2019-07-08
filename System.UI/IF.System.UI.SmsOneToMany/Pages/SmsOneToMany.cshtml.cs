using IF.Core.Data;
using IF.Core.Log;
using IF.Core.Sms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Threading.Tasks;

namespace IF.System.UI.SmsOneToMany.Pages
{

    //[Authorize]

    public class SmsOneToManyModel : PageModel
    {

        private readonly ILogService logService;
        private readonly ISmsLogService auditLogService;

        //public IEnumerable<ApplicationErrorLog> Logs { get; set; }

        public PagedListResponse<ApplicationErrorLog> Logs { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime BeginDate { get; set; }


        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Source { get; set; }

        [BindProperty(SupportsGet = true)]
        public string UserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Channel { get; set; }

        public SmsOneToManyModel(ILogService logservice, IAuditLogService auditLogService)
        {
            this.logService = logservice;
            this.auditLogService = auditLogService;
            this.PageSize = 50;
            this.PageNumber = 1;
            this.BeginDate = DateTime.Now.Date.AddDays(-1);
            this.EndDate = DateTime.Now.Date.AddDays(1);
        }


        public async Task OnGet()
        {

            this.Logs = await logService.GetPaginatedAsync(this.BeginDate, this.EndDate,null,null,null,null, PageNumber, PageSize);

        }

        public async Task OnPost()
        {
            await SetModel();
        }



        public async Task<PartialViewResult> OnGetApplicationLogPartial()
        {

            await SetModel();


            return new PartialViewResult
            {
                ViewName = "_ApplicationLogTable",
                ViewData = new ViewDataDictionary<SmsOneToManyModel>(ViewData, this)
            };
        }      


        private async Task SetModel()
        {
            this.Logs = await logService.GetPaginatedAsync(BeginDate, EndDate, this.UserId, this.Message, this.Source, this.Channel, PageNumber, PageSize = 30);
        }

        public async Task<PartialViewResult> OnGetStackTrace(Guid uniqueId)
        {


            string stackTrace = await this.logService.GetStackTraceAsync(uniqueId);


            return new PartialViewResult
            {
                ViewName = "_StackTrace",
                ViewData = new ViewDataDictionary<string>(ViewData, stackTrace)
            };
        }
    }

}