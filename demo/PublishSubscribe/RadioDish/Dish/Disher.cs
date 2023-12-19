namespace Dish;

public class Disher
{
    public async Task Handle(IEnumerable<int> ports, IEnumerable<string> topics)
    {
        using (var msgHub = new ZaabeeZeroMessageBus(new Serializer()))
        {
            msgHub.DishSocketOptions.ReceiveHighWatermark = 1000;
            foreach (var port in ports)
                msgHub.DishConnect($"tcp://localhost:{port}");
            if (topics is not null)
                foreach (var topic in topics)
                    msgHub.DishJoin(topic);

            Console.WriteLine("Subscriber socket connecting...");
            while (true)
            {
                var (topic, message) = await msgHub.DishReceiveAsync<User>();
                Console.WriteLine($"Topic:{topic}");
                Console.WriteLine($"Message:{message.ToJson()}");
            }
        }
    }
}
