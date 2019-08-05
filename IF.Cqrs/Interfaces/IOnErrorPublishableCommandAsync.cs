using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Interfaces
{
    public interface IOnErrorPublishableCommandAsync<T> : ICommandHandlerAsync<T> where T : BaseCommand
    {
        Task PublishAsync(T command);
    }
}
