namespace Zaabee.ZeroMQ;

public partial class ZaabeeZeroMessageBus
{
    public ThreadSafeSocketOptions DishSocketOptions => _dishSocket.Options;

    public (string, T?) DishReceive<T>()
    {
        var (topic, messageBytes) = _dishSocket.ReceiveBytes();
        return (topic, _serializer.FromBytes<T>(messageBytes));
    }

    public async ValueTask<(string, T?)> DishReceiveAsync<T>()
    {
        var (topic, messageBytes) = await _dishSocket.ReceiveBytesAsync();
        return (topic, _serializer.FromBytes<T>(messageBytes));
    }
}