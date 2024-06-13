namespace Aoxe.ZeroMQ.Abstractions.Socket.Pipeline;

public interface IGatherSocket
{
    T? Pull<T>();
    ValueTask<T?> PullAsync<T>();
}
