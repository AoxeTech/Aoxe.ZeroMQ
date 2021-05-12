using System;
using System.Threading.Tasks;

namespace Zaabee.NetMQ.Abstraction
{
    public interface ISubscriber
    {
        void Subscribe<T>(Func<Action<T>> resolve);
        void Subscribe<T>(string topic, Func<Action<T>> resolve);
        void Subscribe(string topic, Func<Action<string>> resolve);
        void Subscribe(string topic, Func<Action<byte[]>> resolve);

        Task SubscribeAsync<T>(Func<Action<T>> resolve);
        Task SubscribeAsync<T>(string topic, Func<Action<T>> resolve);
        Task SubscribeAsync(string topic, Func<Action<string>> resolve);
        Task SubscribeAsync(string topic, Func<Action<byte[]>> resolve);
    }
}