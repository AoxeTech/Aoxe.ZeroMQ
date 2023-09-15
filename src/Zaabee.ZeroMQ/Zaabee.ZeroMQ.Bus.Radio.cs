namespace Zaabee.ZeroMQ;

public partial class ZaabeeZeroMessageBus
{
    public ThreadSafeSocketOptions RadioSocketOptions => _radioSocket.Options;

    public void Publish<T>(T? message) =>
        _radioSocket.Send(typeof(T).ToString(), _serializer.ToBytes(message));

    public void Publish<T>(string topic, T? message) =>
        _radioSocket.Send(topic, _serializer.ToBytes(message));

    public ValueTask PublishAsync<T>(T? message) =>
        _radioSocket.SendAsync(typeof(T).ToString(), _serializer.ToBytes(message));

    public ValueTask PublishAsync<T>(string topic, T? message) =>
        _radioSocket.SendAsync(topic, _serializer.ToBytes(message));
}