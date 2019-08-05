using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection.Interface
{
    public enum  DependencyScope:int
    {
        Single =1,
        PerScope=2,
        PerRequest =3,
            PerInstance =4

    }
}
