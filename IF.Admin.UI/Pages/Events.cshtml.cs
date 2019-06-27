using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IF.Core.Data;
using IF.Core.EventBus;
using IF.Core.EventBus.Log;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace TutumluAnne.Log.AdminUI.Pages
{
    public class EventsModel : PageModel
    {
        private readonly IEventLogService eventLogService;

        public EventsModel(IEventLogService eventLogService)
        {
            this.eventLogService = eventLogService;
            this.PageSize = 30;
            this.PageNumber = 1;
            this.BeginDate = DateTime.Now.Date.AddDays(-1);
            this.EndDate = DateTime.Now.Date.AddDays(1);
        }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime BeginDate { get; set; }


        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string EventType { get; set; }


        [BindProperty(SupportsGet = true)]
        public string ServiceName { get; set; }


        //[BindProperty(SupportsGet = true)]
        //public string State { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid UniqueId { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid SourceId { get; set; }








        public PagedListResponse<EventLog> Events { get; set; }


        public async Task OnGet()
        {

            await this.SetModel();

        }

        public async Task OnPost()
        {
            await SetModel();
        }



        public async Task<PartialViewResult> OnGetEventsPartial()
        {

            await SetModel();


            return new PartialViewResult
            {
                ViewName = "_EventsTable",
                ViewData = new ViewDataDictionary<EventsModel>(ViewData, this)
            };
        }


        private async Task SetModel()
        {
            this.Events = await this.eventLogService.GetPaginatedEventLogsAsync(this.BeginDate,this.EndDate,this.ServiceName, this.EventType.ToString(),this.UniqueId,this.SourceId,this.PageNumber,this.PageSize);
        }
    }
}