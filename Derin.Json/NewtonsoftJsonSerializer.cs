using Derin.Core.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Json
{
    public class NewtonsoftJsonSerializer : IJsonSerializer
    {
        public NewtonsoftJsonSerializer()
        {
        }

        private static readonly JsonSerializerSettings SettingsNotIndented = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
        };
        private static readonly JsonSerializerSettings SettingsIndented = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
        };

        public string Serialize(object obj, bool indented = false)
        {
            var settings = indented ? SettingsIndented : SettingsNotIndented;
            return JsonConvert.SerializeObject(obj, settings);
        }

        public object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
