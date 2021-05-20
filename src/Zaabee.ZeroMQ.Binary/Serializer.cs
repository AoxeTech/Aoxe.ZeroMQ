using Zaabee.Binary;
using Zaabee.ZeroMQ.Serializer.Abstraction;

namespace Zaabee.ZeroMQ.Binary
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) =>
            BinarySerializer.Serialize(o);

        public T Deserialize<T>(byte[] bytes) =>
            BinarySerializer.Deserialize<T>(bytes);
    }
}