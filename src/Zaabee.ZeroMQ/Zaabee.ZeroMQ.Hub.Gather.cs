using System.Threading.Tasks;
using NetMQ;

namespace Zaabee.ZeroMQ
{
    public partial class ZaabeeZeroMqHub
    {
        public ThreadSafeSocketOptions GatherSocketOptions => _gatherSocket.Options;

        public T Pull<T>() =>
            _serializer.DeserializeFromBytes<T>(_gatherSocket.ReceiveBytes());

        public async Task<T> PullAsync<T>() =>
            _serializer.DeserializeFromBytes<T>(await _gatherSocket.ReceiveBytesAsync());
    }
}