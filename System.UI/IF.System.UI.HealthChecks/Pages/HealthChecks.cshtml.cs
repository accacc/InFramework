using IF.HealthChecks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace IF.System.UI.HealthChecks.Pages
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