namespace Aoxe.ZeroMQ.Test;

public partial class AoxeZeroMessageBusTest
{
    [Fact]
    public async Task PipelineTestAsync()
    {
        using var scatter = new AoxeZeroMessageBus(
            new SystemTextJson.Serializer(),
            scatterBindAddress: "inproc://test-scatter-gather-async"
        );
        using var gather0 = new AoxeZeroMessageBus(
            new SystemTextJson.Serializer(),
            gatherConnectAddress: "inproc://test-scatter-gather-async"
        );
        using var gather1 = new AoxeZeroMessageBus(
            new SystemTextJson.Serializer(),
            gatherConnectAddress: "inproc://test-scatter-gather-async"
        );

        var models = Enumerable.Range(0, 4).Select(_ => TestModelFactory.Create()).ToList();

        models.ForEach(async model => await scatter.PushAsync(model));

        var receiveModels = new List<TestModel?>
        {
            await gather0.PullAsync<TestModel>(),
            await gather0.PullAsync<TestModel>(),
            await gather1.PullAsync<TestModel>(),
            await gather1.PullAsync<TestModel>()
        };

        Assert.True(EqualModels(models, receiveModels));
    }
}
