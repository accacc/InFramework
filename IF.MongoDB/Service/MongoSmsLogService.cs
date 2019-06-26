using IF.Core.Sms;
using IF.MongoDB.Model;
using IF.MongoDB.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB.Service
{

    public class MongoSmsLogService : ISmsLogService
    {

        private readonly IMongoSmsLogRepository smsLogRepository;

        public MongoSmsLogService(IMongoSmsLogRepository smsLogRepository)
        {
            this.smsLogRepository = smsLogRepository;
        }



      
        public async Task LogAsync(string number, string message, DateTime Date, bool IsSent,string Error, string IntegrationId, Guid UniqueId,Guid SourceId)
        {
            SmsLog smsLog = new SmsLog();

            smsLog.Number = number;
            smsLog.Message = message;
            smsLog.Date = Date;
            smsLog.IsSent = IsSent;
            smsLog.UniqueId = UniqueId;
            smsLog.Error = Error;
            smsLog.IntegrationId = IntegrationId;
            smsLog.SourceId = SourceId;

            await this.smsLogRepository.AddLogAsync(smsLog);
        }
    }
}
