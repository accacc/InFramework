using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IF.HealthChecks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TutumluAnne.Log.AdminUI.Pages
{

    //[Authorize]
    public class HealthCheckModel : PageModel
    {
        private readonly IHealthCheckService _healthCheck;

        public CompositeHealthCheckResult HealthCheck { get; set; }

        public TimeSpan ExecutionTime { get; set; }

        public HealthCheckModel(IHealthCheckService healthCheck)
        {
            _healthCheck = healthCheck;
        }

        public async Task OnGet()
        {
            var timedTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(3));
            var stopwatch = Stopwatch.StartNew();
            this.HealthCheck = await _healthCheck.CheckHealthAsync(timedTokenSource.Token);
            this.ExecutionTime = stopwatch.Elapsed;
            ViewData["RefreshSeconds"] = 10;
        }
    }
}