namespace Zaabee.ZeroMQ.Test;

public partial class ZaabeeZeroMessageBusTest
{
    [Fact]
    public void PipelineTest()
    {
        using var scatter =
            new ZaabeeZeroMessageBus(new Jil.Serializer(), scatterBindAddress: "inproc://test-scatter-gather");
        using var gather0 =
            new ZaabeeZeroMessageBus(new Jil.Serializer(), gatherConnectAddress: "inproc://test-scatter-gather");
        using var gather1 =
            new ZaabeeZeroMessageBus(new Jil.Serializer(), gatherConnectAddress: "inproc://test-scatter-gather");

        var models = Enumerable
            .Range(0, 4)
            .Select(_ => TestModelFactory.Create())
            .ToList();

        models.ForEach(model => scatter.Push(model));

        var receiveModels = new List<TestModel?>
        {
            gather0.Pull<TestModel>(),
            gather0.Pull<TestModel>(),
            gather1.Pull<TestModel>(),
            gather1.Pull<TestModel>()
        };

        Assert.True(EqualModels(models, receiveModels));
    }
}