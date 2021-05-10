using System;
using NetMQ;
using NetMQ.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var requestSocket = new RequestSocket(">tcp://localhost:5001"))
            {
                Console.WriteLine($"[{DateTime.Now}]Client start success.");
                while (true)
                {
                    var msg = Console.ReadLine();
                    if (msg is "exit") break;
                    if (string.IsNullOrWhiteSpace(msg)) continue;
                    requestSocket.SendFrame(msg);
                    msg = requestSocket.ReceiveFrameString();
                    Console.WriteLine($"[{DateTime.Now}]{msg}");
                }
            }
        }
    }
}