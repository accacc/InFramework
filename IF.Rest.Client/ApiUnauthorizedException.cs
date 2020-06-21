using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Rest.Client
{
    public class ApiUnauthorizedException : Exception
    {
        public ApiUnauthorizedException()
        {
        }
        public ApiUnauthorizedException(string message) : base(message)
        {
        }
    }
}
