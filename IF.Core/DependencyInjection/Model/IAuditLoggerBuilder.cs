﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.DependencyInjection.Model
{
    public interface IAuditLoggerBuilder
    {
       IInFrameworkBuilder Builder { get; }
    }
}
