using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IF.MongoDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TutumluAnne.Log.AdminUI.Pages
{

    //[Authorize]
    public class PerformanceLogModel : PageModel
    {
        private readonly IMongoPerformanceLogRepository performanceLogRepository;

        public IEnumerable<PerformanceLogLowStat> Stats { get; set; }

        public int Size { get; set; }

        public int Skip { get; set; }

        public string Filter { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public PerformanceLogModel(IMongoPerformanceLogRepository performanceLogRepository)
        {
            this.performanceLogRepository = performanceLogRepository;
            this.BeginDate = DateTime.Now.Date.AddDays(-15);
            this.EndDate = DateTime.Now.Date;
        }


        public async Task OnGet(string searchString, int skip, int amount)
        {
            Stats = await performanceLogRepository.GetLowPerformanceLogsAsync();
        }
    }
}