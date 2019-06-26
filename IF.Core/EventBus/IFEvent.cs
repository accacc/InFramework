using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.EventBus
{
    public class IFEvent
    {
        public IFEvent(Guid sourceId)
        {
            Id = Guid.NewGuid();
            this.SourceId = sourceId;
            CreationDate = DateTime.Now;
        }

        [JsonConstructor]
        public IFEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        [JsonProperty]
        public Guid SourceId { get; set; }

        [JsonProperty]
        public Guid Id { get; private set; }

        [JsonProperty]
        public DateTime CreationDate { get; private set; }
    }
}
