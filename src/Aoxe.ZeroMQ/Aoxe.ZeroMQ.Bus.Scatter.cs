namespace Aoxe.ZeroMQ;

public partial class AoxeZeroMessageBus
{
    public ThreadSafeSocketOptions ScatterSocketOptions => _scatterSocket.Options;

    public void Push<T>(T? message) => _scatterSocket.Send(_serializer.ToBytes(message));

    public ValueTask PushAsync<T>(T? message) =>
        _scatterSocket.SendAsync(_serializer.ToBytes(message));
}
