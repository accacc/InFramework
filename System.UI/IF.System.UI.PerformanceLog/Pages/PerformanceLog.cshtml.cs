using IF.Core.Performance;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IF.System.UI.NotificationLog.Pages
{

    //[Authorize]
    public class PerformanceLogModel : PageModel
    {
        private readonly IPerformanceLogService performanceLogService;

        public IEnumerable<PerformanceLogLowStat> Stats { get; set; }

        public int Size { get; set; }

        public int Skip { get; set; }

        public string Filter { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public PerformanceLogModel(IPerformanceLogService performanceLogService)
        {
            this.performanceLogService = performanceLogService;
            this.BeginDate = DateTime.Now.Date.AddDays(-15);
            this.EndDate = DateTime.Now.Date;
        }


        public async Task OnGet()
        {
            Stats = await performanceLogService.GetLowPerformanceLogsAsync();
        }
    }
}