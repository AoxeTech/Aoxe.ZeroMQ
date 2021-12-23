namespace Zaabee.ZeroMQ.Test;

public partial class ZaabeeZeroMessageBusTest
{
    private readonly List<TestModel> _groupA = new();
    private readonly List<TestModel> _groupB = new();
    private readonly List<TestModel> _groupDefault = new();
    private readonly List<TestModel> _groupAll = new();

    [Fact]
    public void PubSubTest()
    {
        const string groupA = "GroupA";
        const string groupB = "GroupB";
        var groupDefault = typeof(TestModel).ToString();

        using var publisher = new ZaabeeZeroMessageBus(new Jil.Serializer(),
            radioBindAddress: "inproc://test-publish-subscribe");
        publisher.RadioSocketOptions.SendHighWatermark = 1000;

        using var subGroupA = new ZaabeeZeroMessageBus(new Jil.Serializer(),
            dishConnectAddress: "inproc://test-publish-subscribe");
        subGroupA.DishJoin(groupA);
        subGroupA.DishSocketOptions.ReceiveHighWatermark = 1000;

        using var subGroupB = new ZaabeeZeroMessageBus(new Jil.Serializer(),
            dishConnectAddress: "inproc://test-publish-subscribe");
        subGroupB.DishJoin(groupB);
        subGroupA.DishSocketOptions.ReceiveHighWatermark = 1000;

        using var subGroupDefault = new ZaabeeZeroMessageBus(new Jil.Serializer(),
            dishConnectAddress: "inproc://test-publish-subscribe");
        subGroupDefault.DishJoin(groupDefault);
        subGroupDefault.DishSocketOptions.ReceiveHighWatermark = 1000;

        using var subGroupAll = new ZaabeeZeroMessageBus(new Jil.Serializer(),
            dishConnectAddress: "inproc://test-publish-subscribe");
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

        modelsGroupA.ForEach(model => publisher.Publish(groupA, model));
        modelsGroupB.ForEach(model => publisher.Publish(groupB, model));
        modelsGroupDefault.ForEach(model => publisher.Publish(model));

        modelsGroupA.ForEach(_ =>
        {
            var (group, model) = subGroupA.DishReceive<TestModel>();
            Assert.Equal("GroupA", group);
            _groupA.Add(model);
        });

        modelsGroupB.ForEach(_ =>
        {
            var (group, model) = subGroupB.DishReceive<TestModel>();
            Assert.Equal("GroupB", group);
            _groupB.Add(model);
        });

        modelsGroupDefault.ForEach(_ =>
        {
            var (group, model) = subGroupDefault.DishReceive<TestModel>();
            Assert.Equal(groupDefault, group);
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