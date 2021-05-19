using System.Threading.Tasks;

namespace Zaabee.ZeroMQ.Abstraction.Socket.PubSub
{
    public interface IDishSocket
    {
        void Subscribe<T>();
        void Subscribe<T>(string topic);

        Task SubscribeAsync<T>();
        Task SubscribeAsync<T>(string topic);
    }
}