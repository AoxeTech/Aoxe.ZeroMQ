using System.Threading.Tasks;

namespace Zaabee.ZeroMQ.Abstraction.Socket.Pipeline
{
    public interface IScatterSocket
    {
        void Push<T>(T message);
        Task PushAsync<T>(T message);
    }
}