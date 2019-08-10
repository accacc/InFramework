using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Data
{
    public interface IDataUpdateCommandAsync<in TCommand> : IDataCommandAsync where TCommand : BaseCommand
    {
        Task ExecuteAsync(TCommand command);
    }
}
