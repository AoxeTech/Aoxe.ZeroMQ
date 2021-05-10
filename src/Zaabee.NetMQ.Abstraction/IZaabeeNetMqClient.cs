using System;
using System.Threading.Tasks;

namespace Zaabee.NetMQ.Abstraction
{
    public interface IZaabeeNetMqClient
    {
        void Send<T>(T message);
        Task SendAsync<T>(T message);
        T Receive<T>();
        Task<T> ReceiveAsync<T>();

        void Publish<T>(T message);
        Task PublishAsync<T>(T message);
        void Subscribe<T>(Func<Action<T>> resolve);
        Task SubscribeAsync<T>(Func<Action<T>> resolve);
    }
}