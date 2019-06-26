using IF.Core.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Redis
{
    public class RedisBinarySerializer : IRedisSerializer
    {

        private readonly IBinarySerializer binarySerializer;
        public RedisBinarySerializer(IBinarySerializer binarySerializer)
        {
            this.binarySerializer = binarySerializer;
        }

        public object Deserialize(byte[] serializedObject)
        {
            return this.binarySerializer.Deserialize(serializedObject);
        }

        public T Deserialize<T>(byte[] serializedObject)
        {
            return this.binarySerializer.Deserialize<T>(serializedObject);
        }

        public byte[] Serialize(object item)
        {
            return this.binarySerializer.Serialize(item);
        }
    }
}
