namespace Zaabee.ZeroMQ.Test;

public partial class ZaabeeZeroMessageBusTest
{
    [Fact]
    public void OptionsTest()
    {
        var mqHub = new ZaabeeZeroMessageBus(new Jil.Serializer());
        Assert.NotNull(mqHub.ClientSocketOptions);
        Assert.NotNull(mqHub.ServerSocketOptions);
        Assert.NotNull(mqHub.ScatterSocketOptions);
        Assert.NotNull(mqHub.GatherSocketOptions);
        Assert.NotNull(mqHub.RadioSocketOptions);
        Assert.NotNull(mqHub.DishSocketOptions);
    }

    private static bool EqualModels(List<TestModel?>? models0, List<TestModel?>? models1)
    {
        if (models0 is null || models1 is null) return false;
        if (models0.Count != models1.Count) return false;
        return models0.All(model0 => models1.Any(model1 =>
            EqualModel(model0, model1)
        ));
    }

    private static bool EqualModel(TestModel? model0, TestModel? model1) =>
        model0 is not null
        && model1 is not null
        && model0.Id == model1.Id
        && model0.Name == model1.Name
        && model0.Age == model1.Age
        && model0.CreateTime == model1.CreateTime
        && model0.Gender == model1.Gender;
}