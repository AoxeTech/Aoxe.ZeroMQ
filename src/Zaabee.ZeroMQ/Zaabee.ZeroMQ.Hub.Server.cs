using System.Threading.Tasks;
using NetMQ;

namespace Zaabee.ZeroMQ
{
    public partial class ZaabeeZeroMqHub
    {
        public ThreadSafeSocketOptions ServerSocketOptions => _serverSocket.Options;

        public void ServerSend<T>(uint routingId, T message) =>
            _serverSocket.Send(routingId, _serializer.Serialize(message));

        public async Task ServerSendAsync<T>(uint routingId, T message) =>
            await _serverSocket.SendAsync(routingId, _serializer.Serialize(message));

        public (uint, T) ServerReceive<T>()
        {
            var (routingId, clientMsg) = _serverSocket.ReceiveBytes();
            return (routingId, _serializer.Deserialize<T>(clientMsg));
        }

        public async Task<(uint, T)> ServerReceiveAsync<T>()
        {
            var (routingId, clientMsg) = await _serverSocket.ReceiveBytesAsync();
            return (routingId, _serializer.Deserialize<T>(clientMsg));
        }
    }
}