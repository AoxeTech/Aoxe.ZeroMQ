namespace Zaabee.ZeroMQ.Abstractions.Socket.PubSub;

public interface IRadioSocket
{
    void Publish<T>(T message);
    void Publish<T>(string topic, T message);
    Task PublishAsync<T>(T message);
    Task PublishAsync<T>(string topic, T message);
}