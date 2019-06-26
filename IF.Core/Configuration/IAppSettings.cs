namespace IF.Core.Configuration
{
    public interface IAppSettingsCore 
    {
        string ApplicationName { get; set; }
        string Version { get; set; }
        string ApplicationCode { get; set; }

    }

    public interface IAppSettings
    {

        int MaxRetryCount { get; }

        int MaxLogFiles { get; }

        string SchemaPrefix { get; }

        string JsonPath { get; }
        string LogFilePath { get; }

        string LayoutPath { get; }

        string PageLayoutPath { get; }
        string DialogFormLayout { get; }

        string EmptyLayoutPath { get; }

        string MenuPath { get; }

        string LayoutConfigPath { get; }

        string LoginURL { get; }

        string LoginViewPath { get; }

        string DomainName { get; }


        string ArchiveLogFileName { get; }
        string LogFileName { get; }

        string ApplicationCode { get; }

        bool SaveSimulateDataASJson { get; }

        bool IsSimulationData { get; }
        bool SaveAllQueryData { get; }
        bool SaveAllCommandData { get; }
        long ArchiveAboveSize { get; }

        string ApplicationErrorMailList { get; }

        bool SendMailOnError { get; }

        string ApplicationErrorSender { get; }

        string GridLayoutPath { get; }
    }
}
