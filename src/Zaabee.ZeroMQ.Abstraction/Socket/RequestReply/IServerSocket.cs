using System.Threading.Tasks;

namespace Zaabee.ZeroMQ.Abstraction.Socket.RequestReply
{
    public interface IServerSocket
    {
        void ServerSend<T>(uint routingId, T message);
        Task ServerSendAsync<T>(uint routingId, T message);
        (uint, T) ServerReceive<T>();
        Task<(uint, T)> ServerReceiveAsync<T>();
    }
}