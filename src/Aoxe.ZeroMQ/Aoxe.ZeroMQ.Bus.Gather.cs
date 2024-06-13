namespace Aoxe.ZeroMQ;

public partial class AoxeZeroMessageBus
{
    public ThreadSafeSocketOptions GatherSocketOptions => _gatherSocket.Options;

    public T? Pull<T>() => _serializer.FromBytes<T>(_gatherSocket.ReceiveBytes());

    public async ValueTask<T?> PullAsync<T>() =>
        _serializer.FromBytes<T>(await _gatherSocket.ReceiveBytesAsync());
}
