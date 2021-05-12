namespace Zaabee.NetMQ.Abstraction
{
    public interface IPublisher
    {
        void Publish<T>(T message);
        void Publish<T>(string topic,T message);
        void Publish(string topic,string message);
        void Publish(string topic,byte[] message);
    }
}