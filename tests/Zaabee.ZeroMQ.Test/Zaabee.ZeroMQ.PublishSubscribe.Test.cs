namespace Zaabee.ZeroMQ.Test;

public partial class ZaabeeZeroMessageBusTest
{
    private readonly List<TestModel?> _groupA = new();
    private readonly List<TestModel?> _groupB = new();
    private readonly List<TestModel?> _groupDefault = new();
    private readonly List<TestModel?> _groupAll = new();

    [Fact]
    public void PubSubTest()
    {
        const string topicA = "TopicA";
        const string topicB = "TopicB";
        var topicDefault = typeof(TestModel).ToString();

        using var publisher = new ZaabeeZeroMessageBus(new Jil.Serializer(),
            radioBindAddress: "inproc://test-publish-subscribe");
        publisher.RadioSocketOptions.SendHighWatermark = 1000;

        using var subGroupA = new ZaabeeZeroMessageBus(new Jil.Serializer(),
            dishConnectAddress: "inproc://test-publish-subscribe");
        subGroupA.DishJoin(topicA);
        subGroupA.DishSocketOptions.ReceiveHighWatermark = 1000;

        using var subGroupB = new ZaabeeZeroMessageBus(new Jil.Serializer(),
            dishConnectAddress: "inproc://test-publish-subscribe");
        subGroupB.DishJoin(topicB);
        subGroupA.DishSocketOptions.ReceiveHighWatermark = 1000;

        using var subGroupDefault = new ZaabeeZeroMessageBus(new Jil.Serializer(),
            dishConnectAddress: "inproc://test-publish-subscribe");
        subGroupDefault.DishJoin(topicDefault);
        subGroupDefault.DishSocketOptions.ReceiveHighWatermark = 1000;

        using var subGroupAll = new ZaabeeZeroMessageBus(new Jil.Serializer(),
            dishConnectAddress: "inproc://test-publish-subscribe");
        subGroupAll.DishJoin(topicA);
        subGroupAll.DishJoin(topicB);

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

        modelsGroupA.ForEach(model => publisher.Publish(topicA, model));
        modelsGroupB.ForEach(model => publisher.Publish(topicB, model));
        modelsGroupDefault.ForEach(model => publisher.Publish(model));

        modelsGroupA.ForEach(_ =>
        {
            var (topic, model) = subGroupA.DishReceive<TestModel>();
            Assert.Equal(topicA, topic);
            _groupA.Add(model);
        });

        modelsGroupB.ForEach(_ =>
        {
            var (topic, model) = subGroupB.DishReceive<TestModel>();
            Assert.Equal(topicB, topic);
            _groupB.Add(model);
        });

        modelsGroupDefault.ForEach(_ =>
        {
            var (topic, model) = subGroupDefault.DishReceive<TestModel>();
            Assert.Equal(topicDefault, topic);
            _groupDefault.Add(model);
        });

        for (var i = 0; i < modelsGroupA.Count + modelsGroupB.Count; i++)
        {
            var (group, model) = subGroupAll.DishReceive<TestModel>();
            _groupAll.Add(model);
        }

        var groupAll = _groupA.Union(_groupB).ToList();
            
        Assert.True(EqualModels(_groupA, modelsGroupA));
        Assert.True(EqualModels(_groupB, modelsGroupB));
        Assert.True(EqualModels(_groupAll, groupAll));
        Assert.True(EqualModels(_groupDefault, modelsGroupDefault));
    }
}