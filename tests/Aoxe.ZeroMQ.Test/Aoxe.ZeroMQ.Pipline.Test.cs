namespace Aoxe.ZeroMQ.Test;

public partial class AoxeZeroMessageBusTest
{
    [Fact]
    public void PipelineTest()
    {
        using var scatter = new AoxeZeroMessageBus(
            new SystemTextJson.Serializer(),
            scatterBindAddress: "inproc://test-scatter-gather"
        );
        using var gather0 = new AoxeZeroMessageBus(
            new SystemTextJson.Serializer(),
            gatherConnectAddress: "inproc://test-scatter-gather"
        );
        using var gather1 = new AoxeZeroMessageBus(
            new SystemTextJson.Serializer(),
            gatherConnectAddress: "inproc://test-scatter-gather"
        );

        var models = Enumerable.Range(0, 4).Select(_ => TestModelFactory.Create()).ToList();

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
