using System;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace Gather
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var name = args[0];
            using (var gather = new GatherSocket())
            {
                gather.Connect("tcp://localhost:6009");
                Console.WriteLine($"{name} connect success.");
                while (true)
                {
                    var msg = await gather.ReceiveStringAsync();
                    Console.WriteLine($"Receive message [{msg}] on {DateTime.Now}");
                }
            }
        }
    }
}