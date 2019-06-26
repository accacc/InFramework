using Derin.Core.Configuration;
using Derin.Core.Handler;
using Derin.Core.Json;
using System;

namespace Derin.Core.Cqrs
{
    public class SaveSimulateJsonDataDecorator<TRequest, TResponse> : IQueryHandler<TRequest, TResponse>
        where TRequest : BaseRequest
        where TResponse : BaseResponse
    {
        private readonly IQueryHandler<TRequest, TResponse> queryHandler;
        private readonly IJsonSerializer jsonConverter;
        private readonly IAppSettings AppSettings;


        public SaveSimulateJsonDataDecorator(IQueryHandler<TRequest, TResponse> commandHandler, IJsonSerializer jsonConverter, IAppSettings AppSettings)
        {
            this.queryHandler = commandHandler;
            this.jsonConverter = jsonConverter;
            this.AppSettings = AppSettings;
        }

        public TResponse Handle(TRequest request)
        {
            TResponse response;

            if (AppSettings.IsSimulationData)
            {

                Type type = typeof(TResponse);

                string name = type.Name;

                response = jsonConverter.Deserialize<TResponse>(System.IO.File.ReadAllText(this.AppSettings.JsonPath+"SimulateData/" + name + ".json"));

            }
            else
            {

                response = queryHandler.Handle(request);

                if (AppSettings.SaveSimulateDataASJson)
                {
                    Type type = typeof(TResponse);

                    string name = type.Name;

                    Derin.Core.File.FileWriter.WriteData(this.AppSettings.JsonPath+"SimulateData/" + name + ".json", jsonConverter.Serialize(response));
                }
            }

            return response;
        }
    }
}
