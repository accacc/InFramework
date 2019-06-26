//using System;
//using System.Collections.Generic;
//using System.Text;


//using IF.Core.Configuration;
//using IF.Core.Handler;
//using IF.Core.Json;
//using System;
//using System.Threading.Tasks;

//namespace IF.Core.Cqrs.Decorators.Query
//{
//    public class SaveSimulateDataDecoratorAsync<TRequest, TResponse> : IQueryHandlerAsync<TRequest, TResponse>
//            where TRequest : BaseRequest
//            where TResponse : BaseResponse
//    {
//        private readonly IQueryHandlerAsync<TRequest, TResponse> queryHandler;
//        private readonly IJsonSerializer jsonConverter;
//        private readonly IAppSettings AppSettings;


//        public SaveSimulateDataDecoratorAsync(IQueryHandlerAsync<TRequest, TResponse> queryHandler, IJsonSerializer jsonConverter, IAppSettings AppSettings)
//        {
//            this.queryHandler = queryHandler;
//            this.jsonConverter = jsonConverter;
//            this.AppSettings = AppSettings;
//        }

//        public async Task<TResponse> HandleAsync(TRequest request)
//        {
//            TResponse response;

//            if (AppSettings.IsSimulationData)
//            {

//                Type type = typeof(TResponse);

//                string name = type.Name;

//                response = jsonConverter.Deserialize<TResponse>(System.IO.File.ReadAllText(this.AppSettings.JsonPath + "SimulateData/" + name + ".json"));

//            }
//            else
//            {

//                response = await queryHandler.HandleAsync(request);

//                if (AppSettings.SaveSimulateDataASJson)
//                {
//                    Type type = typeof(TResponse);

//                    string name = type.Name;

//                    IF.Core.File.FileWriter.WriteData(this.AppSettings.JsonPath + "SimulateData/" + name + ".json", jsonConverter.Serialize(response));
//                }
//            }

//            return response;
//        }
//    }
//}
