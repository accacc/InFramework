using Derin.Core.Configuration;
using Derin.Core.Handler;
using Derin.Core.Json;
using Derin.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Cqrs
{


    public class SaveAllQueryDataDecorator<TRequest, TResponse> : IQueryHandler<TRequest, TResponse> where TRequest : BaseRequest
        where TResponse : BaseResponse
    {
        private readonly IQueryHandler<TRequest, TResponse> queryHandler;
        private readonly IJsonSerializer jsonConverter;
        private readonly IAppSettings appSettings;


        public SaveAllQueryDataDecorator(IQueryHandler<TRequest, TResponse> queryHandler, IJsonSerializer jsonConverter, IAppSettings appSettings)
        {
            this.queryHandler = queryHandler;
            this.jsonConverter = jsonConverter;
            this.appSettings = appSettings;

        }

        public TResponse Handle(TRequest request)
        {
            TResponse response = queryHandler.Handle(request);

            if (appSettings.SaveAllQueryData)
            {
                var datePattern = DateTime.Now.ToString("yyyyMMdd");
                var date = DateTime.Now.ToString("yyyyMMddTHHmmss");


                Type type = typeof(TRequest);
                string name = type.Name;
                Derin.Core.File.FileWriter.WriteData(this.appSettings.JsonPath + "QueryData-" + datePattern + ".json", date + " " + name + " " + jsonConverter.Serialize(request)+ Environment.NewLine);

                type = typeof(TResponse);
                name = type.Name;
                Derin.Core.File.FileWriter.WriteData(this.appSettings.JsonPath + "QueryData-" + datePattern + ".json",date+" " +name + " " + jsonConverter.Serialize(response)+ Environment.NewLine);
            }

            return response;
        }
    }

    public class SaveAllCommandDataDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : BaseCommand
    {
        private readonly ICommandHandler<TCommand> commandHandler;
        private readonly IJsonSerializer jsonConverter;
        private readonly IAppSettings appSettings;

        public SaveAllCommandDataDecorator(ICommandHandler<TCommand> commandHandler, IJsonSerializer jsonConverter, IAppSettings appSettings)
        {
            this.commandHandler = commandHandler;
            this.jsonConverter = jsonConverter;
            this.appSettings = appSettings;

        }

        public void Handle(TCommand command)
        {
            commandHandler.Handle(command);

            if (appSettings.SaveAllCommandData)
            {
                var datePattern = DateTime.Now.ToString("yyyyMMdd");
                var date = DateTime.Now.ToString("yyyyMMddTHHmmss");


                Type type = typeof(TCommand);

                string name = type.Name;

                Derin.Core.File.FileWriter.WriteData(this.appSettings.JsonPath + "CommandData-" + datePattern + ".json", date + " " + name + " " + jsonConverter.Serialize(command)+ Environment.NewLine);



            }

        }
    }
}
