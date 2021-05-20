using System.Text.Json;
using Zaabee.SystemTextJson;
using Zaabee.ZeroMQ.Serializer.Abstraction;

namespace Zaabee.ZeroMQ.SystemTextJson
{
    public class Serializer : ISerializer
    {
        private static JsonSerializerOptions _jsonSerializerOptions;

        public Serializer(JsonSerializerOptions jsonSerializerOptions = null)
        {
            _jsonSerializerOptions = jsonSerializerOptions;
        }

        public byte[] Serialize<T>(T o) =>
            SystemTextJsonSerializer.Serialize(o, _jsonSerializerOptions);

        public T Deserialize<T>(byte[] bytes) =>
            SystemTextJsonSerializer.Deserialize<T>(bytes, _jsonSerializerOptions);
    }
}