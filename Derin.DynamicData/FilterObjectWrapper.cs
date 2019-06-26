using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.DynamicData
{
    public class FilterObjectWrapper
    {
        public FilterObjectWrapper(string logic, IEnumerable<FilterObject> filterObjects)
        {
            Logic = logic;
            FilterObjects = filterObjects;
        }

        public string Logic { get; set; }
        public IEnumerable<FilterObject> FilterObjects { get; set; }
        public string LogicToken
        {
            get
            {
                switch (Logic)
                {
                    case "and":
                        return "&&";
                    case "or":
                        return "||";
                    default:
                        return null;
                }
            }
        }
    }
}
