using System.Text;
using Jil;
using Zaabee.Jil;
using Zaabee.ZeroMQ.Serializer.Abstraction;

namespace Zaabee.ZeroMQ.Jil
{
    public class Serializer : ISerializer
    {
        private static Encoding _encoding;
        private static Options _options;

        public Serializer(Encoding encoding = null, Options options = null)
        {
            _encoding = encoding ?? Encoding.UTF8;
            _options = options;
        }

        public byte[] Serialize<T>(T dto) =>
            JilSerializer.Serialize(dto, _options, _encoding);

        public T Deserialize<T>(byte[] message) =>
            JilSerializer.Deserialize<T>(message, _options, _encoding);
    }
}