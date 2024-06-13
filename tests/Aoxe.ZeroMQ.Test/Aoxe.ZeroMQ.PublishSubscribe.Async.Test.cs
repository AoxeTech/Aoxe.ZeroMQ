namespace Aoxe.ZeroMQ.Test;

public partial class AoxeZeroMessageBusTest
{
    [Fact]
    public async Task PubSubTestAsync()
    {
        const string topicA = "TopicA";
        const string topicB = "TopicB";
        var topicDefault = typeof(TestModel).ToString();

        using var publisher = new AoxeZeroMessageBus(
            new SystemTextJson.Serializer(),
            radioBindAddress: "inproc://test-publish-subscribe-async"
        );
        publisher.RadioSocketOptions.SendHighWatermark = 1000;

        using var subTopicA = new AoxeZeroMessageBus(
            new SystemTextJson.Serializer(),
            dishConnectAddress: "inproc://test-publish-subscribe-async"
        );
        subTopicA.DishJoin(topicA);
        subTopicA.DishSocketOptions.ReceiveHighWatermark = 1000;

        using var subTopicB = new AoxeZeroMessageBus(
            new SystemTextJson.Serializer(),
            dishConnectAddress: "inproc://test-publish-subscribe-async"
        );
        subTopicB.DishJoin(topicB);
        subTopicA.DishSocketOptions.ReceiveHighWatermark = 1000;

        using var subTopicDefault = new AoxeZeroMessageBus(
            new SystemTextJson.Serializer(),
            dishConnectAddress: "inproc://test-publish-subscribe-async"
        );
        subTopicDefault.DishJoin(topicDefault);
        subTopicDefault.DishSocketOptions.ReceiveHighWatermark = 1000;

        using var subTopicAll = new AoxeZeroMessageBus(
            new SystemTextJson.Serializer(),
            dishConnectAddress: "inproc://test-publish-subscribe-async"
        );
        subTopicAll.DishJoin(topicA);
        subTopicAll.DishJoin(topicB);

        var modelsTopicA = Enumerable.Range(0, 10).Select(_ => TestModelFactory.Create()).ToList();

        var modelsTopicB = Enumerable.Range(0, 10).Select(_ => TestModelFactory.Create()).ToList();

        var modelsTopicDefault = Enumerable
            .Range(0, 10)
            .Select(_ => TestModelFactory.Create())
            .ToList();

        modelsTopicA.ForEach(async model => await publisher.PublishAsync(topicA, model));
        modelsTopicB.ForEach(async model => await publisher.PublishAsync(topicB, model));
        modelsTopicDefault.ForEach(async model => await publisher.PublishAsync(model));

        modelsTopicA.ForEach(async _ =>
        {
            var (topic, model) = await subTopicA.DishReceiveAsync<TestModel>();
            Assert.Equal(topicA, topic);
            _topicA.Add(model);
        });

        modelsTopicB.ForEach(async _ =>
        {
            var (topic, model) = await subTopicB.DishReceiveAsync<TestModel>();
            Assert.Equal(topicB, topic);
            _topicB.Add(model);
        });

        modelsTopicDefault.ForEach(async _ =>
        {
            var (topic, model) = await subTopicDefault.DishReceiveAsync<TestModel>();
            Assert.Equal(topicDefault, topic);
            _topicDefault.Add(model);
        });

        for (var i = 0; i < modelsTopicA.Count + modelsTopicB.Count; i++)
        {
            var (topic, model) = await subTopicAll.DishReceiveAsync<TestModel>();
            _topicAll.Add(model);
        }

        var topicAll = _topicA.Union(_topicB).ToList();

        Assert.True(EqualModels(_topicA, modelsTopicA));
        Assert.True(EqualModels(_topicB, modelsTopicB));
        Assert.True(EqualModels(_topicAll, topicAll));
        Assert.True(EqualModels(_topicDefault, modelsTopicDefault));
    }
}
