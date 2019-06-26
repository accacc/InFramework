using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IF.Core.Rest
{
 

    public class ReliabilitySettings
    {

        public ReliabilitySettings()
            : this(0, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero)
        {
        }


        public ReliabilitySettings(int maximumNumberOfRetries, TimeSpan minimumBackoff, TimeSpan maximumBackOff, TimeSpan deltaBackOff)
        {
            if (maximumNumberOfRetries < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maximumNumberOfRetries), "maximumNumberOfRetries must be greater than 0");
            }

            if (maximumNumberOfRetries > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(maximumNumberOfRetries), "The maximum number of retries allowed is 5");
            }

            if (minimumBackoff.Ticks < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minimumBackoff), "minimumBackoff must be greater than 0");
            }

            if (maximumBackOff.Ticks < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maximumBackOff), "maximumBackOff must be greater than 0");
            }

            if (maximumBackOff.TotalSeconds > 30)
            {
                throw new ArgumentOutOfRangeException(nameof(maximumBackOff), "maximumBackOff must be less than 30 seconds");
            }

            if (deltaBackOff.Ticks < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(deltaBackOff), "deltaBackOff must be greater than 0");
            }

            if (minimumBackoff.TotalMilliseconds > maximumBackOff.TotalMilliseconds)
            {
                throw new ArgumentOutOfRangeException(nameof(minimumBackoff), "minimumBackoff must be less than maximumBackOff");
            }

            this.MaximumNumberOfRetries = maximumNumberOfRetries;
            this.MinimumBackOff = minimumBackoff;
            this.DeltaBackOff = deltaBackOff;
            this.MaximumBackOff = maximumBackOff;
        }


        public int MaximumNumberOfRetries { get; }


        public TimeSpan MinimumBackOff { get; }


        public TimeSpan MaximumBackOff { get; }


        public TimeSpan DeltaBackOff { get; }
    }

    public class RetryDelegatingHandler : DelegatingHandler
    {
        private static readonly List<HttpStatusCode> RetriableServerErrorStatusCodes =
            new List<HttpStatusCode>()
            {
                HttpStatusCode.InternalServerError,
                HttpStatusCode.BadGateway,
                HttpStatusCode.ServiceUnavailable,
                HttpStatusCode.GatewayTimeout
            };

        private readonly ReliabilitySettings settings;

        public RetryDelegatingHandler(ReliabilitySettings settings)
            : this(new HttpClientHandler(), settings)
        {
        }

        public RetryDelegatingHandler(HttpMessageHandler innerHandler, ReliabilitySettings settings)
            : base(innerHandler)
        {
            this.settings = settings;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (this.settings.MaximumNumberOfRetries == 0)
            {
                return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }

            HttpResponseMessage responseMessage = null;

            var numberOfAttempts = 0;
            var sent = false;

            while (!sent)
            {
                var waitFor = this.GetNextWaitInterval(numberOfAttempts);

                try
                {
                    responseMessage = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

                    ThrowHttpRequestExceptionIfResponseCodeCanBeRetried(responseMessage);

                    sent = true;
                }
                catch (TaskCanceledException)
                {
                    numberOfAttempts++;

                    if (numberOfAttempts > this.settings.MaximumNumberOfRetries)
                    {
                        throw new TimeoutException();
                    }

                    await Task.Delay(waitFor).ConfigureAwait(false);
                }
                catch (HttpRequestException)
                {
                    numberOfAttempts++;

                    if (numberOfAttempts > this.settings.MaximumNumberOfRetries)
                    {
                        throw;
                    }

                    await Task.Delay(waitFor).ConfigureAwait(false);
                }
            }

            return responseMessage;
        }

        private static void ThrowHttpRequestExceptionIfResponseCodeCanBeRetried(HttpResponseMessage responseMessage)
        {
            if (RetriableServerErrorStatusCodes.Contains(responseMessage.StatusCode))
            {
                throw new HttpRequestException(string.Format("Http status code '{0}' indicates server error", responseMessage.StatusCode));
            }
        }

        private TimeSpan GetNextWaitInterval(int numberOfAttempts)
        {
            var random = new Random();

            var delta = (int)((Math.Pow(2.0, numberOfAttempts) - 1.0) *
                               random.Next(
                                   (int)(this.settings.DeltaBackOff.TotalMilliseconds * 0.8),
                                   (int)(this.settings.DeltaBackOff.TotalMilliseconds * 1.2)));

            var interval = (int)Math.Min(this.settings.MinimumBackOff.TotalMilliseconds + delta, this.settings.MaximumBackOff.TotalMilliseconds);

            return TimeSpan.FromMilliseconds(interval);
        }
    }

    public enum RestMethodType
    {

        DELETE,


        GET,


        PATCH,


        POST,


        PUT
    }
}
