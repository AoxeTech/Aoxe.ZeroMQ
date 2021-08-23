using System.Threading.Tasks;
using NetMQ;

namespace Zaabee.ZeroMQ
{
    public partial class ZaabeeZeroMessageBus
    {
        public ThreadSafeSocketOptions RadioSocketOptions => _radioSocket.Options;

        public void Publish<T>(T message) =>
            _radioSocket.Send(typeof(T).ToString(), _serializer.SerializeToBytes(message));

        public void Publish<T>(string topic, T message) =>
            _radioSocket.Send(topic, _serializer.SerializeToBytes(message));

        public async Task PublishAsync<T>(T message) =>
            await _radioSocket.SendAsync(typeof(T).ToString(), _serializer.SerializeToBytes(message));

        public async Task PublishAsync<T>(string topic, T message) =>
            await _radioSocket.SendAsync(topic, _serializer.SerializeToBytes(message));
    }
}