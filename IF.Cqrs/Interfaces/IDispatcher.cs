using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Data
{
    public interface IDispatcher
    {

        void Command<TCommand>(TCommand command) where TCommand : BaseCommand;

        TResponse Query<TRequest, TResponse>(TRequest request) where TResponse : BaseResponse,new() where TRequest : BaseRequest;

        Task CommandAsync<TCommand>(TCommand command) where TCommand : BaseCommand;

        Task<TResponse> QueryAsync<TRequest, TResponse>(TRequest request) where TResponse : BaseResponse, new() where TRequest : BaseRequest;
    }
}
