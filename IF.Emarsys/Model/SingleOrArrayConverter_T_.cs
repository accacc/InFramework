using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace IF.Emarsys.Model
{
	internal class SingleOrArrayConverter<T> : JsonConverter
	{
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		public SingleOrArrayConverter()
		{
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(List<T>);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<T>>();
            }
            return new List<T> { token.ToObject<T>() };
        }

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}
	}
}