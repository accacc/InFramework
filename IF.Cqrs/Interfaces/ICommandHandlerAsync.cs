using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Data
{
    public interface ICommandHandlerAsync<TCommand> where TCommand : BaseCommand
    {
        Task HandleAsync(TCommand command);
    }
}
