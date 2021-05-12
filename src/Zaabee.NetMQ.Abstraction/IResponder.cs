using System.Threading.Tasks;

namespace Zaabee.NetMQ.Abstraction
{
    public interface IResponder
    {
        TResult Receive<T, TResult>(T message);
        Task<TResult> ReceiveAsync<T, TResult>(T message);
    }
}