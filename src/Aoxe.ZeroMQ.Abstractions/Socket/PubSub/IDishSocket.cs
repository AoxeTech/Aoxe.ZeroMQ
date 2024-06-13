namespace Aoxe.ZeroMQ.Abstractions.Socket.PubSub;

public interface IDishSocket
{
    (string, T?) DishReceive<T>();

    ValueTask<(string, T?)> DishReceiveAsync<T>();
}
