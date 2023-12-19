namespace Zaabee.ZeroMQ;

public partial class ZaabeeZeroMessageBus
{
    public ThreadSafeSocketOptions GatherSocketOptions => _gatherSocket.Options;

    public T? Pull<T>() => _serializer.FromBytes<T>(_gatherSocket.ReceiveBytes());

    public async ValueTask<T?> PullAsync<T>() =>
        _serializer.FromBytes<T>(await _gatherSocket.ReceiveBytesAsync());
}
