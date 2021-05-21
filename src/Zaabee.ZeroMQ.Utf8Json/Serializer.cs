﻿using Utf8Json;
using Zaabee.Utf8Json;
using Zaabee.ZeroMQ.Serializer.Abstraction;

namespace Zaabee.ZeroMQ.Utf8Json
{
    public class Serializer : ISerializer
    {
        private static IJsonFormatterResolver _jsonFormatterResolver;

        public Serializer(IJsonFormatterResolver jsonFormatterResolver = null)
        {
            _jsonFormatterResolver = jsonFormatterResolver;
        }

        public byte[] Serialize<T>(T o) =>
            Utf8JsonSerializer.Serialize(o, _jsonFormatterResolver);

        public T Deserialize<T>(byte[] bytes) =>
            Utf8JsonSerializer.Deserialize<T>(bytes, _jsonFormatterResolver);
    }
}