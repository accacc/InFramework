using IF.Core.Data;
using IF.Core.Email;
using IF.Core.Log;
using IF.Core.Performance;
using IF.MongoDB.Model;
using System;
using System.Collections.Generic;
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

        public async Task<string> GetBodyAsync(Guid id)
        {
            return await this.mongoEmailLogRepository.GetBodyAsync(id);
        }

        

        public async Task<PagedListResponse<IEmailLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string To, string type, int PageNumber = 0, int PageSize = 50)
        {
            return await this.mongoEmailLogRepository.GetPaginatedAsync(BeginDate, EndDate, To, type, PageNumber, PageSize);
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
