using System.Threading.Tasks;
using NetMQ;

namespace Zaabee.ZeroMQ
{
    public partial class ZaabeeZeroMqHub
    {
        public ThreadSafeSocketOptions DishSocketOptions => _dishSocket.Options;

        public (string, T) Subscribe<T>()
        {
            var (group, messageBytes) = _dishSocket.ReceiveBytes();
            return (group, _serializer.Deserialize<T>(messageBytes));
        }

        public async Task<(string, T)> SubscribeAsync<T>()
        {
            var (group, messageBytes) = await _dishSocket.ReceiveBytesAsync();
            return (group, _serializer.Deserialize<T>(messageBytes));
        }
    }
}