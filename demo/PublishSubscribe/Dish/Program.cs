using System;
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
            var topics = args.Any() ? args : Array.Empty<string>();
            Console.WriteLine("Subscriber started for Topic : {0}", string.Join(",", topics));
            using (var netRt = new NetMQRuntime())
            {
                netRt.Run(new Disher().Handle(args));
            }
        }
    }

    public class Disher
    {
        public async Task Handle(params string[] topics)
        {
            using (var dishSocket = new DishSocket())
            {
                dishSocket.Options.ReceiveHighWatermark = 1000;
                dishSocket.Connect("tcp://localhost:12345");
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