using Zaabee.MsgPack;
using Zaabee.ZeroMQ.Serializer.Abstraction;

namespace Zaabee.ZeroMQ.MsgPack
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) =>
            MsgPackSerializer.Serialize(o);

        public T Deserialize<T>(byte[] bytes) =>
            MsgPackSerializer.Deserialize<T>(bytes);
    }
}