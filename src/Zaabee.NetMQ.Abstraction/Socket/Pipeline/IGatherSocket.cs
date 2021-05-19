using System.Threading.Tasks;

namespace Zaabee.NetMQ.Abstraction.Socket.Pipeline
{
    public interface IGatherSocket
    {
        T Pull<T>();
        Task<T> PullAsync<T>();
    }
}