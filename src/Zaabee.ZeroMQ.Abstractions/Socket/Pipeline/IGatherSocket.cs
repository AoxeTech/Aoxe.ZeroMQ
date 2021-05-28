using System.Threading.Tasks;

namespace Zaabee.ZeroMQ.Abstractions.Socket.Pipeline
{
    public interface IGatherSocket
    {
        T Pull<T>();
        Task<T> PullAsync<T>();
    }
}