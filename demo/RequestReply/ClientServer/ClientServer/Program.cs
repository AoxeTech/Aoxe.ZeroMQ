using System;
using Contracts;
using Zaabee.Jil;
using Zaabee.ZeroMQ;

namespace ClientServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using var msgHub = new ZaabeeZeroMessageBus(new ZaabeeSerializer());
            msgHub.ServerBind("tcp://*:12345");
            msgHub.ClientConnect("tcp://localhost:12345");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Alice",
                CreateTime = DateTime.Now
            };
            msgHub.ClientSend(user);
            Console.WriteLine($"Client sent [{user.ToJson()}] to server.");

            var (routingId, clientMsg) = msgHub.ServerReceive<User>();

            Console.WriteLine(
                $"Server received \"{clientMsg.ToJson()}\" from client which routing Id is [{routingId}]");

            clientMsg.Name = "Bob";
            msgHub.ServerSend(routingId, clientMsg);

            Console.WriteLine($"Server sent [{clientMsg.ToJson()}] to routing Id [{routingId}]");
            var serverMsg = msgHub.ClientReceive<User>();
            Console.WriteLine($"Client received \"{serverMsg.ToJson()}\" from server.");
            Console.ReadLine();
        }
    }
}