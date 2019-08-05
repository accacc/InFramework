using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Handler
{
    public interface ICommandHandler<TCommand> where TCommand : BaseCommand
    {
        void Handle(TCommand command);
    }
}
