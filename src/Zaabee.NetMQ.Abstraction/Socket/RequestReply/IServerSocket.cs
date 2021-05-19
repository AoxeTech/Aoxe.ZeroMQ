using System.Threading.Tasks;

namespace Zaabee.NetMQ.Abstraction.Socket.RequestReply
{
    public interface IServerSocket
    {
        T Receive<T>();
        Task<T> ReceiveAsync<T>();
    }
}