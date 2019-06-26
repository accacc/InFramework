using IF.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Threading;

namespace Derin.Log.NLog
{
    public class NLogLogger : ILogService
    {
        private static Logger logger;

        public NLogLogger(ILogJob logJob)
        {
            logJob.Init();
            logger = LogManager.GetCurrentClassLogger();
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            string userId = GetUserId();

            MappedDiagnosticsContext.Set("userId", userId);

            logger.Error(message);

        }

   

        public void Error(string message, System.Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Error(System.Exception exception)
        {

            string userId = GetUserId();

            MappedDiagnosticsContext.Set("userId", userId);

            while (exception.InnerException != null)
                exception = exception.InnerException;

            logger.Error(exception);
        }


        public void Fatal(string message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(System.Exception exception)
        {
            throw new NotImplementedException();
        }

        

        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        private string GetUserId()
        {
            string userId = "";

            if (Thread.CurrentPrincipal.Identity.Name == null) return userId;

            var userData = Thread.CurrentPrincipal.Identity.Name.Split(',');

            if (userData != null && !String.IsNullOrWhiteSpace(userData[0]))
            {

                userId = userData[0].ToString();
            }

            return userId;
        }

        public void Error(Exception exception, string logger, string message, string UserId)
        {
            throw new NotImplementedException();
        }

        public Task ErrorAsync(Exception exception, string logger, string message, string UserId)
        {
            throw new NotImplementedException();
        }
    }
}
