using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Handler
{
    public interface IDbCommandAsync
    {

    }

    public interface IDbUpdateCommandAsync<in TCommand> : IDbCommandAsync where TCommand : BaseCommand
    {
        Task ExecuteAsync(TCommand command);
    }

    public interface IDbInsertCommandAsync<in TCommand> : IDbCommandAsync where TCommand : BaseCommand
    {
        Task ExecuteAsync(TCommand command);
    }

    public interface IDbDeleteCommandAsync<in TCommand> : IDbCommandAsync where TCommand : BaseCommand
    {
        Task ExecuteAsync(TCommand command);
    }
}
