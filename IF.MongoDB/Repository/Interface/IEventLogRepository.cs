using IF.Core.Data;
using IF.Core.EventBus;
using IF.Core.EventBus.Log;
using IF.MongoDB.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IF.MongoDB.Repository.Interface
{
    public interface IMongoEventLogRepository :IRepository
    {

        Task SaveEventAsync(IFEvent @event, string serviceName, EventStateEnum eventState);



        Task<List<EventLog>> GetEventLogsBySourceIdAsync(Guid sourceId);

        Task<PagedListResponse<EventLog>> GetPaginatedEventLogsAsync(DateTime BeginDate, DateTime EndDate, string ServiceName, string EventType, Guid UniqueId, Guid SourceId, int PageNumber = 0, int PageSize = 50);
    }
}
