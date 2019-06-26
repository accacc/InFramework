﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace IF.Core.DependencyInjection
{
    public interface IQueryHandlerBuilder
    {
        IQueryHandlerBuilder Decoration(Action<IQueryHandlerDecoratorBuilder> action);

        IInFrameworkBuilder Build(Assembly[] assemblies);
    }
}
