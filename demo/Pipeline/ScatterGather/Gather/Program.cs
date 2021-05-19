using System;
using System.Linq;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace Gather
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Please input ports which want to bind :");
            var ports = Console.ReadLine()?.Split(" ").Select(int.Parse);
            using (var gather = new GatherSocket())
            {
                foreach (var port in ports)
                    gather.Connect($"tcp://localhost:{port}");
                Console.WriteLine($"Gather connect [{string.Join(",", ports)}] success.");
                while (true)
                {
                    var msg = await gather.ReceiveStringAsync();
                    Console.WriteLine($"Receive message [{msg}] on {DateTime.Now}");
                }
            }
        }
    }
}