namespace Zaabee.ZeroMQ.Abstractions.Socket.Pipeline;

public interface IScatterSocket
{
    void Push<T>(T? message);
    ValueTask PushAsync<T>(T? message);
}