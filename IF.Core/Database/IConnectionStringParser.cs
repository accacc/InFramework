using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Database
{
    public interface IConnectionStringParser
    {
        ConnectionStringModel Parser(string connectionString);
    }
}
