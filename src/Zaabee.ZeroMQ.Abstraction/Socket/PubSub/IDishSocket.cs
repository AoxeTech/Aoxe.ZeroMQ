using System.Threading.Tasks;

namespace Zaabee.ZeroMQ.Abstraction.Socket.PubSub
{
    public interface IDishSocket
    {
        (string, T) DishReceive<T>();

        Task<(string, T)> DishReceiveAsync<T>();
    }
}