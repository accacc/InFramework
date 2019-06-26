﻿using Derin.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Configuration
{
    public interface IConnectionStringReaderService: IBaseService
    {
        string GetConnectionString();

        string GetConnectionString(string dbKey,string serverName);
    }
}
