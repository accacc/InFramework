using Derin.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Configuration
{
    public interface IAppSettings : IBaseService
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
