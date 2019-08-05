using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Tools.Publish
{
    public class ProjectModule
    {

        public ProjectModule()
        {
            this.UseIn = false;
        }

        public string Name { get; set; }
        public string ModuleName { get; set; }

        public ProjectType Type { get; set; }

        public bool UseIn { get; set; }


        public static List<ProjectModule> GetModules()

        {
            List<ProjectModule> dllUniqueNames = new List<ProjectModule>();


            dllUniqueNames.Add(new ProjectModule { ModuleName = "Derin.Persistence.EF", Type = ProjectType.Net });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "Derin.Web.Mvc",Type = ProjectType.Net });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "Derin.Web.Mvc.Kendo",Type = ProjectType.Net });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "Derin.Web.Mvc.FluentHtml",Type = ProjectType.Net });
            //dllUniqueNames.Add(new ProjectModule { ModuleName = "Core",Type = ProjectType.Net });
            //dllUniqueNames.Add(new ProjectModule { ModuleName = "Log.NLog",Type = ProjectType.Net });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "Derin.Configuration",Type = ProjectType.Net });
            //dllUniqueNames.Add(new ProjectModule { ModuleName = "Json",Type = ProjectType.Net });
            //dllUniqueNames.Add(new ProjectModule { ModuleName = "Rest",Type = ProjectType.Net });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "Derin.JWT",Type = ProjectType.Net });
            //dllUniqueNames.Add(new ProjectModule { ModuleName = "Integration",Type = ProjectType.Net });
            //dllUniqueNames.Add(new ProjectModule { ModuleName = "DynamicData",Type = ProjectType.Net });
            //dllUniqueNames.Add(new ProjectModule { ModuleName = "WebApi",Type = ProjectType.Net });
            //dllUniqueNames.Add(new ProjectModule { ModuleName = "EventBus.RabbitMQ",Type = ProjectType.Net });

            //.Net Core

            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Web.Mvc.FluentHtml", Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Web.Mvc", Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Persistence.EF.Core", Type = ProjectType.Core });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Configuration",Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Validation.FluentValidation",Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.EventBus.RabbitMQ.Integration",Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.EventBus.Azure",Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Core", Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Cqrs",Type = ProjectType.Standart });
            //dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Json",Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.DynamicData",Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Persistence",Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.EventBus.RabbitMQ",Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.AutoMapper",Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.RazorviewEngine", Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.RazorviewEngine.Integration", Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Rest.Client",Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Elasticsearch",Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.HealthChecks",Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.HealthChecks.SqlServer",Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.HealthChecks.RabbitMQ",Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.HealthChecks.Elasticsearch",Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.NetCore.Scheduler",Type = ProjectType.Core });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Redis",Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.MongoDB", Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.MongoDB.Integration", Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Dependency.AutoFac", Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Email.SendGrid", Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Notification.OneSignal", Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.HealthChecks.MongoDb", Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Swagger.Integration", Type = ProjectType.Standart });

            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Sms.InfoBip", Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Sms.Barabut", Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.EventBus.Logging.EF", Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Email.SendGrid.Extension", Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Notification.OneSignal.Integration", Type = ProjectType.Standart });
            dllUniqueNames.Add(new ProjectModule { ModuleName = "IF.Sms.Turatel", Type = ProjectType.Standart });




            dllUniqueNames.OrderBy(o=>o);




            return dllUniqueNames;



        }
    }



}
