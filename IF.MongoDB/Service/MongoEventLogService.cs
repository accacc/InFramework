using IF.Core.Data;
using IF.Core.EventBus;
using IF.Core.EventBus.Log;
using IF.MongoDB.Model;
using IF.MongoDB.Repository.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB.Service
{
    public class MongoEventLogService : IEventLogService
    {
        private readonly IMongoEventLogRepository repository;
        public MongoEventLogService(IMongoEventLogRepository repository)
        {
            this.repository = repository;
        }

        public Task<List<EventLog>> GetEventLogsBySourceIdAsync(Guid sourceId)
        {
            return this.repository.GetEventLogsBySourceIdAsync(sourceId);
        }

        public Task<PagedListResponse<EventLog>> GetPaginatedEventLogsAsync(DateTime BeginDate, DateTime EndDate, string ServiceName, string EventType, Guid UniqueId, Guid SourceId, int PageNumber = 0, int PageSize = 50)
        {
            return this.repository.GetPaginatedEventLogsAsync(BeginDate, EndDate, ServiceName, EventType, UniqueId, SourceId, PageNumber , PageSize);
        }

        public Task SaveEventAsync(IFEvent @event, string serviceName, EventStateEnum eventState)
        {
            return this.repository.SaveEventAsync(@event, serviceName, eventState);
        }
    }
}
