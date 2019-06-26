using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Module
{
    public interface IModule
    {
        string Title { get; }
        string Name { get; }
        Version Version { get; }
        string ViewLocationPath { get; }
    }
}
