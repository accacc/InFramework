using IF.Core.Handler;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Cqrs
{
    public class Dispatcher : IDispatcher
    {

        Dictionary<Type, Type> queryHandlers;
        Dictionary<Type, Type> commandHandlers;

        Dictionary<Type,object[]> queryDecorators;
        Dictionary<Type,object[]> commandDecorators;




        public Dispatcher(string integrationName)
        {
            this.commandHandlers = RegisterCommandHandlers(integrationName);
            this.queryHandlers = RegisterQueryHandlers(integrationName);
            this.commandDecorators = new Dictionary<Type,object[]>();
            this.queryDecorators = new Dictionary<Type, object[]>();
        }

        public void RegisterCommandDecorators(Type type,params object[] args)
        {
            this.commandDecorators.Add(type,args);
        }

        public void RegisterQueryDecorators(Type type,params object[] args)
        {
            this.queryDecorators.Add(type,args);
        }

        public void Command<TCommand>(TCommand command) where TCommand : BaseCommand
        {

            List<object> instances = new List<object>();

            try
            {
                var requestType = command.GetType();

                var handler = Activator.CreateInstance(commandHandlers[requestType]);

                instances.Add(handler);

                //foreach (var decorator in this.queryDecorators)
                //{
                //    var genericType = decorator.Key.MakeGenericType(typeof(TRequest), typeof(TResponse));
                //    var decoratorInstance = Activator.CreateInstance(genericType,handler);
                //    ((IQueryHandler<TRequest, TResponse>)decoratorInstance).Handle(request);
                //}

                for (int i = this.commandDecorators.Count - 1; i >= 0; i--)
                {
                    var item = commandDecorators.ElementAt(i);

                    if (i == this.commandDecorators.Count - 1)
                    {

                        var genericType = item.Key.MakeGenericType(typeof(TCommand));

                        object[] @params = new object[item.Value.Length + 1];

                        @params[0] = instances[0];

                        for (int p = 0; p < item.Value.Length; p++)
                        {
                            @params[p + 1] = item.Value[p];
                        }

                        var decoratorInstance = Activator.CreateInstance(genericType, @params);

                        instances.Add(decoratorInstance);
                    }
                    else
                    {
                        object[] @params = new object[item.Value.Length + 1];

                        @params[0] = instances[i + 1];

                        for (int p = 0; p < item.Value.Length; p++)
                        {
                            @params[p + 1] = item.Value[p];
                        }

                        var genericType = item.Key.MakeGenericType(typeof(TCommand));
                        var decoratorInstance = Activator.CreateInstance(genericType, @params);
                        instances.Add(decoratorInstance);
                    }

                }
            }
            catch
            {

                throw;
            }




            ((ICommandHandler<TCommand>)instances[instances.Count - 1]).Handle(command);

            //var commandType = command.GetType();
            //var handler = Activator.CreateInstance(commandHandlers[commandType]);
            //((ICommandHandler<TCommand>)handler).Handle(command);
        }


        public TResponse Query<TRequest, TResponse>(TRequest request) where TResponse : BaseResponse, new() where TRequest : BaseRequest
        {

            List<object> instances = new List<object>();

            try
            {
                var requestType = request.GetType();

                var handler = Activator.CreateInstance(queryHandlers[requestType]);                

                instances.Add(handler);

                //foreach (var decorator in this.queryDecorators)
                //{
                //    var genericType = decorator.Key.MakeGenericType(typeof(TRequest), typeof(TResponse));
                //    var decoratorInstance = Activator.CreateInstance(genericType,handler);
                //    ((IQueryHandler<TRequest, TResponse>)decoratorInstance).Handle(request);
                //}

                for (int i = this.queryDecorators.Count - 1; i >= 0; i--)
                {
                    var item = queryDecorators.ElementAt(i);

                    if (i == this.queryDecorators.Count - 1)
                    {

                        var genericType = item.Key.MakeGenericType(typeof(TRequest), typeof(TResponse));

                        object[] @params = new object[item.Value.Length + 1];

                        @params[0] = instances[0];

                        for (int p = 0; p < item.Value.Length; p++)
                        {
                            @params[p + 1] = item.Value[p];
                        }

                        var decoratorInstance = Activator.CreateInstance(genericType,@params);

                        instances.Add(decoratorInstance);
                    }
                    else
                    {
                        object[] @params = new object[item.Value.Length+1] ;

                        @params[0] = instances[i + 1];

                        for (int p = 0; p < item.Value.Length; p++)
                        {
                            @params[p + 1] = item.Value[p];
                        }

                        var genericType = item.Key.MakeGenericType(typeof(TRequest), typeof(TResponse));
                        var decoratorInstance = Activator.CreateInstance(genericType,@params);
                        instances.Add(decoratorInstance);
                    }

                }
            }
            catch (System.Exception)
            {

                throw;
            }




            return ((IQueryHandler<TRequest, TResponse>)instances[instances.Count-1]).Handle(request);
            
        }


        Dictionary<Type, Type> RegisterCommandHandlers(string integrationName)
        {
            Func<Type, bool> isCommandHandler = t =>
              t.GetInterfaces()
               .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>));

            Func<Type, IEnumerable<Tuple<Type, Type>>> collect = t =>
              t.GetInterfaces().Select(i =>
                Tuple.Create(i.GetGenericArguments()[0], t));

            return Assembly.Load(integrationName)
                           .GetTypes()
                           .Where(t => !t.IsAbstract && !t.IsGenericType)
                           .Where(isCommandHandler)
                           .SelectMany(collect)
                           .ToDictionary(x => x.Item1, x => x.Item2);
        }


        Dictionary<Type, Type> RegisterQueryHandlers(string integrationName)
        {
            Func<Type, bool> isCommandHandler = t =>
              t.GetInterfaces()
               .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>));

            Func<Type, IEnumerable<Tuple<Type, Type>>> collect = t =>
              t.GetInterfaces().Select(i =>
                Tuple.Create(i.GetGenericArguments()[0], t));

            return Assembly.Load(integrationName)
                           .GetTypes()
                           .Where(t => !t.IsAbstract && !t.IsGenericType)
                           .Where(isCommandHandler)
                           .SelectMany(collect)
                           .ToDictionary(x => x.Item1, x => x.Item2);
        }

        public Task CommandAsync<TCommand>(TCommand command) where TCommand : BaseCommand
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> QueryAsync<TRequest, TResponse>(TRequest request)
            where TRequest : BaseRequest
            where TResponse : BaseResponse, new()
        {
            throw new NotImplementedException();
        }
    }
}
