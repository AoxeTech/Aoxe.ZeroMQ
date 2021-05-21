using TestModels;
using Xunit;
using Zaabee.ZeroMQ.Serializer.Abstraction;

namespace Zaabee.ZeroMQ.Serializer.Test
{
    public class UnitTest
    {
        [Fact]
        public void BinaryTest() =>
            Assert.True(SerializerTest(new Zaabee.ZeroMQ.Binary.Serializer()));
        [Fact]
        public void JilTest() =>
            Assert.True(SerializerTest(new Zaabee.ZeroMQ.Jil.Serializer()));
        [Fact]
        public void MsgPackTest() =>
            Assert.True(SerializerTest(new Zaabee.ZeroMQ.MsgPack.Serializer()));
        [Fact]
        public void NewtonsoftJsonTest() =>
            Assert.True(SerializerTest(new Zaabee.ZeroMQ.NewtonsoftJson.Serializer()));
        [Fact]
        public void ProtobufTest() =>
            Assert.True(SerializerTest(new Zaabee.ZeroMQ.Protobuf.Serializer()));
        [Fact]
        public void SystemTextJsonTest() =>
            Assert.True(SerializerTest(new Zaabee.ZeroMQ.SystemTextJson.Serializer()));
        [Fact]
        public void Utf8JsonTest() =>
            Assert.True(SerializerTest(new Zaabee.ZeroMQ.Utf8Json.Serializer()));
        [Fact]
        public void XmlTest() =>
            Assert.True(SerializerTest(new Zaabee.ZeroMQ.Xml.Serializer()));
        [Fact]
        public void ZeroFormatterTest() =>
            Assert.True(SerializerTest(new Zaabee.ZeroMQ.ZeroFormatter.Serializer()));

        private bool SerializerTest(ISerializer serializer)
        {
            var testModel = TestModelFactory.Create();
            var bytes = serializer.Serialize(testModel);
            var deserializeModel = serializer.Deserialize<TestModel>(bytes);
            return testModel.Id == deserializeModel.Id
                   && testModel.Name == deserializeModel.Name
                   && testModel.Age == deserializeModel.Age
                   && testModel.CreateTime == deserializeModel.CreateTime
                   && testModel.Gender == deserializeModel.Gender;
        }
    }
}