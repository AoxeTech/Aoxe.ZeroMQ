namespace Zaabee.ZeroMQ.Abstractions.Socket.RequestReply;

public interface IClientSocket
{
    void ClientSend<T>(T? message);
    ValueTask ClientSendAsync<T>(T? message);
    T? ClientReceive<T>();
    ValueTask<T?> ClientReceiveAsync<T>();
}
