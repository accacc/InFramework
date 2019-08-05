using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Interfaces
{
    public interface ICommandHandlerAsync<TCommand> where TCommand : BaseCommand
    {
        Task HandleAsync(TCommand command);
    }
}
