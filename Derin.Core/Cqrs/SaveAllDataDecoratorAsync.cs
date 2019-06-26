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


    public class SaveAllQueryDataDecoratorAsync<TRequest, TResponse> : IQueryHandlerAsync<TRequest, TResponse> where TRequest : BaseRequest
        where TResponse : BaseResponse
    {
        private readonly IQueryHandlerAsync<TRequest, TResponse> queryHandler;
        private readonly IJsonSerializer jsonConverter;
        private readonly IAppSettings appSettings;


        public SaveAllQueryDataDecoratorAsync(IQueryHandlerAsync<TRequest, TResponse> queryHandler, IJsonSerializer jsonConverter, IAppSettings appSettings)
        {
            this.queryHandler = queryHandler;
            this.jsonConverter = jsonConverter;
            this.appSettings = appSettings;

        }

        public async Task<TResponse> HandleAsync(TRequest request)
        {
            TResponse response = await queryHandler.HandleAsync(request);

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

    public class SaveAllCommandDataDecoratorAsync<TCommand> : ICommandHandlerAsync<TCommand> where TCommand : BaseCommand
    {
        private readonly ICommandHandlerAsync<TCommand> commandHandler;
        private readonly IJsonSerializer jsonConverter;
        private readonly IAppSettings appSettings;

        public SaveAllCommandDataDecoratorAsync(ICommandHandlerAsync<TCommand> commandHandler, IJsonSerializer jsonConverter, IAppSettings appSettings)
        {
            this.commandHandler = commandHandler;
            this.jsonConverter = jsonConverter;
            this.appSettings = appSettings;

        }

        public async Task HandleAsync(TCommand command)
        {
            await commandHandler.HandleAsync(command);

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
