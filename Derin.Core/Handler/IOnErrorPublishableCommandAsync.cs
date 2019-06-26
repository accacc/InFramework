using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Handler
{
    public interface IOnErrorPublishableCommandAsync<T> : ICommandHandlerAsync<T> where T : BaseCommand
    {
        Task PublishAsync(T command);
    }
}
