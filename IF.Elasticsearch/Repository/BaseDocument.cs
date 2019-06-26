using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Elasticsearch.Repository
{
   
    public abstract class BaseDocument : IBaseDocument
    {

        [Keyword]

        public Guid Id { get; set; }
    }

}
