namespace Zaabee.ZeroMQ;

public partial class ZaabeeZeroMessageBus
{
    public ThreadSafeSocketOptions DishSocketOptions => _dishSocket.Options;

    public (string, T) DishReceive<T>()
    {
        var (group, messageBytes) = _dishSocket.ReceiveBytes();
        return (group, _serializer.FromBytes<T>(messageBytes)!);
    }

    public async Task<(string, T)> DishReceiveAsync<T>()
    {
        var (group, messageBytes) = await _dishSocket.ReceiveBytesAsync();
        return (group, _serializer.FromBytes<T>(messageBytes)!);
    }
}