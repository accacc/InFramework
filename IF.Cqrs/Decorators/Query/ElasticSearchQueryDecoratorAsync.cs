using IF.Core.Handler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Cqrs
{
    public class ElasticSearchQueryDecoratorAsync<TRequest, TResponse> : IQueryHandlerAsync<TRequest, TResponse> where TResponse : BaseResponse, new() where TRequest : BaseRequest
    {
        private readonly IQueryHandlerAsync<TRequest, TResponse> queryHandler;

        IElasticSearchHandlerFactory elasticSearchHandlerFactory;

        public ElasticSearchQueryDecoratorAsync(IQueryHandlerAsync<TRequest, TResponse> queryHandler,IElasticSearchHandlerFactory elasticSearchHandlerFactory)
        {
            this.queryHandler = queryHandler;
            this.elasticSearchHandlerFactory = elasticSearchHandlerFactory;


        }
        public async Task<TResponse> HandleAsync(TRequest request)
        {



            if (request is IQueryElasticsearchable)
            {
                var elasticHandler = this.elasticSearchHandlerFactory.ResolveAsync<TRequest, TResponse>();

                return await elasticHandler.HandleAsync(request);
            }
            else
            {               
                return await queryHandler.HandleAsync(request);
            }



        }
    }
}
