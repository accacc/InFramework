using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Json
{
    public interface IJsonSerializer
    {
        string Serialize(object obj, bool indented = false);
        object Deserialize(string json, Type type);
        T Deserialize<T>(string json);
    }
}
