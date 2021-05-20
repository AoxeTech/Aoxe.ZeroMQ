using Zaabee.ZeroFormatter;
using Zaabee.ZeroMQ.Serializer.Abstraction;

namespace Zaabee.ZeroMQ.ZeroFormatter
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) =>
            ZeroSerializer.Serialize(o);

        public T Deserialize<T>(byte[] bytes) =>
            ZeroSerializer.Deserialize<T>(bytes);
    }
}