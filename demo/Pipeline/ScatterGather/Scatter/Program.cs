using System;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace Scatter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Please input send quantity or exit : ");
            var input = Console.ReadLine();
            Console.WriteLine($"Input : {input}");
            using (var scatter = new ScatterSocket())
            {
                scatter.Bind("tcp://*:6009");
                Console.WriteLine("Scatter bind success.");
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