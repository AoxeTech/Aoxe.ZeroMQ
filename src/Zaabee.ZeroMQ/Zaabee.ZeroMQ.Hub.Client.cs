using System.Threading.Tasks;
using NetMQ;

namespace Zaabee.ZeroMQ
{
    public partial class ZaabeeZeroMqHub
    {
        public ThreadSafeSocketOptions ClientSocketOptions => _clientSocket.Options;
        public void ClientSend<T>(T message) =>
            _clientSocket.Send(_serializer.Serialize(message));

        public async Task ClientSendAsync<T>(T message) =>
            await _clientSocket.SendAsync(_serializer.Serialize(message));

        public T ClientReceive<T>() =>
            _serializer.Deserialize<T>(_clientSocket.ReceiveBytes());

        public async Task<T> ClientReceiveAsync<T>() =>
            _serializer.Deserialize<T>(await _clientSocket.ReceiveBytesAsync());
    }
}