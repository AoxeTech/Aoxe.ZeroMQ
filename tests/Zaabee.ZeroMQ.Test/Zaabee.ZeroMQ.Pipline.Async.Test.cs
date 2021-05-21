using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestModels;
using Xunit;

namespace Zaabee.ZeroMQ.Test
{
    public partial class ZaabeeZeroMqHubTest
    {
        [Fact]
        public async Task PipelineTestAsync()
        {
            using var scatter =
                new ZaabeeZeroMqHub(new Jil.Serializer(), scatterBindAddress: "inproc://test-scatter-gather-async");
            using var gather0 =
                new ZaabeeZeroMqHub(new Jil.Serializer(), gatherConnectAddress: "inproc://test-scatter-gather-async");
            using var gather1 =
                new ZaabeeZeroMqHub(new Jil.Serializer(), gatherConnectAddress: "inproc://test-scatter-gather-async");

            var models = Enumerable
                .Range(0, 4)
                .Select(_ => TestModelFactory.Create())
                .ToList();

            models.ForEach(async model => await scatter.PushAsync(model));

            var receiveModels = new List<TestModel>
            {
                await gather0.PullAsync<TestModel>(),
                await gather0.PullAsync<TestModel>(),
                await gather1.PullAsync<TestModel>(),
                await gather1.PullAsync<TestModel>()
            };

            Assert.True(EqualModels(models, receiveModels));
        }
    }
}