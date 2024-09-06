namespace Aoxe.ZeroMQ.Client;

public class AoxeZeroMqOptions(IBytesSerializer serializer)
{
    public IBytesSerializer Serializer { get; set; } = serializer;
    public string? ServerBindAddress { get; set; }
    public string? ClientConnectAddress { get; set; }
    public string? ScatterBindAddress { get; set; }
    public string? GatherConnectAddress { get; set; }
    public string? RadioBindAddress { get; set; }
    public string? DishConnectAddress { get; set; }
}
