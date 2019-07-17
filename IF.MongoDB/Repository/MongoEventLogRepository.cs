using IF.Core.Data;
using IF.Core.EventBus;
using IF.Core.EventBus.Log;
using IF.MongoDB.Model;
using IF.MongoDB.Repository.Abstract;
using IF.MongoDB.Repository.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB.Repository
{
    

    public class MongoEventLogRepository : GenericRepository, IMongoEventLogRepository
    {




        public MongoEventLogRepository(string url,string db):base(url, db)
        {

        }


        public Task<List<EventLog>> GetEventLogsBySourceIdAsync(Guid sourceId)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedListResponse<EventLog>> GetPaginatedEventLogsAsync(DateTime BeginDate, DateTime EndDate, string ServiceName, string EventType, Guid EventId, Guid SourceId, int PageNumber = 0, int PageSize = 50)
        {
            var filterBuilder = Builders<EventLogMondoDb>.Filter;
            var start = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day);
            var end = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day);

            var filter = filterBuilder.Gte(x => x.CreationTime, new BsonDateTime(start)) &
             filterBuilder.Lte(x => x.CreationTime, new BsonDateTime(end));


            if (!String.IsNullOrWhiteSpace(EventType))
            {
                var eventTypeFilter = filterBuilder.Eq(i => i.EventTypeName, EventType);
                filter = filter & eventTypeFilter;
            }

            if (!String.IsNullOrWhiteSpace(ServiceName))
            {
                var serviceNameFilter = filterBuilder.Eq(i => i.ServiceName, ServiceName);
                filter = filter & serviceNameFilter;
            }


            var stateFilter = filterBuilder.Eq(i => i.State, EventStateEnum.Publishing);
            filter = filter & stateFilter;

            if (Guid.Empty != EventId)
            {
                var eventId = filterBuilder.Eq(i => i.EventId, EventId);
                filter = filter & eventId;
            }

            if (Guid.Empty != SourceId)
            {
                var sourceId = filterBuilder.Eq(i => i.SourceId, SourceId);
                filter = filter & sourceId;
            }

            if (!String.IsNullOrWhiteSpace(ServiceName))
            {
                var serviceNameFilter = filterBuilder.Eq(i => i.ServiceName, ServiceName);
                filter = filter & serviceNameFilter;
            }


            var fields = Builders<EventLogMondoDb>.Projection.Exclude(e => e.Content).Exclude("_id");

            var list = await this.GetQuery<EventLogMondoDb>().Find(filter).Project<EventLog>(fields).Skip((PageNumber - 1) * PageSize).Limit(PageSize).SortByDescending(s => s.CreationTime).ToListAsync();


            var count = await this.GetQuery<EventLogMondoDb>().CountDocumentsAsync(filter);


            return new PagedListResponse<EventLog>(list, PageNumber, PageSize, count);
        }

        public async Task SaveEventAsync(IFEvent @event, string serviceName, EventStateEnum eventState)
        {

            var eventLogEntry = new EventLogMondoDb(@event);
            eventLogEntry.State = eventState;
            eventLogEntry.ServiceName = serviceName;
            await this.AddAsync(eventLogEntry);


        }
    }
}
