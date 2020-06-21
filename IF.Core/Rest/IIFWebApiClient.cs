using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IF.Core.Rest
{
    public interface IIFWebApiClient
    {
        Task<T> PostAsync<T>(string urlPath, BaseCommand @object, Dictionary<string, string> requestHeaders = null, CancellationToken cancellationToken = default(CancellationToken)) where T : BaseCommand;
        Task<T> PutAsync<T>(string urlPath, BaseCommand @object, Dictionary<string, string> requestHeaders = null, CancellationToken cancellationToken = default(CancellationToken)) where T : BaseCommand;
        Task<T> DeleteAsync<T>(string urlPath, BaseCommand @object, Dictionary<string, string> requestHeaders = null, CancellationToken cancellationToken = default(CancellationToken)) where T : BaseCommand;

        Task<T> GetAsync<T>(string urlPath, BaseRequest @params, Dictionary<string, string> requestHeaders = null, CancellationToken cancellationToken = default(CancellationToken)) where T : BaseResponse;
    }
}
