namespace Zaabee.ZeroMQ.Abstractions.Socket.PubSub;

public interface IRadioSocket
{
    void Publish<T>(T? message);
    void Publish<T>(string topic, T? message);
    ValueTask PublishAsync<T>(T? message);
    ValueTask PublishAsync<T>(string topic, T? message);
}