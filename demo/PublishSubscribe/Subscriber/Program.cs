using System;
using System.Collections.Generic;
using NetMQ;
using NetMQ.Sockets;

namespace Subscriber
{
    class Program
    {
        public static readonly IList<string> AllowableCommandLineArgs = new[] {"TopicA", "TopicB", "All"};

        static void Main(string[] args)
        {
            if (args.Length != 1 || !AllowableCommandLineArgs.Contains(args[0]))
            {
                Console.WriteLine("Expected one argument, either 'TopicA', 'TopicB' or 'All'");
                Environment.Exit(-1);
            }

            var topic = args[0] is "All" ? string.Empty : args[0];
            Console.WriteLine("Subscriber started for Topic : {0}", topic);
            using (var subSocket = new SubscriberSocket())
            {
                subSocket.Options.ReceiveHighWatermark = 1000;
                subSocket.Connect("tcp://localhost:12345");
                subSocket.Subscribe(topic);
                Console.WriteLine("Subscriber socket connecting...");
                while (true)
                {
                    var messageTopicReceived = subSocket.ReceiveFrameString();
                    var messageReceived = subSocket.ReceiveFrameString();
                    Console.WriteLine($"Topic:{messageTopicReceived}");
                    Console.WriteLine($"Message:{messageReceived}");
                }
            }
        }
    }
}