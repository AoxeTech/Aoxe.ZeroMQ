using System.Linq;
using System.Threading.Tasks;
using TestModels;
using Xunit;

namespace Zaabee.ZeroMQ.Test
{
    public partial class ZaabeeZeroMqHubTest
    {
        [Fact]
        public async Task PubSubTestAsync()
        {
            const string groupA = "GroupA";
            const string groupB = "GroupB";
            var groupDefault = typeof(TestModel).ToString();

            using var publisher = new ZaabeeZeroMqHub(new Jil.Serializer(),
                radioBindAddress: "inproc://test-publish-subscribe-async");
            publisher.RadioSocketOptions.SendHighWatermark = 1000;

            using var subGroupA = new ZaabeeZeroMqHub(new Jil.Serializer(),
                dishConnectAddress: "inproc://test-publish-subscribe-async");
            subGroupA.DishJoin(groupA);
            subGroupA.DishSocketOptions.ReceiveHighWatermark = 1000;

            using var subGroupB = new ZaabeeZeroMqHub(new Jil.Serializer(),
                dishConnectAddress: "inproc://test-publish-subscribe-async");
            subGroupB.DishJoin(groupB);
            subGroupA.DishSocketOptions.ReceiveHighWatermark = 1000;

            using var subGroupDefault = new ZaabeeZeroMqHub(new Jil.Serializer(),
                dishConnectAddress: "inproc://test-publish-subscribe-async");
            subGroupDefault.DishJoin(groupDefault);
            subGroupDefault.DishSocketOptions.ReceiveHighWatermark = 1000;

            using var subGroupAll = new ZaabeeZeroMqHub(new Jil.Serializer(),
                dishConnectAddress: "inproc://test-publish-subscribe-async");
            subGroupAll.DishJoin(groupA);
            subGroupAll.DishJoin(groupB);

            var modelsGroupA = Enumerable
                .Range(0, 10)
                .Select(_ => TestModelFactory.Create())
                .ToList();

            var modelsGroupB = Enumerable
                .Range(0, 10)
                .Select(_ => TestModelFactory.Create())
                .ToList();

            var modelsGroupDefault = Enumerable
                .Range(0, 10)
                .Select(_ => TestModelFactory.Create())
                .ToList();

            modelsGroupA.ForEach(async model => await publisher.PublishAsync(groupA, model));
            modelsGroupB.ForEach(async model => await publisher.PublishAsync(groupB, model));
            modelsGroupDefault.ForEach(async model => await publisher.PublishAsync(model));

            modelsGroupA.ForEach(async _ =>
            {
                var (group, model) = await subGroupA.SubscribeAsync<TestModel>();
                Assert.Equal("GroupA", group);
                _groupA.Add(model);
            });

            modelsGroupB.ForEach(async _ =>
            {
                var (group, model) = await subGroupB.SubscribeAsync<TestModel>();
                Assert.Equal("GroupB", group);
                _groupB.Add(model);
            });

            modelsGroupDefault.ForEach(async _ =>
            {
                var (group, model) = await subGroupDefault.SubscribeAsync<TestModel>();
                Assert.Equal(groupDefault, group);
                _groupDefault.Add(model);
            });

            for (var i = 0; i < modelsGroupA.Count + modelsGroupB.Count; i++)
            {
                var (group, model) = await subGroupAll.SubscribeAsync<TestModel>();
                _groupAll.Add(model);
            }

            var groupAll = _groupA.Union(_groupB).ToList();
            
            Assert.True(EqualModels(_groupA, modelsGroupA));
            Assert.True(EqualModels(_groupB, modelsGroupB));
            Assert.True(EqualModels(_groupAll, groupAll));
            Assert.True(EqualModels(_groupDefault, modelsGroupDefault));
        }
    }
}