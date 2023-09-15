namespace Zaabee.ZeroMQ.Abstractions.Socket.RequestReply;

public interface IServerSocket
{
    void ServerSend<T>(uint routingId, T? message);
    ValueTask ServerSendAsync<T>(uint routingId, T? message);
    (uint, T?) ServerReceive<T>();
    ValueTask<(uint, T?)> ServerReceiveAsync<T>();
}