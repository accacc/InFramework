using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Rest
{
    public interface  IRestApi
    {
         Response PostAsync<Request, Response>(Request request);
    }
}
