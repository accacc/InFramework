//using IF.Core.Log;
//using FOFramework.Core.Configuration;
//using NLog;
//using NLog.Common;
//using NLog.Config;
//using NLog.Targets;
//using NLog.Layouts;
//using System.Collections.Generic;

//namespace Derin.Log.NLog
//{
//    public class DbLogJob : ILogJob
//    {
//        private readonly Dictionary<string, string> parameters;
//        private readonly ConnectionStringReader connectionStringReader;

//        public DbLogJob(ConnectionStringReader connectionStringReader, Dictionary<string, string> parameters = null)
//        {
//            this.parameters = parameters;
//            this.connectionStringReader = connectionStringReader;
//        }

//        public void Init()
//        {
//            InternalLogger.LogFile = @"C:\temp\InteralLogs.txt";

//            InternalLogger.LogLevel = LogLevel.Debug;

//            var config = new LoggingConfiguration();

//            var dbTarget = new DatabaseTarget();

//            var cnnString = string.Empty;

//            cnnString = this.connectionStringReader.GetConnectionString();


//            dbTarget.ConnectionString = cnnString;
//            var schemaName = System.Configuration.ConfigurationManager.AppSettings["DebugSchema"].ToString();
//            dbTarget.CommandText = schemaName + ".P_LOG_ERROR_ADD";

//            dbTarget.CommandType = System.Data.CommandType.StoredProcedure;
//            dbTarget.KeepConnection = false;
//            //dbTarget.UseTransactions = true;
//            dbTarget.DBProvider = "Oracle.ManagedDataAccess.Client";


//            dbTarget.Parameters.Add(new DatabaseParameterInfo("pLEVEL", new SimpleLayout("${level}")));
//            dbTarget.Parameters.Add(new DatabaseParameterInfo("pLOGGER", new SimpleLayout("${logger}")));
//            dbTarget.Parameters.Add(new DatabaseParameterInfo("message", new SimpleLayout("${message}")));
//            dbTarget.Parameters.Add(new DatabaseParameterInfo("pMACHINENAME", new SimpleLayout("${machinename}")));
//            dbTarget.Parameters.Add(new DatabaseParameterInfo("pCALLSITE", new SimpleLayout("${callsite:filename=true}")));
//            dbTarget.Parameters.Add(new DatabaseParameterInfo("pTHREADID", new SimpleLayout("${threadid}")));
//            dbTarget.Parameters.Add(new DatabaseParameterInfo("pEXCEPTIONMESSAGE", new SimpleLayout("${exception}")));
//            dbTarget.Parameters.Add(new DatabaseParameterInfo("pSTACKTRACE", new SimpleLayout("${stacktrace}")));
//            dbTarget.Parameters.Add(new DatabaseParameterInfo("pUSER_ID", new SimpleLayout("${mdc:userId}")));

//            config.AddTarget("database", dbTarget);

//            var dbRule = new LoggingRule("*", LogLevel.Trace, dbTarget);

//            config.LoggingRules.Add(dbRule);

//            LogManager.Configuration = config;
//        }
//    }
//}
