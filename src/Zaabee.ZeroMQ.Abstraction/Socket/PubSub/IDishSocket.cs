using System.Threading.Tasks;

namespace Zaabee.ZeroMQ.Abstraction.Socket.PubSub
{
    public interface IDishSocket
    {
        (string, T) Subscribe<T>();

        Task<(string, T)> SubscribeAsync<T>();
    }
}