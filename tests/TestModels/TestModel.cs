namespace TestModels;

public class TestModel
{
    public virtual Guid Id { get; set; }

    public virtual int Age { get; set; }

    public virtual string Name { get; set; }

    public virtual DateTime CreateTime { get; set; }

    public virtual Gender Gender { get; set; }
}
