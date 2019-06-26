using IF.Core.Json;
using IF.Core.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Redis
{
    public class RedisJsonSerializer : IRedisSerializer
    {
        private readonly IJsonSerializer jsonSerializer;
        private static readonly Encoding encoding = Encoding.UTF8;

        public RedisJsonSerializer(IJsonSerializer jsonSerializer)
        {
            this.jsonSerializer = jsonSerializer;
        }

        public object Deserialize(byte[] serializedObject)
        {
            var jsonString = encoding.GetString(serializedObject);
            return jsonSerializer.Deserialize(jsonString,typeof(object));
        }

        public T Deserialize<T>(byte[] serializedObject)
        {
            var jsonString = encoding.GetString(serializedObject);
            return jsonSerializer.Deserialize<T>(jsonString);
        }

        public byte[] Serialize(object item)
        {
            var jsonString =  jsonSerializer.Serialize(item);
            return encoding.GetBytes(jsonString);
        }
    }
}
