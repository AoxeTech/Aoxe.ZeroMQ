using System.Collections.Generic;
using System.Linq;
using TestModels;
using Xunit;

namespace Zaabee.ZeroMQ.Test
{
    public partial class ZaabeeZeroMqHubTest
    {
        [Fact]
        public void PipelineTest()
        {
            using var scatter =
                new ZaabeeZeroMqHub(new Jil.Serializer(), scatterBindAddress: "inproc://test-scatter-gather");
            using var gather0 =
                new ZaabeeZeroMqHub(new Jil.Serializer(), gatherConnectAddress: "inproc://test-scatter-gather");
            using var gather1 =
                new ZaabeeZeroMqHub(new Jil.Serializer(), gatherConnectAddress: "inproc://test-scatter-gather");

            var models = Enumerable
                .Range(0, 4)
                .Select(_ => TestModelFactory.Create())
                .ToList();

            models.ForEach(model => scatter.Push(model));

            var receiveModels = new List<TestModel>
            {
                gather0.Pull<TestModel>(),
                gather0.Pull<TestModel>(),
                gather1.Pull<TestModel>(),
                gather1.Pull<TestModel>()
            };

            Assert.True(EqualModels(models, receiveModels));
        }
    }
}