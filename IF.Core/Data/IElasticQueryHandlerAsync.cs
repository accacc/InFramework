﻿using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Data
{
    public interface IElasticQueryHandlerAsync<TRequest, TResponse>
       where TRequest : BaseRequest
       where TResponse : BaseResponse
    {
        Task<TResponse> HandleAsync(TRequest request);
    }
}
