using Zaabee.Protobuf;
using Zaabee.ZeroMQ.Serializer.Abstraction;

namespace Zaabee.ZeroMQ.Protobuf
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) =>
            ProtobufSerializer.Serialize(o);

        public T Deserialize<T>(byte[] bytes) =>
            ProtobufSerializer.Deserialize<T>(bytes);
    }
}