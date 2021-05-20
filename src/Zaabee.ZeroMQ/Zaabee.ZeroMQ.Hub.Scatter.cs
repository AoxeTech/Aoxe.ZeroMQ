using System.Threading.Tasks;
using NetMQ;

namespace Zaabee.ZeroMQ
{
    public partial class ZaabeeZeroMqHub
    {
        public ThreadSafeSocketOptions ScatterSocketOptions => _scatterSocket.Options;

        public void Push<T>(T message) =>
            _scatterSocket.Send(_serializer.Serialize(message));

        public async Task PushAsync<T>(T message) =>
            await _scatterSocket.SendAsync(_serializer.Serialize(message));
    }
}