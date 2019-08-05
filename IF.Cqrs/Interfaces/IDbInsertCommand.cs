using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Data
{
    public interface IDbInsertCommand<in TCommand> : IDbCommand where TCommand : BaseCommand
    {
        void Execute(TCommand command);
    }
}
