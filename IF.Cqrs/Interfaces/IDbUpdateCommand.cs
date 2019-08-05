using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Handler
{
    public interface IDbUpdateCommand<in TCommand> : IDbCommand where TCommand : BaseCommand
    {
        void Execute(TCommand command);
    }
}
