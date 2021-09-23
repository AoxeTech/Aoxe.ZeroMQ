using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using NetMQ;
using Zaabee.Jil;
using Zaabee.ZeroMQ;

namespace Dish
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input the ports which want to connect :");
            var ports = Console.ReadLine()?.Split(" ").Select(int.Parse);
            Console.WriteLine("Please input the groups :");
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
        public async Task Handle(IEnumerable<int> ports, IEnumerable<string> topics)
        {
            using (var msgHub = new ZaabeeZeroMessageBus(new ZaabeeSerializer()))
            {
                msgHub.DishSocketOptions.ReceiveHighWatermark = 1000;
                foreach (var port in ports)
                    msgHub.DishConnect($"tcp://localhost:{port}");
                if (topics is not null)
                    foreach (var topic in topics)
                        msgHub.DishJoin(topic);

                Console.WriteLine("Subscriber socket connecting...");
                while (true)
                {
                    var (group, message) = await msgHub.DishReceiveAsync<User>();
                    Console.WriteLine($"Topic:{group}");
                    Console.WriteLine($"Message:{message.ToJson()}");
                }
            }
        }
    }
}