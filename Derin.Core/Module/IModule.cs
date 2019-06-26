using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Module
{
    public class IModule
    {
        string Title { get; }
        string Name { get; }
        Version Version { get; }
    }
}
