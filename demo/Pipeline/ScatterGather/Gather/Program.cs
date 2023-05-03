namespace Gather;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Please input ports which want to bind :");
        var ports = Console.ReadLine()?.Split(" ").Select(int.Parse);
        using (var gather = new ZaabeeZeroMessageBus(new Serializer()))
        {
            foreach (var port in ports)
                gather.GatherConnect($"tcp://localhost:{port}");
            Console.WriteLine($"Gather connect [{string.Join(",", ports)}] success.");
            while (true)
            {
                var msg = await gather.PullAsync<User>();
                Console.WriteLine($"Receive message [{msg.ToJson()}] on {DateTime.Now}");
            }
        }
    }
}