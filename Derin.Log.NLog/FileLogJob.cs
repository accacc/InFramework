using IF.Core.Configuration;
using IF.Core.Log;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Derin.Log.NLog
{
    public class FileLogJob : ILogJob
    {

        private readonly IAppSettings appSettings;
        //private readonly Dictionary<string, string> parameters;
        public FileLogJob(IAppSettings appSettings)//, Dictionary<string, string> parameters)
        {
            this.appSettings = appSettings;
            //this.parameters = parameters;
        }

        public void Init()
        {
            var config = new LoggingConfiguration();

            var fileTarget = new FileTarget();
            fileTarget.ArchiveEvery = FileArchivePeriod.Day;
            fileTarget.ArchiveNumbering = ArchiveNumberingMode.Date;
            fileTarget.MaxArchiveFiles = appSettings.MaxLogFiles;
            fileTarget.ConcurrentWrites = true;
            fileTarget.KeepFileOpen = false;
            fileTarget.FileName = appSettings.LogFilePath+ appSettings.LogFileName;
            fileTarget.ArchiveFileName = appSettings.LogFilePath+ appSettings.ArchiveLogFileName;
            fileTarget.ArchiveAboveSize = appSettings.ArchiveAboveSize;
            fileTarget.ArchiveDateFormat = "yyyy-MM-dd";



            fileTarget.Layout = @"Date: ${date} 
                                    Level: ${level} 
                                    Logger: ${logger} 
                                    Message: ${message} 
                                    MachineName: ${machinename} 
                                    Callsite: ${callsite:filename=true} 
                                    ThreadId: ${threadid} 
                                    Exception: ${exception} 
                                    StackTrace: ${stacktrace}: 
                                    UserId: ${mdc:userId}

                                    ----------------------------------------------------------------------------
                                    ";

            var rule = new LoggingRule("*", LogLevel.Trace, fileTarget);
            config.LoggingRules.Add(rule);

            config.AddTarget("file", fileTarget);
            LogManager.Configuration = config;
        }
    }
}
