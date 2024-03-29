﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IF.DynamicData
{

    [DataContract]
    public class DynamicData
    {
        [DataMember(Name = "take")]
        public int? Take { get; set; }

        [DataMember(Name = "skip")]
        public int? Skip { get; set; }

        [DataMember(Name = "page")]
        public int? Page { get; set; }

        [DataMember(Name = "pagesize")]
        public int? PageSize { get; set; }

        [DataMember(Name = "logic")]
        public string Logic { get; set; }

        [DataMember(Name = "filter")]
        public Filter Filter { get; set; }

        [DataMember(Name = "sort")]
        public IEnumerable<Sort> Sort { get; set; }
        
        //[DataMember(Name = "group")]
        //public IEnumerable<GroupObject> Groups { get; set; }

        //[DataMember(Name = "aggregate")]
        //public IEnumerable<AggregateObject> AggregateObjects { get; set; }
    }
}
