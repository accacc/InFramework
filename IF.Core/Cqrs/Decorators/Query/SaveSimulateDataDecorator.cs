//using IF.Core.Configuration;
//using IF.Core.Handler;
//using IF.Core.Json;
//using System;

//namespace IF.Core.Cqrs.Decorators.Query
//{
//    public class SaveSimulateDataDecorator<TRequest, TResponse> : IQueryHandler<TRequest, TResponse>
//        where TRequest : BaseRequest
//        where TResponse : BaseResponse
//    {
//        private readonly IQueryHandler<TRequest, TResponse> queryHandler;
//        private readonly IJsonSerializer jsonConverter;
//        private readonly IAppSettings AppSettings;


//        public SaveSimulateDataDecorator(IQueryHandler<TRequest, TResponse> commandHandler, IJsonSerializer jsonConverter, IAppSettings AppSettings)
//        {
//            this.queryHandler = commandHandler;
//            this.jsonConverter = jsonConverter;
//            this.AppSettings = AppSettings;
//        }

//        public TResponse Handle(TRequest request)
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

//                response = queryHandler.Handle(request);

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
