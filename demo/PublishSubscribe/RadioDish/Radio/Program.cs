namespace Radio;

class Program
{
    static async Task Main(string[] args)
    {
        var rand = new Random();
        Console.WriteLine("Please input the ports which want to bind :");
        var ports = Console.ReadLine()?.Split(" ").Select(int.Parse);
        using (var msgHub = new ZaabeeZeroMessageBus(new Serializer()))
        {
            Console.WriteLine("Publisher socket binding...");
            msgHub.RadioSocketOptions.SendHighWatermark = 1000;
            foreach (var port in ports)
            {
                msgHub.RadioBind($"tcp://*:{port}");
                Console.WriteLine($"Radio socket has bind port [{port}].");
            }

            Console.Write("Please input the topics :");
            var topics = Console.ReadLine()?.Split(" ").ToList();
            Console.Write("Please input the publish quantity or exist:");
            var input = Console.ReadLine();
            while (input is not "exit")
            {
                var quantity = int.Parse(input ?? string.Empty);
                var sw = Stopwatch.StartNew();
                for (var i = 0; i < quantity; i++)
                {
                    var randomizedTopic = rand.Next(0, topics.Count);
                    var topic = topics[randomizedTopic];
                    var msg = new User
                    {
                        Id = Guid.NewGuid(),
                        Name = "Alice",
                        CreateTime = DateTime.Now
                    };
                    Console.WriteLine($"Sending message : {msg.ToJson()}");
                    await msgHub.PublishAsync(topic, msg);
                }

                sw.Stop();
                Console.WriteLine($"Take {sw.ElapsedMilliseconds} ms.");
                Console.Write("Please input the publish quantity or exist:");
                input = Console.ReadLine();
            }
        }
    }
}
