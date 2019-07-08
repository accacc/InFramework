using IF.Core.Data;
using IF.Core.Log;
using System;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public class MongoLogService : ILogService
    {

        private readonly IMongoLogRepository mongoLogRepository;

        public MongoLogService(IMongoLogRepository mongoLogRepository)
        {
            this.mongoLogRepository = mongoLogRepository;
        }

        public void Error(Exception exception, string logger, string message, string UserId, Guid UniqueId, string IpAdress, string Channel)
        {
            ApplicationErrorLogMongoDB applicationLog = GetLog(exception, logger, message, UserId,UniqueId,IpAdress,Channel);

            this.mongoLogRepository.AddLogAsync(applicationLog);
        }


        public async Task ErrorAsync(Exception exception, string logger, string message, string UserId,Guid UniqueId, string IpAdress, string Channel)
        {
            ApplicationErrorLogMongoDB applicationLog = GetLog(exception, logger, message, UserId,UniqueId,IpAdress, Channel);

            await this.mongoLogRepository.AddLogAsync(applicationLog);


        }

        private static ApplicationErrorLogMongoDB GetLog(Exception exception, string logger, string message, string UserId, Guid UniqueId, string IpAdress, string Channel)
        {
            while (exception.InnerException != null)
                exception = exception.InnerException;

            ApplicationErrorLogMongoDB applicationLog = new ApplicationErrorLogMongoDB();
            applicationLog.Level = "1";
            applicationLog.UniqueId = UniqueId;
            applicationLog.Logger = logger;
            applicationLog.Message = message;
            applicationLog.ExceptionMessage = exception.Message;
            applicationLog.StackTrace = exception.StackTrace;
            applicationLog.UserId = UserId;
            applicationLog.MachineName = Environment.MachineName;
            applicationLog.IPAddress = IpAdress;
            applicationLog.Channel = Channel;
            return applicationLog;
        }

        public void Error(string logger, string message, string UserId, Guid UniqueId,string IpAdress, string Channel)
        {
            ApplicationErrorLogMongoDB applicationLog = GetLog(logger, message, UserId, UniqueId,IpAdress,Channel);

            this.mongoLogRepository.AddLogAsync(applicationLog);
        }

        private static ApplicationErrorLogMongoDB GetLog(string logger, string message, string UserId, Guid UniqueId, string IpAdress, string Channel)
        {
            ApplicationErrorLogMongoDB applicationLog = new ApplicationErrorLogMongoDB();
            applicationLog.Level = "1";
            applicationLog.UniqueId = UniqueId;
            applicationLog.Logger = logger;
            applicationLog.Message = message;
            applicationLog.ExceptionMessage = message;
            applicationLog.StackTrace = "-";
            applicationLog.UserId = UserId;
            applicationLog.MachineName = Environment.MachineName;
            applicationLog.IPAddress = IpAdress;
            return applicationLog;
        }

        public async Task ErrorAsync(string logger, string message, string UserId, Guid UniqueId, string IpAdress, string Channel)
        {
            ApplicationErrorLogMongoDB applicationLog = GetLog(logger, message, UserId, UniqueId,IpAdress, Channel);

            await this.mongoLogRepository.AddLogAsync(applicationLog);
        }

        public void Warn(string logger, string message, string UserId, Guid UniqueId, string IpAdress, string Channel)
        {
            ApplicationErrorLogMongoDB applicationLog = GetLog(logger, message, UserId, UniqueId,IpAdress,Channel);
            applicationLog.Level = "2";

            this.mongoLogRepository.AddLogAsync(applicationLog);
        }

        public async Task WarnAsync(string logger, string message, string UserId, Guid UniqueId, string IpAdress, string Channel)
        {
            ApplicationErrorLogMongoDB applicationLog = GetLog(logger, message, UserId, UniqueId,IpAdress,Channel);
            applicationLog.Level = "2";

            await this.mongoLogRepository.AddLogAsync(applicationLog);
        }

        public void Info(string logger, string message, string UserId, Guid UniqueId, string IpAdress, string Channel)
        {
            ApplicationErrorLogMongoDB applicationLog = GetLog(logger, message, UserId, UniqueId,IpAdress,Channel);

            this.mongoLogRepository.AddLogAsync(applicationLog);

            applicationLog.Level = "3";
        }

        public async Task InfoAsync(string logger, string message, string UserId, Guid UniqueId, string IpAdress, string Channel)
        {
            ApplicationErrorLogMongoDB applicationLog = GetLog(logger, message, UserId, UniqueId,IpAdress,Channel);
            applicationLog.Level = "3";

            await this.mongoLogRepository.AddLogAsync(applicationLog);
        }

        public async Task<string> GetStackTraceAsync(Guid id)
        {
            return await this.mongoLogRepository.GetStackTraceAsync(id);
        }

        public async Task<PagedListResponse<ApplicationErrorLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string userId, string Message, string Source, string Channel, int skipNumber = 0, int takeNumber = 50)
        {
            return await this.mongoLogRepository.GetPaginatedAsync(BeginDate, EndDate, userId, Message, Source, Channel, skipNumber, takeNumber);
        }
    }
}
