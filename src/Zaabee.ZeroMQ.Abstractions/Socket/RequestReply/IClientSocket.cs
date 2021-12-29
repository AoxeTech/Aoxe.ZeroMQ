namespace Zaabee.ZeroMQ.Abstractions.Socket.RequestReply;

public interface IClientSocket
{
    void ClientSend<T>(T message);
    Task ClientSendAsync<T>(T message);
    T? ClientReceive<T>();
    Task<T?> ClientReceiveAsync<T>();
}