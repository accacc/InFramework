using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.EventBus.Log
{
    public interface IEventLogService
    {
        Task SaveEventAsync(IFEvent @event, string serviceName, EventStateEnum eventState);

        Task<List<EventLog>> GetEventLogsBySourceIdAsync(Guid sourceId);

        Task<PagedListResponse<EventLog>> GetPaginatedEventLogsAsync(DateTime BeginDate, DateTime EndDate, string ServiceName, string EventType, Guid UniqueId, Guid SourceId, int PageNumber = 0, int PageSize = 50);

    }
}
