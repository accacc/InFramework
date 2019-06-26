using IF.Core.Log;
using IF.Core.Elasticsearch;
using IF.Elasticsearch.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Elasticsearch
{
    public class ElasticSearchLogService : ILogService
    {


        private readonly IElasticsearchApplicationLogProvider logger;

        public ElasticSearchLogService(IElasticsearchApplicationLogProvider logger)
        {
            this.logger = logger;
        }



        public void Error(Exception exception, string logger, string message, string UserId, Guid UniqueId,string IpAddress,string Channel)
        {
            ApplicationErrorLog applicationLog = GetLog(exception, logger, message, UserId, UniqueId,IpAddress, Channel);

            this.logger.AddLog(applicationLog);
        }


        public async Task ErrorAsync(Exception exception, string logger, string message, string UserId, Guid UniqueId, string IpAddress, string Channel)
        {
            ApplicationErrorLog applicationLog = GetLog(exception, logger, message, UserId, UniqueId,IpAddress,  Channel);

            await this.logger.AddLogAsync(applicationLog);
        }

        private static ApplicationErrorLog GetLog(Exception exception, string logger, string message, string UserId, Guid UniqueId, string IpAddress, string Channel)
        {
            while (exception.InnerException != null)
                exception = exception.InnerException;

            ApplicationErrorLog applicationLog = new ApplicationErrorLog();
            applicationLog.Level = "1";
            applicationLog.Logger = logger;
            applicationLog.Message = message;
            applicationLog.ExceptionMessage = exception.Message;
            applicationLog.StackTrace = exception.StackTrace;
            applicationLog.UserId = UserId;
            applicationLog.UniqueId = UniqueId;
            applicationLog.IPAddress = IpAddress;
            applicationLog.MachineName = Environment.MachineName;
            applicationLog.Channel = Channel;
            return applicationLog;
        }

        public void Error(string logger, string message, string UserId, Guid UniqueId, string IpAddress, string Channel)
        {
            throw new NotImplementedException();
        }

        public Task ErrorAsync(string logger, string message, string UserId, Guid UniqueId, string IpAddress, string Channel)
        {
            throw new NotImplementedException();
        }

        public void Warn(string logger, string message, string UserId, Guid UniqueId, string IpAddress, string Channel)
        {
            throw new NotImplementedException();
        }

        public Task WarnAsync(string logger, string message, string UserId, Guid UniqueId, string IpAddress, string Channel)
        {
            throw new NotImplementedException();
        }

        public void Info(string logger, string message, string UserId, Guid UniqueId, string IpAddress, string Channel)
        {
            throw new NotImplementedException();
        }

        public Task InfoAsync(string logger, string message, string UserId, Guid UniqueId, string IpAddress, string Channel)
        {
            throw new NotImplementedException();
        }
    }
}
