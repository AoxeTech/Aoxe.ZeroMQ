using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Zaabee.Jil;
using Zaabee.ZeroMQ;

namespace Gather
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Please input ports which want to bind :");
            var ports = Console.ReadLine()?.Split(" ").Select(int.Parse);
            using (var gather = new ZaabeeZeroMqHub(new Serializer()))
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
}