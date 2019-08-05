using IF.Core.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Cqrs.Exception
{
    public class CommandHandlerNotFoundException : IFApplicationException
    {
    }
}
