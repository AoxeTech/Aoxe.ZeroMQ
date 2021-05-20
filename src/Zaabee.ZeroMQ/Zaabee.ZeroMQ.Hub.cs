using NetMQ;
using NetMQ.Sockets;
using Zaabee.ZeroMQ.Abstraction;
using Zaabee.ZeroMQ.Serializer.Abstraction;

namespace Zaabee.ZeroMQ
{
    public partial class ZaabeeZeroMqHub : IZaabeeZeroMqHub
    {
        private readonly ISerializer _serializer;
        private readonly ServerSocket _serverSocket = new();
        private readonly ClientSocket _clientSocket = new();
        private readonly ScatterSocket _scatterSocket = new();
        private readonly GatherSocket _gatherSocket = new();
        private readonly RadioSocket _radioSocket = new();
        private readonly DishSocket _dishSocket = new();

        public ZaabeeZeroMqHub(ISerializer serializer,
            string serverBindAddress = null,
            string clientConnectAddress = null,
            string scatterBindAddress = null,
            string gatherConnectAddress = null,
            string radioBindAddress = null,
            string dishConnectAddress = null)
        {
            _serializer = serializer;
            if (serverBindAddress is not null)
                ServerBind(serverBindAddress);
            if (clientConnectAddress is not null)
                ClientConnect(clientConnectAddress);
            if (scatterBindAddress is not null)
                ScatterBind(scatterBindAddress);
            if (gatherConnectAddress is not null)
                GatherConnect(gatherConnectAddress);
            if (radioBindAddress is not null)
                RadioBind(radioBindAddress);
            if (dishConnectAddress is not null)
                DishConnect(dishConnectAddress);
        }

        public void ServerBind(string address) =>
            _serverSocket.Bind(address);

        public void ClientConnect(string address) =>
            _clientSocket.Connect(address);

        public void ScatterBind(string address) =>
            _scatterSocket.Bind(address);

        public void GatherConnect(string address) =>
            _gatherSocket.Connect(address);

        public void RadioBind(string address) =>
            _radioSocket.Bind(address);

        public void DishConnect(string address) =>
            _dishSocket.Connect(address);

        public void DishJoin(string group) =>
            _dishSocket.Join(group);

        public void Dispose()
        {
            _serverSocket.Dispose();
            _clientSocket.Dispose();
            _scatterSocket.Dispose();
            _gatherSocket.Dispose();
            _radioSocket.Dispose();
            _dishSocket.Dispose();

            NetMQConfig.Cleanup();
        }
    }
}