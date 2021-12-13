namespace Zaabee.ZeroMQ.Abstractions.Socket.PubSub;

public interface IDishSocket
{
    (string, T) DishReceive<T>();

    Task<(string, T)> DishReceiveAsync<T>();
}