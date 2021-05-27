using System.Threading.Tasks;
using NetMQ;

namespace Zaabee.ZeroMQ
{
    public partial class ZaabeeZeroMqHub
    {
        public ThreadSafeSocketOptions ClientSocketOptions => _clientSocket.Options;

        public void ClientSend<T>(T message) =>
            _clientSocket.Send(_serializer.SerializeToBytes(message));

        public async Task ClientSendAsync<T>(T message) =>
            await _clientSocket.SendAsync(_serializer.SerializeToBytes(message));

        public T ClientReceive<T>() =>
            _serializer.DeserializeFromBytes<T>(_clientSocket.ReceiveBytes());

        public async Task<T> ClientReceiveAsync<T>() =>
            _serializer.DeserializeFromBytes<T>(await _clientSocket.ReceiveBytesAsync());
    }
}