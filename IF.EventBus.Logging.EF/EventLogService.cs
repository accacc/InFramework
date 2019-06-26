//using IF.Core.Data;
//using IF.Core.EventBus;
//using IF.Core.EventBus.Log;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace IF.EventBus.Logging.EF
//{
//    public class EventLogService : IEventLogService
//    {
//        private readonly EventLogContext _integrationEventLogContext;
//        private readonly string cnnString;

//        public EventLogService(string cnnstring)
//        {
//            this.cnnString = cnnstring ?? throw new ArgumentNullException(nameof(cnnstring));

//            _integrationEventLogContext = new EventLogContext(
//                new DbContextOptionsBuilder<EventLogContext>()
//                    .UseSqlServer(cnnString)                    
//                    //.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
//                    .Options);
//        }

//        public async Task SaveEventAsync(IFEvent @event,string serviceName, EventStateEnum eventState)
//        {


//            try
//            {
//                var eventLogEntry = new EventLogEntry(@event);
//                eventLogEntry.State = eventState;
//                eventLogEntry.ServiceName = serviceName;
//                //_integrationEventLogContext.Database.UseTransaction(transaction);
//                await _integrationEventLogContext.IntegrationEventLogs.AddAsync(eventLogEntry);

//                await _integrationEventLogContext.SaveChangesAsync();
//            }
//            catch (Exception ex)
//            {

//                string exa = ex.GetBaseException().Message;
//            }
//        }

//        //public void  MarkEventAsPublishedAsync(IFEvent @event,string servicename)
//        //{
//        //    try
//        //    {

//        //        var eventLogEntry = _integrationEventLogContext.IntegrationEventLogs.Single(ie => ie.EventId == @event.Id);
//        //        eventLogEntry.TimesSent++;
//        //        eventLogEntry.ServiceName = servicename;
//        //        eventLogEntry.State = EventStateEnum.Published;

//        //        _integrationEventLogContext.IntegrationEventLogs.Update(eventLogEntry);

//        //        _integrationEventLogContext.SaveChanges();
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        string exa = ex.GetBaseException().Message;
//        //    }
//        //}

       

//        //public void ChangeEventState(Guid eventId,string servicename, EventStateEnum eventState)
//        //{
//        //    try
//        //    {

//        //        var eventLogEntry = _integrationEventLogContext.IntegrationEventLogs.SingleOrDefault(ie => ie.EventId == eventId);
//        //        eventLogEntry.TimesSent++;
//        //        eventLogEntry.ServiceName = servicename;
//        //        eventLogEntry.State = eventState;

//        //        _integrationEventLogContext.IntegrationEventLogs.Update(eventLogEntry);

//        //        _integrationEventLogContext.SaveChanges();
//        //    }
//        //    catch (Exception ex)
//        //    {

//        //        string exa = ex.GetBaseException().Message;
//        //    }
//        //}

//        public async Task<List<EventLog>> GetEventLogsBySourceIdAsync(Guid sourceId)
//        {
//            try
//            {

//                var eventLogEntry = await _integrationEventLogContext.IntegrationEventLogs.Cast<EventLog>().Where(ie => ie.SourceId == sourceId).ToListAsync();
//                return eventLogEntry;
//            }
//            catch (Exception ex)
//            {

//                string exa = ex.GetBaseException().Message;
//            }

//            return new List<EventLog>();
//        }


//        public async Task<PagedListResponse<EventLog>> GetPaginatedEventLogsAsync(DateTime BeginDate, DateTime EndDate, int PageNumber = 0, int PageSize = 50)
//        {

//            try
//            {                

//                var query = _integrationEventLogContext.IntegrationEventLogs.Select(s=>new EventLog

//                {
//                    CreationTime = s.CreationTime,
//                    Content = s.Content,
//                    EventId = s.EventId,
//                    EventTypeName = s.EventTypeName,
//                    ServiceName = s.ServiceName,
//                    Id = s.Id,
//                    SourceId = s.SourceId,
//                    State = s.State,
//                    TimesSent = s.TimesSent
//                }
//                ).Where(i => i.CreationTime >= BeginDate && i.CreationTime <= EndDate);

//                var count = await query.CountAsync();

//                var data = await query.OrderByDescending(o=>o.CreationTime).Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync();

//                PagedListResponse<EventLog> pagedList = new PagedListResponse<EventLog>(data, PageNumber, PageSize, count);

//                return pagedList;
//            }
//            catch (Exception ex)
//            {

//                string exa = ex.GetBaseException().Message;
//            }

//            return new PagedListResponse<EventLog>();
//        }
//    }
//}
