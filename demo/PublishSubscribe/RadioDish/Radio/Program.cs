using System;
using System.Diagnostics;
using System.Linq;
using NetMQ;
using NetMQ.Sockets;

namespace Radio
{
    class Program
    {
        static void Main(string[] args)
        {
            var rand = new Random();
            Console.WriteLine("Please input the ports which want to bind :");
            var ports = Console.ReadLine()?.Split(" ").Select(int.Parse);
            using (var radioSocket = new RadioSocket())
            {
                Console.WriteLine("Publisher socket binding...");
                radioSocket.Options.SendHighWatermark = 1000;
                foreach (var port in ports)
                {
                    radioSocket.Bind($"tcp://*:{port}");
                    Console.WriteLine($"Radio socket has bind port [{port}].");
                }

                Console.Write("Please input the topics :");
                var topics = Console.ReadLine()?.Split(" ").ToList();
                Console.Write("Please input the publish quantity or exist:");
                var input = Console.ReadLine();
                while (input is not "exit")
                {
                    var quantity = int.Parse(input ?? string.Empty);
                    var sw = Stopwatch.StartNew();
                    for (var i = 0; i < quantity; i++)
                    {
                        var randomizedTopic = rand.Next(0, topics.Count);
                        var topic = topics[randomizedTopic];
                        var msg = $"{topic} msg-{i}";
                        Console.WriteLine("Sending message : {0}", msg);
                        radioSocket.Send(topic, msg);
                    }

                    sw.Stop();
                    Console.WriteLine($"Take {sw.ElapsedMilliseconds} ms.");
                    Console.Write("Please input the publish quantity or exist:");
                    input = Console.ReadLine();
                }
            }
        }
    }
}