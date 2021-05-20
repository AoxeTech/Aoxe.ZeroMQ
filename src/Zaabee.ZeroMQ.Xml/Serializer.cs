using Zaabee.Xml;
using Zaabee.ZeroMQ.Serializer.Abstraction;

namespace Zaabee.ZeroMQ.Xml
{
    public class Serializer : ISerializer
    {
        public byte[] Serialize<T>(T o) =>
            XmlSerializer.Serialize(o);

        public T Deserialize<T>(byte[] bytes) =>
            XmlSerializer.Deserialize<T>(bytes);
    }
}