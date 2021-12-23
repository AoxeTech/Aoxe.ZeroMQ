namespace Zaabee.ZeroMQ;

public partial class ZaabeeZeroMessageBus
{
    public ThreadSafeSocketOptions ScatterSocketOptions => _scatterSocket.Options;

    public void Push<T>(T message) =>
        _scatterSocket.Send(_serializer.ToBytes(message));

    public async Task PushAsync<T>(T message) =>
        await _scatterSocket.SendAsync(_serializer.ToBytes(message));
}