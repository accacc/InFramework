using Derin.Core.Configuration;
using Derin.Core.Handler;
using Derin.Core.Json;
using System;
using System.Threading.Tasks;

namespace Derin.Core.Cqrs
{
    public class SaveSimulateJsonDataDecoratorAsync<TRequest, TResponse> : IQueryHandlerAsync<TRequest, TResponse>
        where TRequest : BaseRequest
        where TResponse : BaseResponse
    {
        private readonly IQueryHandlerAsync<TRequest, TResponse> queryHandler;
        private readonly IJsonSerializer jsonConverter;
        private readonly IAppSettings AppSettings;


        public SaveSimulateJsonDataDecoratorAsync(IQueryHandlerAsync<TRequest, TResponse> queryHandler, IJsonSerializer jsonConverter, IAppSettings AppSettings)
        {
            this.queryHandler = queryHandler;
            this.jsonConverter = jsonConverter;
            this.AppSettings = AppSettings;
        }

        public async Task<TResponse> HandleAsync(TRequest request)
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

                response = await queryHandler.HandleAsync(request);

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
