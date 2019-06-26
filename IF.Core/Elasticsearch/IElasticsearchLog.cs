using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Elasticsearch
{
    public interface IElasticSearchLog
    {
        DateTime Timestamp { get; set; }

        Guid UniqueId { get; set; }
    }
}
