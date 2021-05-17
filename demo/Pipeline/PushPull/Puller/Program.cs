using System;
using NetMQ;
using NetMQ.Sockets;

namespace Puller
{
    class Program
    {
        static void Main(string[] args)
        {
            var name = args[0];
            using (var puller = new PullSocket())
            {
                puller.Connect("tcp://localhost:6009");
                Console.WriteLine($"{name} connect success.");
                while (true)
                {
                    var msg = puller.ReceiveFrameString();
                    Console.WriteLine($"Receive message [{msg}] on {DateTime.Now}");
                }
            }
        }
    }
}