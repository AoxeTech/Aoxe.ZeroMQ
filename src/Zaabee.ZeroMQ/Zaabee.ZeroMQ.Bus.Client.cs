namespace Zaabee.ZeroMQ;

public partial class ZaabeeZeroMessageBus
{
    public ThreadSafeSocketOptions ClientSocketOptions => _clientSocket.Options;

    public void ClientSend<T>(T? message) =>
        _clientSocket.Send(_serializer.ToBytes(message));

    public ValueTask ClientSendAsync<T>(T? message) =>
        _clientSocket.SendAsync(_serializer.ToBytes(message));

    public T? ClientReceive<T>() =>
        _serializer.FromBytes<T>(_clientSocket.ReceiveBytes());

    public async ValueTask<T?> ClientReceiveAsync<T>() =>
        _serializer.FromBytes<T>(await _clientSocket.ReceiveBytesAsync());
}