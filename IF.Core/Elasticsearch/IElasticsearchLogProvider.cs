using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Elasticsearch
{

   
    public interface IElasticSearchLogProvider<T>
    {
        Task AddLogAsync(T log);

        void AddLog(T log);

        IEnumerable<T> QueryLogs(string filter = "*", ElasticsearchLogPaging paging = null);

        long Count(string filter);
    }
}
