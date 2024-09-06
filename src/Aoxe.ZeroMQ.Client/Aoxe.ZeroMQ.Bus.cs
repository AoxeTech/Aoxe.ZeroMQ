namespace Aoxe.ZeroMQ.Client;

public partial class AoxeZeroMessageBus : IAoxeZeroMessageBus
{
    private readonly IBytesSerializer _serializer;

    private readonly ServerSocket _serverSocket = new();
    private readonly ClientSocket _clientSocket = new();
    private readonly ScatterSocket _scatterSocket = new();
    private readonly GatherSocket _gatherSocket = new();
    private readonly RadioSocket _radioSocket = new();
    private readonly DishSocket _dishSocket = new();

    public AoxeZeroMessageBus(AoxeZeroMqOptions options)
        : this(
            options.Serializer,
            options.ServerBindAddress,
            options.ClientConnectAddress,
            options.ScatterBindAddress,
            options.GatherConnectAddress,
            options.RadioBindAddress,
            options.DishConnectAddress
        ) { }

    public AoxeZeroMessageBus(
        IBytesSerializer serializer,
        string? serverBindAddress = null,
        string? clientConnectAddress = null,
        string? scatterBindAddress = null,
        string? gatherConnectAddress = null,
        string? radioBindAddress = null,
        string? dishConnectAddress = null
    )
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

    public void ServerBind(params string[] addresses)
    {
        foreach (var address in addresses)
            _serverSocket.Bind(address);
    }

    public void ClientConnect(params string[] addresses)
    {
        foreach (var address in addresses)
            _clientSocket.Connect(address);
    }

    public void ScatterBind(params string[] addresses)
    {
        foreach (var address in addresses)
            _scatterSocket.Bind(address);
    }

    public void GatherConnect(params string[] addresses)
    {
        foreach (var address in addresses)
            _gatherSocket.Connect(address);
    }

    public void RadioBind(params string[] addresses)
    {
        foreach (var address in addresses)
            _radioSocket.Bind(address);
    }

    public void DishConnect(params string[] addresses)
    {
        foreach (var address in addresses)
            _dishSocket.Connect(address);
    }

    public void DishJoin(params string[] topics)
    {
        foreach (var topic in topics)
            _dishSocket.Join(topic);
    }

    public void Dispose()
    {
        _serverSocket.Dispose();
        _clientSocket.Dispose();
        _scatterSocket.Dispose();
        _gatherSocket.Dispose();
        _radioSocket.Dispose();
        _dishSocket.Dispose();
    }
}
