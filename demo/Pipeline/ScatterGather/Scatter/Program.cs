using System;
using System.Linq;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace Scatter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Please input ports which want to bind :");
            var ports = Console.ReadLine()?.Split(" ").Select(int.Parse);
            using (var scatter = new ScatterSocket())
            {
                foreach (var port in ports)
                    scatter.Bind($"tcp://*:{port}");
                Console.WriteLine($"Scatter bind [{string.Join(",", ports)}] success.");
                Console.Write("Please input send quantity or exit : ");
                var input = Console.ReadLine();
                while (!string.IsNullOrWhiteSpace(input) && input is not "exit")
                {
                    var quantity = int.Parse(input);
                    for (var i = 0; i < quantity; i++)
                        await scatter.SendAsync($"Scatter sent {i}");
                    Console.WriteLine("Please input send quantity or exit : ");
                    input = Console.ReadLine();
                }
            }
        }
    }
}