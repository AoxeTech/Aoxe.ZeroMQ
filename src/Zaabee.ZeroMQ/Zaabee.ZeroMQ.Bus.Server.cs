namespace Zaabee.ZeroMQ;

public partial class ZaabeeZeroMessageBus
{
    public ThreadSafeSocketOptions ServerSocketOptions => _serverSocket.Options;

    public void ServerSend<T>(uint routingId, T? message) =>
        _serverSocket.Send(routingId, _serializer.ToBytes(message));

    public async ValueTask ServerSendAsync<T>(uint routingId, T? message) =>
        await _serverSocket.SendAsync(routingId, _serializer.ToBytes(message));

    public (uint, T?) ServerReceive<T>()
    {
        var (routingId, clientMsg) = _serverSocket.ReceiveBytes();
        return (routingId, _serializer.FromBytes<T>(clientMsg));
    }

    public async ValueTask<(uint, T?)> ServerReceiveAsync<T>()
    {
        var (routingId, clientMsg) = await _serverSocket.ReceiveBytesAsync();
        return (routingId, _serializer.FromBytes<T>(clientMsg));
    }
}