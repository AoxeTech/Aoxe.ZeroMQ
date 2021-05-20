using System.Threading.Tasks;
using NetMQ;

namespace Zaabee.ZeroMQ
{
    public partial class ZaabeeZeroMqHub
    {
        public ThreadSafeSocketOptions RadioSocketOptions => _radioSocket.Options;
        
        public void Publish<T>(T message) =>
            _radioSocket.Send(typeof(T).ToString(), _serializer.Serialize(message));

        public void Publish<T>(string topic, T message) =>
            _radioSocket.Send(topic, _serializer.Serialize(message));

        public async Task PublishAsync<T>(T message) =>
            await _radioSocket.SendAsync(typeof(T).ToString(), _serializer.Serialize(message));

        public async Task PublishAsync<T>(string topic, T message) =>
            await _radioSocket.SendAsync(topic, _serializer.Serialize(message));
    }
}