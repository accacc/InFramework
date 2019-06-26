using IF.Core.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IF.HealthChecks.Checks
{
    public static partial class HealthCheckBuilderExtensions
    {
        // Default URL check

        public static HealthCheckBuilder AddUrlCheck(this HealthCheckBuilder builder, string url, string Name)
        {
            Guard.ArgumentNotNull(nameof(builder), builder);

            return AddUrlCheck(builder, url, Name,builder.DefaultCacheDuration);
        }

        public static HealthCheckBuilder AddUrlCheck(this HealthCheckBuilder builder, string url, string Name, TimeSpan cacheDuration)
            => AddUrlCheck(builder, url, Name,response => UrlChecker.DefaultUrlCheck(response), cacheDuration);

        // Func returning IHealthCheckResult

        public static HealthCheckBuilder AddUrlCheck(this HealthCheckBuilder builder, string url, string Name, Func<HttpResponseMessage, IHealthCheckResult> checkFunc)
        {
            Guard.ArgumentNotNull(nameof(builder), builder);

            return AddUrlCheck(builder, url,Name, checkFunc, builder.DefaultCacheDuration);
        }

        public static HealthCheckBuilder AddUrlCheck(this HealthCheckBuilder builder, string url, string Name,
                                                     Func<HttpResponseMessage, IHealthCheckResult> checkFunc,
                                                     TimeSpan cacheDuration)
        {
            Guard.ArgumentNotNull(nameof(checkFunc), checkFunc);

            return AddUrlCheck(builder, url, Name,response => new ValueTask<IHealthCheckResult>(checkFunc(response)), cacheDuration);
        }

        // Func returning Task<IHealthCheckResult>

        public static HealthCheckBuilder AddUrlCheck(this HealthCheckBuilder builder, string url, string Name, Func<HttpResponseMessage, Task<IHealthCheckResult>> checkFunc)
        {
            Guard.ArgumentNotNull(nameof(builder), builder);

            return AddUrlCheck(builder, url,Name, checkFunc, builder.DefaultCacheDuration);
        }

        public static HealthCheckBuilder AddUrlCheck(this HealthCheckBuilder builder, string url, string Name,
                                                     Func<HttpResponseMessage, Task<IHealthCheckResult>> checkFunc,
                                                     TimeSpan cacheDuration)
        {
            Guard.ArgumentNotNull(nameof(checkFunc), checkFunc);

            return AddUrlCheck(builder, url, Name, response => new ValueTask<IHealthCheckResult>(checkFunc(response)), cacheDuration);
        }

        // Func returning ValueTask<IHealthCheckResult>

        public static HealthCheckBuilder AddUrlCheck(this HealthCheckBuilder builder, string url, string Name, Func<HttpResponseMessage, ValueTask<IHealthCheckResult>> checkFunc)
        {
            Guard.ArgumentNotNull(nameof(builder), builder);

            return AddUrlCheck(builder, url,Name, checkFunc, builder.DefaultCacheDuration);
        }

        public static HealthCheckBuilder AddUrlCheck(this HealthCheckBuilder builder, string url,string Name,
                                                     Func<HttpResponseMessage, ValueTask<IHealthCheckResult>> checkFunc,
                                                     TimeSpan cacheDuration)
        {
            Guard.ArgumentNotNull(nameof(builder), builder);
            Guard.ArgumentNotNullOrEmpty(nameof(url), url);
            Guard.ArgumentNotNull(nameof(checkFunc), checkFunc);

            var urlCheck = new UrlChecker(checkFunc, url);
            builder.AddCheck($"{Name}:{url}", () => urlCheck.CheckAsync(), cacheDuration);
            return builder;
        }
    }
}
