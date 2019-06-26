using IF.MongoDB.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.MongoDB
{
    public class LogContext
    {
        private readonly IMongoDatabase _database = null;

        public LogContext(string cnnString, string database)
        {
            var client = new MongoClient(cnnString);
            if (client != null)
                _database = client.GetDatabase(database);
        }

        public IMongoCollection<ApplicationErrorLog> Logs
        {
            get
            {
                return _database.GetCollection<ApplicationErrorLog>("ApplicationLog");
            }
        }

        public IMongoCollection<PerformanceLog> PerformanceLogs
        {
            get
            {
                return _database.GetCollection<PerformanceLog>("PerformanceLogs");
            }
        }

        public IMongoCollection<AuditLog> AuditLogs
        {
            get
            {
                return _database.GetCollection<AuditLog>("AuditLog");
            }
        }

        public IMongoCollection<EmailLog> EmailLogs
        {
            get
            {
                return _database.GetCollection<EmailLog>("EmailLog");
            }
        }

        public IMongoCollection<NotificationLog> NotificationLogs
        {
            get
            {
                return _database.GetCollection<NotificationLog>("NotificationLogs");
            }
        }


        public IMongoCollection<SmsLog> SmsLogs
        {
            get
            {
                return _database.GetCollection<SmsLog>("SmsLogs");
            }
        }

        public IMongoCollection<EventLogMondoDb> EventLogs
        {
            get
            {
                return _database.GetCollection<EventLogMondoDb>("EventLogs");
            }
        }
    }
}
