namespace Zaabee.NetMQ.Abstraction
{
    public interface IRequester
    {
        TResult Send<T, TResult>(T message);
    }
}