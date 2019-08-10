using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Data
{
    public interface IDataDeleteCommand<in TCommand> : IDataCommand where TCommand : BaseCommand
    {
        void Execute(TCommand command);
    }
}
