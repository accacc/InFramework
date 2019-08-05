using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Data
{
    public interface IDbDeleteCommandAsync<in TCommand> : IDbCommandAsync where TCommand : BaseCommand
    {
        Task ExecuteAsync(TCommand command);
    }
}
