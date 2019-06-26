using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Derin.DynamicData
{
    [DataContract]
    public class Filter
    {
        [DataMember(Name = "field")]
        public string Field { get; set; }

        [DataMember(Name = "operator")]
        public string Operator { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "logic")]
        public string Logic { get; set; }

        [DataMember(Name = "ignoreCase")]
        public string IgnoreCase { get; set; }

        [DataMember(Name = "filters")]
        public List<Filter> Filters { get; set; }
    }
}