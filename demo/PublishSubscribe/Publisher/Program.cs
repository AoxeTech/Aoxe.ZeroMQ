using System;
using System.Threading;
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
                for (var i = 0; i < 100; i++)
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
                    Thread.Sleep(500);
                }
            }
        }
    }
}