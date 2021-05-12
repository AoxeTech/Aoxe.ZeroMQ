using System;
using System.Diagnostics;
using NetMQ;
using NetMQ.Sockets;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var rand = new Random(50);
            using (var pubSocket = new PublisherSocket())
            {
                Console.WriteLine("Publisher socket binding...");
                pubSocket.Options.SendHighWatermark = 1000;
                pubSocket.Bind("tcp://*:12345");

                Console.Write("Please input the publish quantity or exist:");
                var input = Console.ReadLine();
                while (input is not "exit")
                {
                    var sw = Stopwatch.StartNew();
                    var quantity = int.Parse(input ?? string.Empty);
                    for (var i = 0; i < quantity; i++)
                    {
                        var randomizedTopic = rand.NextDouble();
                        if (randomizedTopic > 0.5)
                        {
                            var msg = "TopicA msg-" + i;
                            Console.WriteLine("Sending message : {0}", msg);
                            pubSocket.SendMoreFrame("TopicA").SendFrame(msg);
                        }
                        else
                        {
                            var msg = "TopicB msg-" + i;
                            Console.WriteLine("Sending message : {0}", msg);
                            pubSocket.SendMoreFrame("TopicB").SendFrame(msg);
                        }
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