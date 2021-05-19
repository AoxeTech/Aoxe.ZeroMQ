namespace Zaabee.NetMQ.Serializer.Abstraction
{
    public interface ISerializer
    {
        byte[] Serialize<T>(T dto);
        T Deserialize<T>(byte[] message);
    }
}