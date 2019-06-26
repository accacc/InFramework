using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Serialization
{
    public interface IBinarySerializer
    {
        byte[] Serialize(object item);

        Task<byte[]> SerializeAsync(object item);

        /// </returns>
        object Deserialize(byte[] serializedObject);

        /// </returns>
        Task<object> DeserializeAsync(byte[] serializedObject);

        T Deserialize<T>(byte[] serializedObject);

        Task<T> DeserializeAsync<T>(byte[] serializedObject);
    }
}
