using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.Serialization
{
    public interface IRedisSerializer
    {
        byte[] Serialize(object item);

        object Deserialize(byte[] serializedObject);
        T Deserialize<T>(byte[] serializedObject);
    }
}
