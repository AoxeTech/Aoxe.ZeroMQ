using System;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace ScatterGather
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var scatter = new ScatterSocket();
            using var gather1 = new GatherSocket();
            using var gather2 = new GatherSocket();
            using var gather3 = new GatherSocket();

            scatter.Bind("inproc://test-scatter-gather");
            Console.WriteLine("Scatter bind.");
            gather1.Connect("inproc://test-scatter-gather");
            Console.WriteLine("Gather1 connected.");
            gather2.Connect("inproc://test-scatter-gather");
            Console.WriteLine("Gather2 connected.");
            gather3.Connect("inproc://test-scatter-gather");
            Console.WriteLine("Gather3 connected.");

            await scatter.SendAsync("1");
            Console.WriteLine("Scatter sent 1");
            await scatter.SendAsync("2");
            Console.WriteLine("Scatter sent 2");
            await scatter.SendAsync("3");
            Console.WriteLine("Scatter sent 3");
            await scatter.SendAsync("4");
            Console.WriteLine("Scatter sent 4");
            await scatter.SendAsync("5");
            Console.WriteLine("Scatter sent 5");
            await scatter.SendAsync("6");
            Console.WriteLine("Scatter sent 6");

            var m1 = await gather1.ReceiveStringAsync();
            Console.WriteLine($"Gather1 received {m1}");
            var m2 = await gather1.ReceiveStringAsync();
            Console.WriteLine($"Gather1 received {m2}");
            var m3 = await gather2.ReceiveStringAsync();
            Console.WriteLine($"Gather2 received {m3}");
            var m4 = await gather2.ReceiveStringAsync();
            Console.WriteLine($"Gather2 received {m4}");
            var m5 = await gather3.ReceiveStringAsync();
            Console.WriteLine($"Gather3 received {m5}");
            var m6 = await gather3.ReceiveStringAsync();
            Console.WriteLine($"Gather3 received {m6}");

            Console.ReadLine();
        }
    }
}