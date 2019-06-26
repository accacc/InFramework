using IF.Core.Email;
using IF.Core.Log;
using IF.Core.Performance;
using IF.MongoDB.Model;
using System;
using System.Threading.Tasks;

namespace IF.MongoDB
{
    public class MongoEmailLogService : IEmailLogService
    {

        private readonly IMongoEmailLogRepository mongoEmailLogRepository;

        public MongoEmailLogService(IMongoEmailLogRepository mongoEmailLogRepository)
        {
            this.mongoEmailLogRepository = mongoEmailLogRepository;
        }

       

        public async Task LogAsync(string From, string To, string Body, DateTime Date, string Type, bool IsSent, string Subject, Guid UniqueId,Guid SourceId)
        {
            EmailLog emailLog = new EmailLog();

            emailLog.From = From;
            emailLog.To = To;
            emailLog.Body = Body;
            emailLog.Date = Date;
            emailLog.Type = Type;
            emailLog.IsSent = IsSent;
            emailLog.Subject = Subject;
            emailLog.UniqueId = UniqueId;
            emailLog.SourceId = SourceId;

            await this.mongoEmailLogRepository.AddLogAsync(emailLog);
        }
    }
}
