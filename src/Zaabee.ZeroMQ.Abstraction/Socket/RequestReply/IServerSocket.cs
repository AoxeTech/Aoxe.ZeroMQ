using System.Threading.Tasks;

namespace Zaabee.ZeroMQ.Abstraction.Socket.RequestReply
{
    public interface IServerSocket
    {
        T Receive<T>();
        Task<T> ReceiveAsync<T>();
    }
}