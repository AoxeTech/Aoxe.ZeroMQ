namespace Server;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Please input ports which want to bind :");
        var ports = Console.ReadLine()?.Split(" ").Select(int.Parse);
        using (var server = new ServerSocket())
        {
            foreach (var port in ports)
                server.Bind($"tcp://*:{port}");
            Console.WriteLine($"Server has connected port [{string.Join(",", ports)}]");
            while (true)
            {
                var (routingId, clientMsg) = await server.ReceiveStringAsync();
                Console.WriteLine(
                    $"Server has received message [{clientMsg}] from routingId [{routingId}]"
                );
                var serverMsg = $"Receive [{clientMsg}] on [{DateTime.Now}]";
                await server.SendAsync(routingId, serverMsg);
                Console.WriteLine($"Server has sent message : {serverMsg}");
            }
        }
    }
}
