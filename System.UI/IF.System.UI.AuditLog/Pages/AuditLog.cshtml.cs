using IF.Core.Data;
using IF.Core.EventBus;
using IF.Core.EventBus.Log;
using IF.Core.Log;
using IF.MongoDB;
using IF.MongoDB.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TutumluAnne.Log.AdminUI.Pages
{

//    [Authorize]
    public class AuditLogModel : PageModel
    {
        private readonly IAuditLogService auditLogService;
        private readonly IEventLogService eventLogService;

        public PagedListResponse<IAuditLog> Logs { get; set; }

        



        [BindProperty(SupportsGet = true)]
        public string Source { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; }

        [BindProperty(SupportsGet = true)]

        public DateTime BeginDate { get; set; }

        [BindProperty(SupportsGet = true)]

        public DateTime EndDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string UserId { get; set; }

        public AuditLogModel(IAuditLogService auditLogService)
        {
            this.auditLogService = auditLogService;
            //this.eventLogService = eventLogService;
            this.PageSize = 30;
            this.PageNumber = 1;
            this.BeginDate = DateTime.Now.Date.AddDays(-1);
            this.EndDate = DateTime.Now.Date.AddDays(1);
        }


        public async Task OnGet()
        {

            this.Logs = await auditLogService.GetPaginatedAsync(this.BeginDate, this.EndDate, null, null, PageNumber, PageSize);

        }

        public async Task OnPost()
        {
            await SetModel();
        }



        public async Task<PartialViewResult> OnGetAuditLogPartial()
        {

            await SetModel();

            return new PartialViewResult
            {
                ViewName = "_AuditLogTable",
                ViewData = new ViewDataDictionary<AuditLogModel>(ViewData, this)
            };
        }


      

        private async Task SetModel()
        {
            this.Logs = await auditLogService.GetPaginatedAsync(BeginDate, EndDate, this.Source, this.UserId,this.PageNumber, this.PageSize = 30);
        }

        public async Task<PartialViewResult> OnGetDetail(Guid uniqueId)
        {


            IAuditLog details = await this.auditLogService.GetDetailAsync(uniqueId);

            return new PartialViewResult
            {
                ViewName = "_AuditLogDetail",
                ViewData = new ViewDataDictionary<IAuditLog>(ViewData, details)
            };
        }


        public async Task<PartialViewResult> OnGetEventsPartial(Guid uniqueId)
        {

            List<EventLog> events = await this.eventLogService.GetEventLogsBySourceIdAsync(uniqueId);

            PagedListResponse<EventLog> pagedList = new PagedListResponse<EventLog>(events, 1, 100);

            return new PartialViewResult
            {
                ViewName = "_EventsTable",
                ViewData = new ViewDataDictionary<List<EventLog>>(ViewData, pagedList)
            };
        }
    }
}