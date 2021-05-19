using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Please input ports which want to connect :");
            var ports = Console.ReadLine()?.Split(" ").Select(int.Parse);
            using (var client = new ClientSocket())
            {
                foreach (var port in ports)
                    client.Connect($"tcp://localhost:{port}");
                Console.WriteLine($"Client has connected port [{string.Join(",", ports)}]");
                Console.WriteLine("Input message to send or exit :");
                var msg = Console.ReadLine();
                while (msg is not "exit")
                {
                    await client.SendAsync(Encoding.UTF8.GetBytes(msg));
                    Console.WriteLine($"Client has sent [{msg}] to server.");
                    var serverMsg = await client.ReceiveStringAsync();
                    Console.WriteLine($"Client received \"{serverMsg}\" from server on [{DateTime.Now}].");
                    Console.WriteLine("Input message to send or exit :");
                    msg = Console.ReadLine();
                }
            }
        }
    }
}