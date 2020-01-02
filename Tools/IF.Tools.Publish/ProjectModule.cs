using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Tools.Publish
{

    public enum ProjectNetCoreVersion
    {
        Version_2_0 = 0,
        Version_2_1 = 1,
        Latest = int.MaxValue
    }
    public class ProjectModule
    {

        public ProjectModule()
        {
            this.UseIn = false;
            
        }

        public string Name { get; set; }
        public string ModuleName { get; set; }

        public string Path { get; set; }

        public ProjectType Type { get; set; }

        public ProjectNetCoreVersion Version { get; set; }

        public bool UseIn { get; set; }


        public static List<ProjectModule> GetModules()

        {
            List<ProjectModule> dllUniqueNames = new List<ProjectModule>();

            //.Net Core

            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Web.Mvc.Kendo", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Web.Mvc.FluentHtml", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Web.Mvc", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Persistence.EF", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Configuration", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Validation.FluentValidation", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.EventBus.RabbitMQ.Integration", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.EventBus.Azure", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Core", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Jwt", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Cqrs", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            //dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Json",Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.DynamicData", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Persistence", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.EventBus.RabbitMQ", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.AutoMapper", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.RazorviewEngine", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.RazorviewEngine.Integration", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Rest.Client", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Elasticsearch", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.HealthChecks", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.HealthChecks.SqlServer", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.HealthChecks.RabbitMQ", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.HealthChecks.Elasticsearch", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.NetCore.Scheduler", Type = ProjectType.Core, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Redis", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.MongoDB", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.MongoDB.Integration", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Dependency.AutoFac", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Email.SendGrid", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Notification.OneSignal", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.HealthChecks.MongoDb", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Swagger.Integration", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });

            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Sms.InfoBip", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Sms.Barabut", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.EventBus.Logging.EF", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Email.SendGrid.Extension", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Notification.OneSignal.Integration", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Sms.Turatel", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Emarsys", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Batch", Type = ProjectType.Standart , Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Batch.MongoDb", Type = ProjectType.Standart , Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Persistence.EF.SqlServer.Integration", Type = ProjectType.Standart , Version = ProjectNetCoreVersion.Version_2_0});
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Persistence.EF.PostgreSql.Integration", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Persistence.EF.Localization", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Localization.Integration", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0 });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Module.Dictionary", Type = ProjectType.Standart, Version = ProjectNetCoreVersion.Version_2_0, Path = "Modules" });
            




            return dllUniqueNames.OrderBy(o => o.ModuleName).ToList();
            

        }
    }
}
