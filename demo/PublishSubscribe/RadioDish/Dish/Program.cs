using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace Dish
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input the ports which want to connect :");
            var ports = Console.ReadLine()?.Split(" ").Select(int.Parse);
            Console.WriteLine("Please input the topics :");
            var topics = Console.ReadLine()?.Split(" ");

            Console.WriteLine($"Dish socket has bind ports [{string.Join(",", ports)}].");
            Console.WriteLine($"Subscriber started for Topic : {string.Join(",", topics)}");
            using (var netRt = new NetMQRuntime())
            {
                netRt.Run(new Disher().Handle(ports, topics));
            }
        }
    }

    public class Disher
    {
        public async Task Handle(IEnumerable<int> ports,IEnumerable<string> topics)
        {
            using (var dishSocket = new DishSocket())
            {
                dishSocket.Options.ReceiveHighWatermark = 1000;
                foreach (var port in ports)
                    dishSocket.Connect($"tcp://localhost:{port}");
                if (topics is not null)
                    foreach (var topic in topics)
                        dishSocket.Join(topic);

                Console.WriteLine("Subscriber socket connecting...");
                while (true)
                {
                    var (group, message) = await dishSocket.ReceiveStringAsync();
                    Console.WriteLine($"Topic:{group}");
                    Console.WriteLine($"Message:{message}");
                }
            }
        }
    }
}