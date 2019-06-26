using IF.Core.Configuration;
using System;

namespace Derin.Configuration
{

    public class AppSettings : IAppSettings
    {

        private const string MAX_RETRY_COUNT = "MaxRetryCount";
        private const int DEFAULT_MAX_RETRY_COUNT = 5;

        private readonly IConfigurationService configService;
        public AppSettings(IConfigurationService configService)
        {
            this.configService = configService;
        }

        public int MaxRetryCount => configService.GetValue<int>(MAX_RETRY_COUNT, DEFAULT_MAX_RETRY_COUNT);

        public string SchemaPrefix => configService.GetValue<string>("SchemaPrefix");

        public bool SaveSimulateDataASJson => configService.GetValue<bool>("SaveSimulateDataASJson", false);
        public bool SaveAllQueryData => configService.GetValue<bool>("SaveAllQueryData", false);
        public bool SaveAllCommandData => configService.GetValue<bool>("SaveAllCommandData", false);

        public bool IsSimulationData => configService.GetValue<bool>("IsSimulationData",false);


        public int MaxLogFiles => configService.GetValue<int>("MaxLogFiles", 999);

        public string ApplicationCode => configService.GetValue<string>("ApplicationCode", "0");

        public long ArchiveAboveSize => configService.GetValue<long>("ArchiveAboveSize", long.MaxValue);

        public string ArchiveLogFileName => configService.GetValue<string>("ArchiveLogFileName");

        public string JsonPath => AppDomain.CurrentDomain.BaseDirectory + configService.GetValue<string>("JsonPath");
        public string LogFilePath => AppDomain.CurrentDomain.BaseDirectory + configService.GetValue<string>("LogFilePath");
        public string LogFileName => configService.GetValue<string>("LogFileName");

        public string LoginURL => configService.GetValue<string>("LoginURL", "");

        public string DomainName => configService.GetValue<string>("DomainName");

        public string ApplicationErrorMailList => configService.GetValue<string>("ApplicationErrorMailList", "");

        public bool SendMailOnError => configService.GetValue<bool>("SendMailOnError",false);

        public string ApplicationErrorSender => configService.GetValue<string>("ApplicationErrorSender","");


        public string LayoutConfigPath => configService.GetValue<string>("LayoutPath", "Default");

        public string LayoutPath { get { return String.Format("~/Views/Shared/Layouts{0}_Layout.cshtml", this.LayoutConfigPath); } }
        public string GridLayoutPath { get { return String.Format("~/Views/Shared/Layouts{0}_GridLayout.cshtml", this.LayoutConfigPath); } }
        public string DialogFormLayout { get { return String.Format("~/Views/Shared/Layouts{0}_DialogFormLayout.cshtml", this.LayoutConfigPath); } }

        public string PageLayoutPath { get { return String.Format("~/Views/Shared/Layouts{0}_PageLayout.cshtml", LayoutConfigPath); } }

        public string EmptyLayoutPath { get { return String.Format("~/Views/Shared/Layouts{0}_LayoutEmpty.cshtml", LayoutConfigPath); } }

        public string MenuPath { get { return String.Format("~/Views/Shared/Layouts{0}Menu.cshtml", LayoutConfigPath); } }

        public string LoginViewPath { get { return String.Format("~/Views/Shared/Layouts{0}Login.cshtml", LayoutConfigPath); } }

        

      
    }
}
