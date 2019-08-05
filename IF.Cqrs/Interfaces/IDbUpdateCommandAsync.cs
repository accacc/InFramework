using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Interfaces
{
    public interface IDbUpdateCommandAsync<in TCommand> : IDbCommandAsync where TCommand : BaseCommand
    {
        Task ExecuteAsync(TCommand command);
    }
}
