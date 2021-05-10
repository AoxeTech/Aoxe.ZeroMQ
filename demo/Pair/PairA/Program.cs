using System;
using NetMQ.Sockets;

namespace PairA
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var pairSocket = new PairSocket())
            {
                pairSocket.Bind("tcp://*:12345");
            }
        }
    }
}