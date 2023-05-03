namespace PairServer;

class Program
{
    static void Main(string[] args)
    {
        using (var pairSocket = new PairSocket())
        {
            Console.WriteLine("Socket start bind...");
            pairSocket.Bind("tcp://*:6000");
            Console.WriteLine("Socket bind success.");
            while (true)
            {
                pairSocket.SendFrame(Encoding.UTF8.GetBytes($"Server message to client3 in [{DateTime.Now}]"));
                var msg = pairSocket.ReceiveFrameBytes();
                Console.WriteLine(Encoding.UTF8.GetString(msg));
                Thread.Sleep(1000);
            }
        }
    }
}