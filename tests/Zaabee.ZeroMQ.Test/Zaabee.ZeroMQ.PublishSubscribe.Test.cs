namespace Zaabee.ZeroMQ.Test;

public partial class ZaabeeZeroMessageBusTest
{
    private readonly List<TestModel?>? _topicA = new();
    private readonly List<TestModel?>? _topicB = new();
    private readonly List<TestModel?>? _topicDefault = new();
    private readonly List<TestModel?>? _topicAll = new();

    [Fact]
    public void PubSubTest()
    {
        const string topicA = "TopicA";
        const string topicB = "TopicB";
        var topicDefault = typeof(TestModel).ToString();

        using var publisher = new ZaabeeZeroMessageBus(
            new SystemTextJson.Serializer(),
            radioBindAddress: "inproc://test-publish-subscribe"
        );
        publisher.RadioSocketOptions.SendHighWatermark = 1000;

        using var subTopicA = new ZaabeeZeroMessageBus(
            new SystemTextJson.Serializer(),
            dishConnectAddress: "inproc://test-publish-subscribe"
        );
        subTopicA.DishJoin(topicA);
        subTopicA.DishSocketOptions.ReceiveHighWatermark = 1000;

        using var subTopicB = new ZaabeeZeroMessageBus(
            new SystemTextJson.Serializer(),
            dishConnectAddress: "inproc://test-publish-subscribe"
        );
        subTopicB.DishJoin(topicB);
        subTopicA.DishSocketOptions.ReceiveHighWatermark = 1000;

        using var subTopicDefault = new ZaabeeZeroMessageBus(
            new SystemTextJson.Serializer(),
            dishConnectAddress: "inproc://test-publish-subscribe"
        );
        subTopicDefault.DishJoin(topicDefault);
        subTopicDefault.DishSocketOptions.ReceiveHighWatermark = 1000;

        using var subTopicAll = new ZaabeeZeroMessageBus(
            new SystemTextJson.Serializer(),
            dishConnectAddress: "inproc://test-publish-subscribe"
        );
        subTopicAll.DishJoin(topicA);
        subTopicAll.DishJoin(topicB);

        var modelsTopicA = Enumerable.Range(0, 10).Select(_ => TestModelFactory.Create()).ToList();

        var modelsTopicB = Enumerable.Range(0, 10).Select(_ => TestModelFactory.Create()).ToList();

        var modelsTopicDefault = Enumerable
            .Range(0, 10)
            .Select(_ => TestModelFactory.Create())
            .ToList();

        modelsTopicA.ForEach(model => publisher.Publish(topicA, model));
        modelsTopicB.ForEach(model => publisher.Publish(topicB, model));
        modelsTopicDefault.ForEach(model => publisher.Publish(model));

        modelsTopicA.ForEach(_ =>
        {
            var (topic, model) = subTopicA.DishReceive<TestModel>();
            Assert.Equal(topicA, topic);
            _topicA.Add(model);
        });

        modelsTopicB.ForEach(_ =>
        {
            var (topic, model) = subTopicB.DishReceive<TestModel>();
            Assert.Equal(topicB, topic);
            _topicB.Add(model);
        });

        modelsTopicDefault.ForEach(_ =>
        {
            var (topic, model) = subTopicDefault.DishReceive<TestModel>();
            Assert.Equal(topicDefault, topic);
            _topicDefault.Add(model);
        });

        for (var i = 0; i < modelsTopicA.Count + modelsTopicB.Count; i++)
        {
            var (topic, model) = subTopicAll.DishReceive<TestModel>();
            _topicAll.Add(model);
        }

        var topicAll = _topicA.Union(_topicB).ToList();

        Assert.True(EqualModels(_topicA, modelsTopicA));
        Assert.True(EqualModels(_topicB, modelsTopicB));
        Assert.True(EqualModels(_topicAll, topicAll));
        Assert.True(EqualModels(_topicDefault, modelsTopicDefault));
    }
}
