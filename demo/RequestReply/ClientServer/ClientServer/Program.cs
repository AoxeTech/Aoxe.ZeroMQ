using System;
using System.Text;
using NetMQ;
using NetMQ.Sockets;

namespace ClientServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using var server = new ServerSocket();
            using var client = new ClientSocket();
            server.Bind("inproc://client-server");
            client.Connect("inproc://client-server");

            client.Send(Encoding.UTF8.GetBytes("Hello"));
            Console.WriteLine("Client has sent \"Hello\" to server.");
            
            var (routingId, clientMsg) = server.ReceiveBytes();
            
            Console.WriteLine($"Server received \"{Encoding.UTF8.GetString(clientMsg)}\" from client which routing Id [{routingId}]");

            server.Send(routingId, "World");
            Console.WriteLine($"Server sent \"World\" to routing Id [{routingId}]");
            var serverMsg = client.ReceiveString();
            Console.WriteLine($"Client received \"{serverMsg}\" from server.");
            Console.ReadLine();
        }
    }
}