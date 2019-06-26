using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Elasticsearch.Repository
{
    public interface IBaseDocument
    {
        Guid Id { get; set; }
    }
}
