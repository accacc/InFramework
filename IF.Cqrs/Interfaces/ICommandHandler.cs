using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Interfaces
{
    public interface ICommandHandler<TCommand> where TCommand : BaseCommand
    {
        void Handle(TCommand command);
    }
}
