using System.Threading.Tasks;

namespace Zaabee.NetMQ.Abstraction
{
    public interface IPair
    {
        void Send<T>(T message);
        T Receive<T>();
        Task<T> ReceiveAsync<T>();
    }
}